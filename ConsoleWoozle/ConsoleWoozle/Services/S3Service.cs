using Amazon.S3;
using Amazon.S3.Model;
using ConsoleWoozle.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Services
{
    public interface IS3Service
    {
        public Task<Stream?> GetFileStream(string key);
        public Task WriteFile(string key);
    }
    public class S3Service : IS3Service
    {
        #region Fields
        private readonly IAmazonS3? _s3Client;
        private readonly S3Configuration? _configuration;
        #endregion

        #region IsCreated
        public async Task<Stream?> GetFileStream(string key) 
        {
            if (_s3Client != null)
            {
                Trace.WriteLine($"Calling S3Service.GetFileStream with key: {key}");

                GetObjectRequest request = new()
                {
                    Key = key,
                    BucketName = _configuration?.BucketName
                };

                var response = await _s3Client.GetObjectAsync(request).ConfigureAwait(false);

                return response.ResponseStream;
            }

            return null;
        }
        #endregion


        #region WriteFile
        public Task WriteFile(string key)
        {

        }
        #endregion
    }
}
