using System;

namespace MostAwesomeDartApplicationEver.Models
{
    public class Set : MatchComponent 
    {
        public int? Id { get; set; }
        public Match Match { get; set; }
        public int Score { get; set; }
        public int DartsThrown { get; set; }
        public Darter Darter { get; set; }
    }
}
