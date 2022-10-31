using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;

namespace MostAwesomeDartApplicationEver.Models
{
    public class Match : MatchComponent   
    {
        public int? Id { get; set; }

        public DateTime ScheduledDateTime { get; set; }

        public IEnumerable<Darter> Darters { get; set; }
    }
}
