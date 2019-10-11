using System.Collections.Generic;

namespace Oppimispäiväkirja
{
    class Program
    {
        public static List<Topic> Topicbox = new List<Topic>();
        private static void Main()
        {
            FileManager filu = new FileManager();
            FileManager.LoadAllTopics(filu);
            ConsoleUI.Start();
        }
    }
}