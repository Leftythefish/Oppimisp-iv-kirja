using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace Oppimispäiväkirja
{
    public class SQLMethods
    {
        public static SqlConnection c = new SqlConnection("database=Topics;Server=localhost;trusted_connection=true");
        public static string query;

        public static void AddToServer() 
        {
            foreach (var item in Program.Topicbox)
            {
                AddOrUpdate(item);
            }
            Console.Clear();
            Console.WriteLine("Tietokanta päivitetty");
            Console.ReadLine();
            ConsoleUI.Start();
        }
        public static void GetList() 
        {
            query = "select * from TopicList";
            c.Open();
            SqlCommand cmd = new SqlCommand(query, c);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                string nimi, kuvaus, source;
                double time_estimate, time_spent;
                DateTime startdate, end_date;
                bool inprogress;
                try
                {
                    if (r[1] == null)
                    {
                        nimi = "default";
                    }
                    else
                    {
                        nimi = (String)r[1];
                    }
                    if (r[2] == null)
                    {
                        kuvaus = "defaultkuvaus";
                    }
                    else
                    {
                        kuvaus = (String)r[2];
                    }
                    if (r[3] == null)
                    {
                        time_estimate = 00.00;
                    }
                    else
                    {
                        try
                        {
                            time_estimate = Convert.ToDouble(r[3]);
                        }
                        catch (Exception)
                        {
                            time_estimate = 00.00;
                        }
                    }
                    if (r[4] == null)
                    {
                        time_spent = 00.00;
                    }
                    else
                    {
                        try
                        {
                            time_spent = Convert.ToDouble(r[4]);
                        }
                        catch (Exception)
                        {
                            time_spent = 00.00;
                        }
                    }
                    if (r[5] == null)
                    {
                        source = "Default lähde";
                    }
                    else
                    {
                        source = (String)r[5];
                    }
                    if (r[6] == null)
                    {
                        startdate = DateTime.Parse("2019-01-01 8:00:00Z");
                    }
                    else
                    {                       
                        startdate = (DateTime)r[6];
                    }
                    if (r[7] == null)
                    {
                        end_date = DateTime.Parse("2019-01-01 8:00:00Z");
                    }
                    else
                    {
                        end_date = (DateTime)r[7];
                    }
                    if (r[8] == null)
                    {
                        inprogress = true;
                    }
                    else
                    {
                        //if ((String)r[8] == "Kesken")
                        //{
                        //    inprogress = true;
                        //}
                        //else if ((String)r[8] == "Valmis")
                        //{
                        //    inprogress = false;
                        //}
                        if (r[8] is bool)
                        {
                            inprogress = (bool) r[8];
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
            r.Close();
            c.Close();
            cmd.Dispose();
        }

        public static void AddOrUpdate(Topic topic)
        {
            using (CommittableTransaction tx = new CommittableTransaction())
            {
                try
                {
                    SqlCommand check_list = new SqlCommand("SELECT COUNT(*) FROM [TopicList] WHERE ([ID] = @ID)");
                    // Kytketään transaktio yhteyteen
                    c.Open();
                    c.EnlistTransaction(tx);
                    check_list.Connection = c;
                    // Suoritetaan komennot
                    int topicID = topic.ID;

                    check_list.Parameters.AddWithValue("@ID", topicID);
                    int TopicExists = (int)check_list.ExecuteScalar();
                    SqlCommand cmd = new SqlCommand();

                    if (TopicExists > 0)
                    {
                        cmd.Connection = c;
                        cmd.CommandText = "UPDATE TopicList SET ID = @ID, Title = @Title, Description = @Description, TimeEstimate = @TimeEstimate, TimeSpent = @TimeSpent, Source = @Source ,StartLearningDate = @StartDate, EndLearningDate = @End_date, InProgress = @Inprogress WHERE ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", topic.ID);
                        cmd.Parameters.AddWithValue("@Title", topic.Title);
                        cmd.Parameters.AddWithValue("@Description", topic.Description);
                        cmd.Parameters.AddWithValue("@TimeEstimate", topic.TimeEstimate);
                        cmd.Parameters.AddWithValue("@TimeSpent", topic.TimeSpent);
                        cmd.Parameters.AddWithValue("@Source", topic.Source);
                        cmd.Parameters.AddWithValue("@StartDate", topic.StartLearningDate);
                        cmd.Parameters.AddWithValue("@End_date", topic.CompletionDate);
                        cmd.Parameters.AddWithValue("@Inprogress", topic.InProgress);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.Connection = c;
                        cmd.CommandText = "INSERT INTO[dbo].[TopicList]([ID] ,[Title] ,[Description] ,[TimeEstimate] ,[TimeSpent] ,[Source] ,[StartLearningDate] ,[EndLearningDate] ,[InProgress]) VALUES (@ID, @Title, @Description, @TimeEstimate, @TimeSpent, @Source , @StartDate, @End_date, @Inprogress)";
                        cmd.Parameters.AddWithValue("@ID", topic.ID);
                        cmd.Parameters.AddWithValue("@Title", topic.Title);
                        cmd.Parameters.AddWithValue("@Description", topic.Description);
                        cmd.Parameters.AddWithValue("@TimeEstimate", topic.TimeEstimate);
                        cmd.Parameters.AddWithValue("@TimeSpent", topic.TimeSpent);
                        cmd.Parameters.AddWithValue("@Source", topic.Source);
                        cmd.Parameters.AddWithValue("@StartDate", topic.StartLearningDate);
                        cmd.Parameters.AddWithValue("@End_date", topic.CompletionDate);
                        cmd.Parameters.AddWithValue("@Inprogress", topic.InProgress);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    check_list.Dispose();
                    cmd.Dispose();
                }
                catch (Exception)
                {
                    // tai perutaan (esim. catch-lohkossa)
                    tx.Rollback();
                    Console.WriteLine("Error, rolled back all changes.");
                }
                c.Close();
            }
        }

        public static void LisääPäivitäTransaction(Topic topic)
        {
            c.Open();
            int topicID = topic.ID;

            SqlCommand check_product = new SqlCommand("SELECT COUNT(*) FROM [TopicList] WHERE ([ID] = @topic_id)", c);
            check_product.Parameters.AddWithValue("@topic_id", topicID);
            int UserExist = (int)check_product.ExecuteScalar();
            SqlCommand cmd = new SqlCommand();

            if (UserExist > 0)
            {
                cmd.Connection = c;
                cmd.CommandText = "UPDATE topic SET ID = @ID, Title = @Title, Description = @Description, TimeEstimate = @TimeEstimate, TimeSpent = @TimeSpent, Source = @Source ,StartLearningDate = @StartDate, CompletionDate = @End_date, InProgress = @Inprogress, WHERE ID = @topic_id";
                cmd.Parameters.AddWithValue("@ID", topic.ID);
                cmd.Parameters.AddWithValue("@Title", topic.Title);
                cmd.Parameters.AddWithValue("@Description", topic.Description);
                cmd.Parameters.AddWithValue("@TimeEstimate", topic.TimeEstimate);
                cmd.Parameters.AddWithValue("@TimeSpent", topic.TimeSpent);
                cmd.Parameters.AddWithValue("@Source", topic.Source);
                cmd.Parameters.AddWithValue("@StartDate", topic.StartLearningDate);
                cmd.Parameters.AddWithValue("@End_date", topic.CompletionDate);
                cmd.Parameters.AddWithValue("@Inprogress", topic.InProgress);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.Connection = c;
                cmd.CommandText = "INSERT INTO[dbo].[TopicList]([ID] ,[Title] ,[Description] ,[TimeEstimate] ,[TimeSpent] ,[Source] ,[StartLearningDate] ,[EndLearningDate] ,[InProgress]) VALUES (<ID, int,> ,<Title, varchar(250),> ,<Description, varchar(250),> ,<TimeEstimate, int,> ,<TimeSpent, int,> ,<Source, varchar(250),> ,<StartLearningDate, datetime,> ,<EndLearningDate, datetime,> ,<InProgress, bit,>)";
                cmd.Parameters.AddWithValue("@ID", topic.ID);
                cmd.Parameters.AddWithValue("@Title", topic.Title);
                cmd.Parameters.AddWithValue("@Description", topic.Description);
                cmd.Parameters.AddWithValue("@TimeEstimate", topic.TimeEstimate);
                cmd.Parameters.AddWithValue("@TimeSpent", topic.TimeSpent);
                cmd.Parameters.AddWithValue("@Source", topic.Source);
                cmd.Parameters.AddWithValue("@StartDate", topic.StartLearningDate);
                cmd.Parameters.AddWithValue("@End_date", topic.CompletionDate);
                cmd.Parameters.AddWithValue("@Inprogress", topic.InProgress);
                cmd.ExecuteNonQuery();
            }
            c.Close();
            check_product.Dispose();
            cmd.Dispose();
            return;
           

        }
    }
}
