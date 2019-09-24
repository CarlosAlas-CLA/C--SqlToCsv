using System;
using System.Data.SqlClient;
using System.IO;
namespace CodeLibrary
    {
    public static class CreateCsvFile
        {//Variables
        private static string connectionString;
        private static string queryString;
        private static string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff");
        private static string filesName;
        private static string outputFileNamePath;
        private static string copyToArchiveFilePath;
        private static string fileType = ".csv";
        private static string logFile ;
        private static string headerNames;
        private static string rows;
        private static SqlConnection sqlConnection;
        private static SqlDataReader sqlReader;
        private static StreamWriter streamWriter;
        private static SqlCommand sqlCommand;
        //Method
        public static void csvFile(string connection, string query, string fileName, string outputFilePath, string archiveDestinationPath, string logFilePath)
            {
            connectionString = connection;
            queryString = query;
            filesName = fileName;
            outputFileNamePath =  outputFilePath;
            copyToArchiveFilePath = archiveDestinationPath;
            logFile = logFilePath;
            try
                {
                using (sqlConnection = new SqlConnection(connectionString))
                    {
                    sqlCommand = new SqlCommand(queryString, sqlConnection);
                    sqlConnection.Open();
                    using (sqlReader = sqlCommand.ExecuteReader())
                        {
                        while (sqlReader.Read())
                            {
                           //Create file 
                            using (streamWriter = new StreamWriter(outputFileNamePath +filesName+ time + fileType))
                                {
                                // Loop through the fields and write headers
                                for (int i = 0; i < sqlReader.FieldCount; i++)
                                    {
                                    headerNames = sqlReader.GetName(i);
                                    if (headerNames.Contains(","))
                                        headerNames = "\"" + headerNames + "\"";
                                    streamWriter.Write(headerNames + ",");
                                    }
                                streamWriter.WriteLine();
                                // Loop through the rows and write data
                                    {
                                    for (int i = 0; i < sqlReader.FieldCount; i++)
                                        {
                                        rows = sqlReader[i].ToString();
                                        if (rows.Contains(","))
                                            rows = "\"" + rows + "\"";
                                        streamWriter.Write(rows + ",");
                                        }
                                    streamWriter.WriteLine();
                                    }
                                }//copy to archive
                            File.Copy(outputFileNamePath +filesName + time + fileType, copyToArchiveFilePath +filesName+ time + fileType);
                            }
                        }
                    }
                }
            catch (Exception ex)
                {//Write to log file
                File.AppendAllText(logFilePath, "\n*******************************"+time+"\n"+ex.TargetSite + "\n "+ex.Message +"\n"+ ex.Data + "\n"+ex.StackTrace+ "\n*******************************");
               
                }
            }
        }
    }