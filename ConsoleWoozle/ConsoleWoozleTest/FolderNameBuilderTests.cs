using ConsoleWoozle.Utils;
using Xunit;

namespace ConsoleWoozleTest
{
    public class FolderNameBuilderTests
    {

        [Fact]
        public void Test_Folder_Names()
        {
            FolderNameBuilder.BuildFolderName(null);
            FolderNameBuilder.BuildFolderName(new List<string> { });
            FolderNameBuilder.BuildFolderName(new List<string> { "6qqNVTkY8uBg9cP3Jd7DAH", "4iMO20EPodreIaEl8qW66y", "7tYKF4w9nC0nq9CsPZTHyP" });
            FolderNameBuilder.BuildFolderName(new List<string> { "6qqNVTkY8uBg9cP3Jd7DAH", "7tYKF4w9nC0nq9CsPZTHyP", "4iMO20EPodreIaEl8qW66y" });
            FolderNameBuilder.BuildFolderName(new List<string> { "6qqNVTkY8uBg9cP3Jd7DAH", "4iMO20EPodreIaEl8qW66y" });
            FolderNameBuilder.BuildFolderName(new List<string> { "6qqNVTkY8uBg9cP3Jd7DAH" });
            
        }
    }
}
