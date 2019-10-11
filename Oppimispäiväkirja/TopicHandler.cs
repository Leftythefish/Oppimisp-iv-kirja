using System;
using System.Linq;

namespace Oppimispäiväkirja
{
    class TopicHandler
    {
        public static void ListAllTopics()
        {
            Console.Clear();
            foreach (var a in Program.Topicbox)
            {
                PrintTopic(a);
            }
            Console.ReadLine();
            ConsoleUI.Start();
        }
        public static void AddNewTopic()
        {
            string name;
            Topic aihe = new Topic();
            Console.Clear();
            try
            {
                Console.WriteLine("Lisää Aihe:");
                name = Console.ReadLine();
                aihe.Title = name;
                Program.Topicbox.Add(aihe);
            }
            catch (Exception)
            {
                Console.WriteLine("Jotain meni pieleen aiheen lisäämisessä. Palaa alkuun painamalla enter");
                Console.ReadLine();
                ConsoleUI.Start();
            }
            Console.WriteLine("[1]\t Lisää uusi aihe \n[2]\t Lisää aiheelle muita ominaisuuksia\n[3]\t Palaa alkuun");
            var i2 = Console.ReadKey();

            if (i2.Key == ConsoleKey.D1)
            {
                Console.Clear();
                AddNewTopic();
            }
            else if (i2.Key == ConsoleKey.D2)
            {
                Console.Clear();
                TopicHandler.AddProperties(aihe);
            }
            else if (i2.Key == ConsoleKey.D3)
            {
                Console.Clear();
                ConsoleUI.Start();
            }
            else
            {
                Console.Clear();
                ConsoleUI.Start();
            }
        }
        public static void FindTopicToModify()
        {
            Console.Clear();
            Console.WriteLine("Aiheet:");
            foreach (var item in Program.Topicbox)
            {
                Console.WriteLine(item.ID + "\t" + item.Title);
            }
            Console.WriteLine("Kirjoita sen aiheen ID jota haluat muokata:");
            string inputline = Console.ReadLine();
            var select = Program.Topicbox.Where(k => k.ID.Equals(Convert.ToInt32(inputline))).First();

            Console.Clear();
            PrintTopic(select);
            Console.WriteLine("----------------");
            Console.WriteLine("[1]\t Muokkaa aihetta \n[2]\t Poista aihe\n[3]\t Palaa alkuun\n");
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.D1)
            {
                AddProperties(select);
            }
            else if (input.Key == ConsoleKey.D2)
            {
                Program.Topicbox.RemoveAll((x) => x.ID.Equals(Convert.ToInt32(inputline)));
                FindTopicToModify();
            }
            else if (input.Key == ConsoleKey.D3)
            {
                ConsoleUI.Start();
            }

