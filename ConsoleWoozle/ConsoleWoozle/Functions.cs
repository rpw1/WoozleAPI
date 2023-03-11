using Amazon.Lambda.Core;
using ConsoleWoozle.Domain;
using ConsoleWoozle.Services;
using Newtonsoft.Json;
using System.Diagnostics;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ConsoleWoozle
{
    public class Functions
    {
        #region Fields
        private readonly Startup _startup;
        private readonly IBuildSolutionService? _buildSolutionService;
        private readonly IS3Service? _s3Service;
        #endregion

        public Functions()
        {
            _startup = new Startup();
            _startup.ConfigureServices();
            _buildSolutionService = _startup.GetBuildSolutionService();
            _s3Service = _startup.GetS3Service();
        }

        #region GetSolution
        public async Task GetSolution(SolutionFileInput input, ILambdaContext _context)
        {

            _s3Service.GetFileStream()

            Trace.WriteLine($"Creating Solution for Artists {JsonConvert.SerializeObject(input)}");
            if (_buildSolutionService != null)
            {
                string? s3FilePath = await _buildSolutionService.ExecuteAsync(input).ConfigureAwait(false);
                Trace.WriteLine($"Finished Processing Solution filed saved with key {s3FilePath}");
            }
            else
            {
                Trace.WriteLine("Error BuildSolutionService is null");
            }
        }
        #endregion

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter Test Mode? (Y/N): ");
            bool isTestMode = Console.ReadLine() == "Y";

            while (true)
            {
                Console.WriteLine("Execute ");


            }
        }
    }
}
