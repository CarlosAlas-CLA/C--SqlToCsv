using CodeLibrary;
namespace TestCodeLibrary
    {
    static class Program
        {
        static void Main(string[] args)
            {
            string connectionString= " ";
            string query = "select *from dbo.test";
            string fileName = "Daily Invoice -";
            string archive = @"C:\Desktop\New folder\Archive\";
            string output = @"C:Desktop\New folder\Output\";
            string log = @"C:\Desktop\New folder\log.txt";
            
            CreateCsvFile.csvFile(connectionString, query,fileName, archive, output, log);
            }
        }
    }
