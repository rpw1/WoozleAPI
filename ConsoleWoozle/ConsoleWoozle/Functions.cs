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
        #endregion

        public Functions()
        {
            _startup = new Startup();
            _startup.ConfigureServices();
            _buildSolutionService = _startup.GetBuildSolutionService();
        }

        #region CreateSolutionScheduleFile
        public async Task BuildSolution(SolutionFileInput input, ILambdaContext _context)
        {
            Trace.WriteLine($"Creating Solution for Artists {JsonConvert.SerializeObject(input)}");
            if (_buildSolutionService != null )
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

        }
    }
}
