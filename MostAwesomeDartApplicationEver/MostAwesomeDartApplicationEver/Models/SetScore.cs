using System;

namespace MostAwesomeDartApplicationEver.Models
{
    public class SetScore : IScore  
    {
        public int Score { get; set; }
        public int DartsThrown { get; set; }

        public int CalculateScore()
        {
            throw new System.NotImplementedException("Not implemented");
        }

        private Set _setScore;

        private Darter _darter;

    }
}
