using System;

namespace MostAwesomeDartApplicationEver.Models
{
    public class Round : MatchComponent 
    {
        public int? Id { get; set; }
        public Leg Leg { get; set; }
        public int Score { get; set; }
        public int DartsThrown { get; set; }
        public Darter Darter => Leg.Darter;
    }
}
