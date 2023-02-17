using Amazon.S3;
using ConsoleWoozle.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Services
{
    public interface IS3Service
    {
        public Task WriteFile(string key);
    }
    public class S3Service : IS3Service
    {
        #region Fields
        private readonly IAmazonS3 _s3Client;
        private readonly S3Configuration _configuration;
        #endregion

        #region WriteFile
        public Task WriteFile(string key)
        {

        }
        #endregion
    }
}
