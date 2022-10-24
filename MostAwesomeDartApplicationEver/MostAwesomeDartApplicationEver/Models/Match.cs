using System;

namespace MostAwesomeDartApplicationEver.Models
{
    public class Match : MatchComponent   
    {
        public int? Id { get; set; }

        private DateTime _scheduledDateTime;

        private Set[] _sets;

        private Darter[] _darter;
        private MatchScore[] _matchScore;

    }
}
