using System;

namespace MostAwesomeDartApplicationEver.Models
{
    public class Leg : MatchComponent 
    {
        public int? Id { get; set; }
        public Darter Darter => Set.Darter;
        public Set Set { get; set; }
        public int Score { get; set; }
        public int DartsThrown { get; set; }
    }
}
