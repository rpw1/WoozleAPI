using System.Text;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Amazon.SecretsManager.Extensions.Caching;
using Newtonsoft.Json;

namespace ConsoleWoozle.Services
{
    public interface ISecretsManagementService
    {
        Task<T?> GetSecretAsync<T>(string secretArn);
    }

    public class SecretsManagementService : ISecretsManagementService
    {

        private readonly SecretsManagerCache _secretsManagerCache;

        public SecretsManagementService() 
        {
            AmazonSecretsManagerClient secretsManager = new AmazonSecretsManagerClient(RegionEndpoint.USEast1);
            _secretsManagerCache= new SecretsManagerCache(secretsManager);
        }
        public async Task<T?> GetSecretAsync<T>(string secretArn)
        {
            GetSecretValueResponse response = new();
            try
            {
                response = await _secretsManagerCache.GetCachedSecret(secretArn).GetSecretValue();
            }
            catch (Exception ex)
            {
                Console.WriteLine(new AmazonSecretsManagerException(ex));
            }

            if (!string.IsNullOrEmpty(response.SecretString))
            {
                return JsonConvert.DeserializeObject<T>(response.SecretString);
            }
            else
            {
                StreamReader streamReader = new(response.SecretBinary);
                string secretString = Encoding.UTF8.GetString(Convert.FromBase64String(await streamReader.ReadToEndAsync().ConfigureAwait(false)));
                return JsonConvert.DeserializeObject<T>(secretString);
            }
        }
    }

}
