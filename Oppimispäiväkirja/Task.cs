using System;
using System.Collections.Generic;
using System.Text;

namespace Oppimispäiväkirja
{
    class Task
    {
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
        //Notes List<string> Muistiinpanoja liittyen tehtävään
        private List<string> notes;
        public List<string> Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        //Deadline datetime Milloin tulisi olla valmis
        private DateTime deadline;
        public DateTime Deadline
        {
            get { return deadline; }
            set { deadline = value; }
        }        
        //Priority enum Kuinka kiireinen kyseinen tehtävä on
        public enum Priority
        {
            Low = 0,
            Medium = 1,
            High = 2,
        }
        //Done bool Onko tehtävä valmis
        private bool done;
        public bool Done
        {
            get { return done; }
            set { done = value; }
        }
        public Task()
        {
            id = ++nextId;
        }
        public Task(string title)
        {
            this.Title = title;
        }
    }
}
