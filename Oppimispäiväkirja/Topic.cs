using System;
using System.Collections.Generic;
using System.Text;


namespace Oppimispäiväkirja
{
    class Topic
    {
        #region fields and properties
        //Id int Tähän talletetaan kyseisen aiheen tunniste
        private readonly int id;
        private static int nextId = 1000;

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
        }
        public Topic(IEnumerable<Topic> select)
        {
            id = ++nextId;
        }

        public Topic(string title)
        {
            id = ++nextId;
            this.Title = title;
        }

        public Topic(string title, string description) : this(title)
        {
            this.Description = description;
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
