using System.Collections.Generic;
using System.Data.SqlClient;

namespace Oppimispäiväkirja
{
    class Program
    {

        public static List<Topic> Topicbox = new List<Topic>();
        private static void Main()
        {

            SQLMethods.GetList();
            //FileManager filu = new FileManager();
            //FileManager.LoadAllTopics(filu);
            ConsoleUI.Start();
        }
    }
}