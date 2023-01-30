using Amazon.Auth.AccessControlPolicy;
using Amazon.Runtime.Internal;
using ConsoleWoozle.Configuration;
using ConsoleWoozle.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle
{
    public class Startup
    {
        #region Fields
        private IConfiguration? _configuration;
        private IServiceProvider? _serviceProvider;
        private readonly IServiceCollection _serviceCollection;
        #endregion

        public Startup() 
        { 
            _serviceCollection = new ServiceCollection();
        }

        #region ConfigureService
        public void ConfigureServices()
        {
            _serviceCollection.AddHttpContextAccessor();
            _serviceCollection.AddSingleton<IConfigurationService, ConfigurationService>();

            var configurationService = _serviceCollection.BuildServiceProvider().GetService<IConfigurationService>();
            if (configurationService != null )
            {
                _configuration = configurationService.GetConfiguration();
                _serviceCollection.AddSingleton(GetSpotifyCredentialsConfiguration(_configuration));
                _serviceCollection.AddSingleton<ISecretsManagementService, SecretsManagementService>();
                _serviceCollection.AddSingleton<ISpotifyAuthenticationService, SpotifyAuthenticationService>();
                _serviceCollection.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddLambdaLogger(_configuration.GetLambdaLoggerOptions());
                });
            }
            ConfigureHttpClient(_serviceCollection);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }
        #endregion

        #region GetServices
        public ISecretsManagementService? GetSecretsManagementService()
        {
            return _serviceProvider?.GetService<ISecretsManagementService>();
        }
        #endregion

        #region ConfigureHttpClient(IServiceCollection services)
        private void ConfigureHttpClient(IServiceCollection services)
        {
            services.AddHttpClient(ApplicationConstants.ApplicationId,
                c => { c.Timeout = TimeSpan.FromSeconds(25); })
                .AddPolicyHandler(ConfigureHttpClientRetryPolicy(services, ApplicationConstants.ApplicationId))
                .AddPolicyHandler(GetHttpClientRequestTimeoutPolicy())
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                    ServerCertificateCustomValidationCallback = delegate { return true; },
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                });
        }
        #endregion

        #region GetHttpClientRequestTimeoutPolicy
        public static AsyncTimeoutPolicy<HttpResponseMessage> GetHttpClientRequestTimeoutPolicy()
        {
            return Polly.Policy.TimeoutAsync<HttpResponseMessage>(5);
        }
        #endregion

        #region ConfigureHttpClientRetryPolicy
        private AsyncRetryPolicy<HttpResponseMessage> ConfigureHttpClientRetryPolicy(IServiceCollection services, string httpClientName)
        {
            HttpStatusCode[] httpStatusCodesValidForRetry =
            {
                HttpStatusCode.RequestTimeout,
                HttpStatusCode.InternalServerError,
                HttpStatusCode.BadGateway,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.GatewayTimeout
            };

            var policy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(r => httpStatusCodesValidForRetry.Contains(r.StatusCode))
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(3),
                });
            return policy;
        }
        #endregion

        #region GetSpotifyCredentialsConfiguration
        private static SpotifyConfiguration GetSpotifyCredentialsConfiguration(IConfiguration configuration)
        {
            return new SpotifyConfiguration
            {
                CredentialsArn = configuration.GetValue<string>(ConfigurationKeys.SpotifyCredentialsArn),
                AutenticationUrl = configuration.GetValue<string>(ConfigurationKeys.SpotifyAuthenticationUrl),
                BaseUrl = configuration.GetValue<string>(ConfigurationKeys.SpotifyBaseUrl)
            };
        }
        #endregion
    }
}
