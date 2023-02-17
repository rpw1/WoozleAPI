using ConsoleWoozle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Services
{

    public interface IBuildSolutionService
    {
        public Task<string?> ExecuteAsync(SolutionFileInput input);
    }

    public class BuildSolutionService : IBuildSolutionService
    {
        public Task<string?> ExecuteAsync(SolutionFileInput input)
        {
            throw new NotImplementedException();
        }
    }
}
