using System;
using System.Collections.Generic;


namespace Oppimispäiväkirja
{
    public class Topic
    {
        #region fields and properties
        //Id int Tähän talletetaan kyseisen aiheen tunniste
        private readonly int id;
        private static int nextId = 1;

        public int ID { get { return id; } }

        //Title string Aiheen otsikko
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        //Description string Aiheen kuvaus
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        //EstimatedTimeToMaster double Aika arvio, kuinka kauan aiheen oppimiseen kuluu aikaa
        private double timeEstimate;
        public double TimeEstimate
        {
            get { return timeEstimate; }
            set { timeEstimate = value; }
        }
        //TimeSpent double Käytetty aika
        private double timeSpent;
        public double TimeSpent
        {
            get { return timeSpent; }
            set { timeSpent = value; }
        }
        //Source string Mahdollinen lähde esim web url tai kirja
        private string source;
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        //StartLearningDate datetime Aloitus aika
        private DateTime startLearningDate;
        public DateTime StartLearningDate
        {
            get { return startLearningDate; }
            set { startLearningDate = value; }
        }
        //InProgress bool Onko aiheen opiskelu kesken
        private bool inProgress;
        public bool InProgress
        {
            get { return inProgress; }
            set { inProgress = value; }
        }
        //CompletionDate Milloin aihe on opiskeltu
        private DateTime completionDate;
        public DateTime CompletionDate
        {
            get { return completionDate; }
            set { completionDate = value; }
        }
        public Task Task { get; }
        #endregion

        #region constructors
        public Topic()
        {
            id = ++nextId;
            this.StartLearningDate = DateTime.Now;
            this.CompletionDate = DateTime.Now; 
            this.Description = "default";
            this.TimeEstimate = 00.00d;
            this.TimeSpent = 00.00d;
            this.Source = "whatever";
            this.InProgress = true;
        }
        public Topic(string title) : this()
        {
            this.Title = title;
        }

        public Topic(string title, string description, double timeestimate, double timespent, string source, DateTime startdate, DateTime end_date, bool inprogress) : this(title)
        {
            this.Title = title;
            this.Description = description;
            this.TimeEstimate = timeestimate;
            this.TimeSpent = timespent;
            this.Source = source;
            this.StartLearningDate = startdate;
            this.CompletionDate = end_date;
            this.InProgress = inprogress;
        }
        #endregion
    }
}
