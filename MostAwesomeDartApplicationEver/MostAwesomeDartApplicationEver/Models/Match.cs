using System;

namespace MostAwesomeDartApplicationEver.Models
{
    public class Match : MatchComponent   
    {
        private DateTime _scheduledDateTime;

        private Set[] _sets;

        private Darter[] _darter;
        private MatchScore[] _matchScore;

    }
}