            else
            {
                Console.Clear();
                Console.WriteLine("Antamasi arvo ei kelpaa.");
                ConsoleUI.Start();
            }
        }
        public static void AddProperties(Topic aihe)
        {
            Console.Clear();
            Console.WriteLine($"Muokataan aihetta: {aihe.Title.ToUpper()}");
            Console.WriteLine("-----------------------");
            Console.WriteLine("VALITSE MINKÄ OMINAISUUDEN HALUAT LISÄTÄ/MUOKATA");
            Console.WriteLine("Huomioithan että ominaisuuden muokkaaminen poistaa aikaisemman tiedon järjestelmästä");
            Console.WriteLine();
            Console.WriteLine("[1]\t Nimi \n[2]\t Kuvaus\n[3]\t Aika-arvio\n[4]\t Käytetty aika\n[5]\t Lähde\n[6]\t Aloitus PVM");
            Console.WriteLine("[7]\t Vaihe \n[8]\t Valmistunut PVM\n[ENTER]\t Palaa alkuun\n");

            ConsoleKeyInfo input2 = Console.ReadKey();

            if (input2.Key == ConsoleKey.D1) { EditPropertyName(aihe); }
            else if (input2.Key == ConsoleKey.D2) { EditPropertyDescription(aihe); }
            else if (input2.Key == ConsoleKey.D3) { EditPropertyTimeEst(aihe); }
            else if (input2.Key == ConsoleKey.D4) { EditPropertyTimeSpent(aihe); }
            else if (input2.Key == ConsoleKey.D5) { EditPropertySource(aihe); }
            else if (input2.Key == ConsoleKey.D6) { EditPropertyStartLearningDate(aihe); }
            else if (input2.Key == ConsoleKey.D7) { EditPropertyInProgress(aihe); }
            else if (input2.Key == ConsoleKey.D8) { EditPropertyCompletionDate(aihe); }
            //else if (input2.Key == ConsoleKey.D9) { EditPropertySource(); }
            else if (input2.Key == ConsoleKey.Enter) { ConsoleUI.Start(); }
            else
            {
                Console.Clear();
                Console.WriteLine("Antamasi arvo ei kelpaa.");
                Console.ReadLine();
                AddProperties(aihe);
            }
        }
        public static void EditPropertyName(Topic aihe)
        {
            string e_name;
            Console.Clear();
            Console.WriteLine($"Nimi nyt: {aihe.Title}");
            Console.WriteLine("Uusi nimi:");
            e_name = Console.ReadLine();
            aihe.Title = e_name;
            AddProperties(aihe);
        }
        public static void EditPropertyDescription(Topic aihe)
        {
            string e_description;
            Console.Clear();
            Console.WriteLine($"Kuvaus nyt: {aihe.Description}");
            Console.WriteLine("Uusi kuvaus:");
            e_description = Console.ReadLine();
            aihe.Description = e_description;
            AddProperties(aihe);
        }
        public static void EditPropertyTimeEst(Topic aihe)
        {
            double e_time_estimate;
            Console.Clear();
            Console.WriteLine($"Arvioitu kesto nyt: {aihe.TimeEstimate}");
            Console.WriteLine("Anna uusi arvioitu kesto muodossa 00,00:");
            try
            {
                e_time_estimate = Convert.ToDouble(Console.ReadLine());
                aihe.TimeEstimate = e_time_estimate;
            }
            catch (Exception)
            {
                Console.WriteLine("Annettu kesto ei kelpaa. Paina Enteriä jatkaaksesi.");
                Console.ReadLine();
            }
            finally { AddProperties(aihe); }

        }
        public static void EditPropertyTimeSpent(Topic aihe)
        {
            double e_time_spent;
            Console.Clear();
            Console.WriteLine($"Arvioitu kesto nyt: {aihe.TimeSpent}");
            Console.WriteLine("Uusi arvioitu kesto muodossa 00,00:");
            try
            {
                e_time_spent = double.Parse(Console.ReadLine());
                aihe.TimeSpent = e_time_spent;
            }
            catch (Exception)
            {
                Console.WriteLine("Annettu kesto ei kelpaa. Paina Enteriä jatkaaksesi.");
                Console.ReadLine();
            }
            finally { AddProperties(aihe); }

        }
        public static void EditPropertySource(Topic aihe)
        {
            string e_source;
            Console.Clear();
            Console.WriteLine($"Lähde nyt: {aihe.Source}");
            Console.WriteLine("Uusi lähde:");
            e_source = Console.ReadLine();
            aihe.Source = e_source;
            AddProperties(aihe);
        }
        public static void EditPropertyCompletionDate(Topic aihe)
        {
            Console.Clear();
            Console.WriteLine($"Tämänhetkinen päättymispäivä: {aihe.CompletionDate}");
            try
            {
                Console.WriteLine("Anna päättymispäivämäärä muodossa: yyyy-mm-dd");

                String inp = Console.ReadLine();
                aihe.CompletionDate = DateTime.Parse(inp + " 8:00:00Z");
            }
            catch (FormatException)
            {
                Console.WriteLine("Annettu PVM ei kelpaa. Paina Enteriä jatkaaksesi. (PVM on oltava muodossa: yyyy-mm-dd)");
                Console.ReadLine();
                AddProperties(aihe);
            }
            finally { AddProperties(aihe); }

        }
        public static void EditPropertyInProgress(Topic aihe)
        {
            Console.Clear();
            string status;
            if (aihe.InProgress == true)
            {
                status = "Kesken";
            }
            else
            {
                status = "Valmis";
            }

            Console.WriteLine($"Tehtävän status: {status}");
            Console.WriteLine("Päivitä status:");
            Console.WriteLine("[1]\t KESKEN \n[2]\t VALMIS");
            var i3 = Console.ReadKey();

            if (i3.Key == ConsoleKey.D1)
            {
                aihe.InProgress = true;
                Console.Clear();
                AddProperties(aihe);

            }
            else if (i3.Key == ConsoleKey.D2)
            {
                aihe.InProgress = false;
                Console.Clear();
                AddProperties(aihe);
            }
            else
            {
                EditPropertyInProgress(aihe);
            }

            AddProperties(aihe);
        }
        public static void EditPropertyStartLearningDate(Topic aihe)
        {
            Console.Clear();
            Console.WriteLine($"Tämänhetkinen alkamispäivä: {aihe.StartLearningDate}");
            try
            {
                Console.WriteLine("Anna päättymispäivämäärä muodossa: yyyy-mm-dd");
                String inp = Console.ReadLine();
                aihe.StartLearningDate = DateTime.Parse(inp + " 8:00:00Z");
            }
            catch (FormatException)
            {
                Console.WriteLine("Annettu PVM ei kelpaa. Paina Enteriä jatkaaksesi. (PVM on oltava muodossa: yyyy-mm-dd)");
                Console.ReadLine();
                AddProperties(aihe);
            }
            finally { AddProperties(aihe); }
        }
        public static void DeleteTopic(Topic aihe)
        {
            Program.Topicbox.Remove(aihe);
        }
        public static void PrintTopic(Topic aihe)
        {
            string status;
            Console.WriteLine("----------------");
            if (aihe.InProgress == true)
            {
                status = "Kesken";
            }
            else
            {
                status = "Valmis";
            }
            Console.WriteLine(aihe.ID + " " + aihe.Title);
            Console.WriteLine("----------------");
            Console.WriteLine("Status: " + status);
            Console.WriteLine("Kuvaus:\n" + aihe.Description);
            Console.WriteLine("Arvioitu aika: " + aihe.TimeEstimate);
            Console.WriteLine("Käytetty aika: " + aihe.TimeSpent);
            Console.WriteLine("Lähde: " + aihe.Source);
            Console.WriteLine("Aloitettu: " + aihe.StartLearningDate.ToString("dd/MM/yyyy"));
            Console.WriteLine("Valmis: " + aihe.CompletionDate.ToString("dd/MM/yyyy"));
        }
    }
}
