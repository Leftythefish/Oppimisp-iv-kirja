using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Oppimispäiväkirja
{
    class Program
    {
        static List<string> tallennustiedosto = new List<string>();
        static List<Topic> Topicbox = new List<Topic>();

        static void Main(string[] args)
        {
            LoadAllTopics();
            Start();
        }

        private static void LoadAllTopics()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\riaah\OneDrive\Tiedostot\ACADEMY\Oppimispäiväkirja\Topics.txt"))
                {
                    string line;
                    // Read and display lines from the file until the end of the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        tallennustiedosto.Add(line);
                    }
                    sr.Dispose();
                }
                FileModifyFields();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Tiedostoa ei löydy.");
            }


        }

        private static void Start()
        {
            Console.Clear();
            /*string input = Console.ReadLine();
            args = input.Split(" ");*/
            Console.WriteLine("VALITSE");
            Console.WriteLine("[1]\t Lisää aihe \n[2]\t Listaa aiheet\n[3]\t Muokkaa aihetta\n[4]\t Tallenna sessio\n ");
            var input = Console.ReadKey();


            if (input.Key == ConsoleKey.D1) { AddNewTopic(); }
            else if (input.Key == ConsoleKey.D2) { ListAllTopics(); }
            else if (input.Key == ConsoleKey.D3) { FindTopicToModify(); }
            else if (input.Key == ConsoleKey.D4) { SaveCurrentTopics(); }

            else
            {
                Console.Clear();
                Console.WriteLine("Antamasi arvo ei kelpaa.");
                Start();
            }

        }

        private static void SaveCurrentTopics()
        {
            try
            {
                using (StreamWriter sw2 = new StreamWriter(@"C:\Users\riaah\OneDrive\Tiedostot\ACADEMY\Oppimispäiväkirja\Topics.txt"))
                {
                    foreach (var topic in Topicbox)
                    {
                        string status;
                        if (topic.InProgress == true)
                        {
                            status = "Kesken";
                        }
                        else
                        {
                            status = "Valmis";
                        }
                        sw2.WriteLine(topic.Title + ";" + topic.Description + ";" + topic.TimeEstimate + ";" + topic.TimeSpent + ";" + topic.Source + ";" + topic.StartLearningDate + ";" + topic.CompletionDate + ";" + status);
                    }
                    sw2.Dispose();
                }
                Console.Clear();
                Console.WriteLine("Tiedot tallennettu.");
                Console.ReadLine();
            }
            catch (FileNotFoundException) { Console.WriteLine("Tiedostoa ei löydy."); }
            catch (Exception ex) { Console.WriteLine($"Tuntematon ongelma tiedoston kanssa, {ex}"); }
            Start();
        }

        private static void ListAllTopics()
        {
            string status;
            Console.Clear();
            foreach (var a in Topicbox)
            {
                Console.WriteLine("----------------");
                if (a.InProgress == true)
                {
                    status = "Kesken";
                }
                else
                {
                    status = "Valmis";
                }
                Console.WriteLine("Status: " + status);
                Console.WriteLine(a.ID + " " + a.Title + "\nKuvaus:" + a.Description);
                Console.WriteLine("Arvioitu aika:" + a.TimeEstimate);
                Console.WriteLine("Käytetty aika:" + a.TimeSpent);
                Console.WriteLine("Lähde:" + a.Source);
                Console.WriteLine("Aloitettu:" + a.StartLearningDate.ToString());
                Console.WriteLine("Valmis:" + a.CompletionDate.ToString());
            }
            Console.ReadLine();
            Start();
        }

        private static void AddNewTopic()
        {
            string name;
            Topic aihe = new Topic();
            Console.Clear();
            try
            {
                Console.WriteLine("Lisää Aihe:");
                name = Console.ReadLine();
                aihe.Title = name;
                Topicbox.Add(aihe);
            }
            catch (Exception)
            {
                Console.WriteLine("Jotain meni pieleen aiheen lisäämisessä. Palaa alkuun painamalla enter");
                Console.ReadLine();
                Start();
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
                AddProperties(aihe);
            }
            else if (i2.Key == ConsoleKey.D3)
            {
                Console.Clear();
                Start();
            }
            else
            {
                Console.Clear();
                Start();
            }            
        }

        private static void FileModifyFields()
        {
            foreach (var rivi in tallennustiedosto)
            {
                string[] om;
                om = rivi.Split(";");
                //järjestys on NIMI KUVAUS AIKA-ARVIO, KULUNUT AIKA, LÄHDE 
                /*UNIMPLEMENTED: DateTime start_date, bool inprogress, DateTime completion_date*/
                string nimi, kuvaus, source;
                double time_estimate, time_spent;
                DateTime startdate, end_date;
                bool inprogress;
                try
                {
                    if (om[0] == null || om[0] == "")
                    {
                        nimi = "default";
                    }
                    else
                    {
                        nimi = om[0];
                    }
                    if (om[1] == null || om[1] == "")
                    {
                        kuvaus = "defaultkuvaus";
                    }
                    else
                    {
                        kuvaus = om[1];
                    }
                    if (om[2] == null || om[2] == "")
                    {
                        time_estimate = 00.00;
                    }
                    else
                    {
                        try
                        {
                            time_estimate = Convert.ToDouble(om[2]);
                        }
                        catch (Exception)
                        {
                            time_estimate = 00.00;
                        }
                    }
                    if (om[3] == null || om[3] == "")
                    {
                        time_spent = 00.00;
                    }
                    else
                    {
                        try
                        {
                            time_spent = Convert.ToDouble(om[2]);
                        }
                        catch (Exception)
                        {
                            time_spent = 00.00;
                        }
                    }
                    if (om[4] == null || om[4] == "")
                    {
                        source = "Default lähde";
                    }
                    else
                    {
                        source = om[4];
                    }
                    if (om[5] == null || om[5] == "")
                    {
                        startdate = DateTime.Parse("2019-01-01 8:00:00Z");
                    }
                    else
                    {
                        startdate = DateTime.Parse(om[5]);
                    }
                    if (om[6] == null || om[6] == "")
                    {
                        end_date = DateTime.Parse("2019-01-01 8:00:00Z");
                    }
                    else
                    {
                        end_date = DateTime.Parse(om[6]);
                    }
                    if (om[7] == null || om[7] == "")
                    {
                        inprogress = true;
                    }
                    else
                    {
                        if (om[7] == "Kesken")
                        {
                            inprogress = true;
                        }
                        if (om[7] == "Valmis")
                        {
                            inprogress = false;
                        }
                        else
                        {
                            inprogress = true;
                        }
                    }
                    Topic topic = new Topic(nimi, kuvaus, time_estimate, time_spent, source, startdate, end_date, inprogress);
                    Topicbox.Add(topic);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Virhetila tiedoston lukemisessa.");
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private static void FindTopicToModify()
        {
            Console.Clear();
            Console.WriteLine("Aiheet:");
            foreach (var item in Topicbox)
            {
                Console.WriteLine(item.ID + "\t" + item.Title);
            }
            Console.WriteLine("Kirjoita sen aiheen ID jota haluat muokata:");
            string inputline = Console.ReadLine();
            var select = Topicbox.Where(k => k.ID.Equals(Convert.ToInt32(inputline)));
            
            Console.WriteLine("[1]\t Muokkaa aihetta \n[2]\t Poista aihe\n[3]\t Palaa alkuun\n");
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.D1)
            {
                foreach (var item in select)
                {
                    AddProperties(item);
                }
            }
            else if (input.Key == ConsoleKey.D2)
            {
                Topicbox.RemoveAll((x) => x.ID.Equals(Convert.ToInt32(inputline)));
                FindTopicToModify();
            }
            else if (input.Key == ConsoleKey.D3) 
            {
                Start();
            }
            
            else
            {
                Console.Clear();
                Console.WriteLine("Antamasi arvo ei kelpaa.");
                Start();
            }
        }

        private static void DeleteTopic(Topic item)
        {
            Topicbox.Remove(item);
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
            else if (input2.Key == ConsoleKey.Enter) { Start(); }
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
            string e_name = "";
            Console.Clear();
            Console.WriteLine($"Nimi nyt: {aihe.Title}");
            Console.WriteLine("Uusi nimi:");
            e_name = Console.ReadLine();
            aihe.Title = e_name;
            AddProperties(aihe);
        }
        public static void EditPropertyDescription(Topic aihe)
        {
            string e_description = "";
            Console.Clear();
            Console.WriteLine($"Kuvaus nyt: {aihe.Description}");
            Console.WriteLine("Uusi kuvaus:");
            e_description = Console.ReadLine();
            aihe.Description = e_description;
            AddProperties(aihe);
        }
        public static void EditPropertyTimeEst(Topic aihe)
        {
            double e_time_estimate = 0.0;
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
            double e_time_spent = 0.0;
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
            string e_source = "";
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
    }
}