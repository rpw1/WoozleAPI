using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ConsoleWoozle
{
    public class Functions
    {
        #region Fields
        private readonly Startup _startup;

        #endregion

        public Functions()
        {
            _startup = new Startup();
            _startup.ConfigureServices();
        }

        #region CreateSolutionScheduleFile
        public void CreateSolutionScheduleFile(ILambdaContext _context)
        {

        }
        #endregion

    }
}
