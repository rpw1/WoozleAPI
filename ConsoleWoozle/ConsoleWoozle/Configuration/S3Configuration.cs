using Amazon;

namespace ConsoleWoozle.Configuration
{
    public class S3Configuration
    {
        public RegionEndpoint? RegionEndpoint { get; set; }
        public string? BucketName { get; set; }
    }
}
