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

        public class Serializable
        {
            public Set[] Sets { get; set; }
            public Darter[] Darters { get; set; }
            public MatchScore[] MatchScores { get; set; }
        }

        public static explicit operator Serializable(Match match)
        {
            return new Serializable()
            {
                Sets = match._sets,
                Darters = match._darter,
                MatchScores = match._matchScore
            };
        }
    }
}
