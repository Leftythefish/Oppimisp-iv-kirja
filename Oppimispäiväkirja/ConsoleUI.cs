using System;

namespace Oppimispäiväkirja
{
    class ConsoleUI
    {
        public static void Start()
        {
            Console.Clear();
            Console.WriteLine("VALITSE");
            Console.WriteLine("[1]\t Lisää aihe \n[2]\t Listaa aiheet\n[3]\t Muokkaa aihetta\n[4]\t Tallenna sessio\n ");
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.D1) { TopicHandler.AddNewTopic(); }
            else if (input.Key == ConsoleKey.D2) { TopicHandler.ListAllTopics(); }
            else if (input.Key == ConsoleKey.D3) { TopicHandler.FindTopicToModify(); }
            else if (input.Key == ConsoleKey.D4) { FileManager.SaveCurrentTopics(); }
            else
            {
                Console.Clear();
                Console.WriteLine("Antamasi arvo ei kelpaa.");
                Start();
            }
        }
    }
}
