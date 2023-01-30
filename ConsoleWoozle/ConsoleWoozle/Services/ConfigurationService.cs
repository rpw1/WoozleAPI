using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Services
{

    public interface IConfigurationService
    {
        IConfiguration GetConfiguration();
    }

    public class ConfigurationService : IConfigurationService
    {
        private IConfiguration? _configuration;
        public IConfiguration GetConfiguration()
        {
            return _configuration ?? (_configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build());
        }
    }
}
