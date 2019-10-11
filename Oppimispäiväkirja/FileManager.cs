using System;
using System.Collections.Generic;
using System.IO;


namespace Oppimispäiväkirja
{
    class FileManager
    {
        static readonly List<string> tallennustiedosto = new List<string>();

        public static void LoadAllTopics(FileManager _)
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


        public static void FileModifyFields()
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
                    Program.Topicbox.Add(topic);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Virhetila tiedoston lukemisessa.");
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        public static void SaveCurrentTopics()
        {
            try
            {
                using (StreamWriter sw2 = new StreamWriter(@"C:\Users\riaah\OneDrive\Tiedostot\ACADEMY\Oppimispäiväkirja\Topics.txt"))
                {
                    foreach (var topic in Program.Topicbox)
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
            ConsoleUI.Start();
        }

    }
}
