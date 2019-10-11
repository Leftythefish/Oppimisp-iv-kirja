using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Oppimispäiväkirja
{
    class Program
    {

        public static List<Topic> Topicbox = new List<Topic>();

        static void Main(string[] args)
        {
            FileManager filu = new FileManager();
            FileManager.LoadAllTopics(filu);
            ConsoleUI.Start();
        }

















    }
}