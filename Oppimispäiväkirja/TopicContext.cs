using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Oppimispäiväkirja
{
    public class TopicContext : DbContext
    {
        public TopicContext() : base()
        {

        }
        public DbSet<Topic> Topics { get; set; }


    }
}
