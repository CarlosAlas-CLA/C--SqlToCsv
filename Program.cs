using CodeLibrary;
namespace TestCodeLibrary
    {
    static class Program
        {
        static void Main(string[] args)
            {
            string connectionString= "";
            string query = "select *from dbo.test";
            string archive = @"";
            string output = @"";
            string log = @"";
            
            CreateCsvFile.csvFile(connectionString, query, archive, output, log);
            }
        }
    }
