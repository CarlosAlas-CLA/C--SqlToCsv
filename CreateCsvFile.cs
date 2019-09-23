﻿using System;
using System.Data.SqlClient;
using System.IO;
namespace CodeLibrary
    {
    public static class CreateCsvFile
        {
        private static string connec;
        private static string queryString;
        private static string time = DateTime.Now.ToString("yyyyMMddHHmmssffff");
        private static string archiveFileName;
        private static string copyArchiveFileName;
        private static string fileType = ".csv";
        private static string logFile = ".txt";
        private static string name;
        private static string value;
        private static SqlConnection conn;
        private static SqlDataReader reader;
        private static StreamWriter fs;
        private static SqlCommand command;
        public static void csvFile(string connection, string query, string archiveFileToWritePath, string fileDestinationPath, string logFilePath)
            {
            connec = connection;
            queryString = query;
            archiveFileName = archiveFileToWritePath;
            copyArchiveFileName = fileDestinationPath;
            logFile = logFilePath;
            try
                {
                using (conn = new SqlConnection(connec))
                    {
                    command = new SqlCommand(queryString, conn);
                    conn.Open();
                    using (reader = command.ExecuteReader())
                        {
                        while (reader.Read())
                            {
                            // File.WriteAllText(filename, reader[0].ToString());
                            using (fs = new StreamWriter(archiveFileName + time + fileType))
                                {
                                // Loop through the fields and add headers
                                for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                    name = reader.GetName(i);
                                    if (name.Contains(","))
                                        name = "\"" + name + "\"";
                                    fs.Write(name + ",");
                                    }
                                fs.WriteLine();
                                // Loop through the rows and output the data
                                while (reader.Read())
                                    {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                        value = reader[i].ToString();
                                        if (value.Contains(","))
                                            value = "\"" + value + "\"";
                                        fs.Write(value + ",");
                                        }
                                    fs.WriteLine();
                                    }
                                }
                            File.Copy(archiveFileName + time + fileType, copyArchiveFileName + time + fileType);
                            }
                        }
                    }
                }
            catch (Exception ex)
                {
                File.AppendAllText(logFilePath , ex.Message + ex.InnerException + ex.Source + ex.HelpLink);
                }
            }
        }
    }