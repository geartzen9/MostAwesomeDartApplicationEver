using System;

namespace MostAwesomeDartApplicationEver.Models
{
    public class MatchScore : IScore 
    {
        public int Score { get; set; }
        public int DartsThrown { get; set; }


        public int CalculateScore() 
        {
            throw new System.NotImplementedException("Not implemented");
        }

        private Match _matchScore;

    }
}
