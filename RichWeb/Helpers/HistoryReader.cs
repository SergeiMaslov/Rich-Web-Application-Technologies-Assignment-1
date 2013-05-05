using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using RichWeb.Models;

namespace RichWeb.Helpers
{
    public class HistoryReader
    {
        private static HistoryReader _instance;

        private readonly string _filepath = Path.Combine(MvcApplication.Root, "App_Data\\history.txt");
        
        private HistoryReader()
		{}

        public static HistoryReader GetInstance()
        {
            return _instance ?? (_instance = new HistoryReader());
        }

        public List<Record> GetHistory(string username)
        {
            List<Record> output = new List<Record>();
            using (StreamReader reader = new StreamReader(_filepath))
            {
                while (reader.Peek() > 0)
                {
                    string[] splittedRecord = reader.ReadLine().Split(':');
                    output.Add(new Record
                        {
                            Username = splittedRecord[0],
                            Message = splittedRecord[1]
                        });
                }
            }
            return output;
        }

        public void AddToHistory(string username, string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_filepath, true))
                {
                    writer.WriteLine(username + ":" + message);
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}