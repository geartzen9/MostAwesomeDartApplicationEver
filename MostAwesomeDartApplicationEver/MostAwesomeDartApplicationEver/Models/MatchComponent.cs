namespace MostAwesomeDartApplicationEver.Models
{
    public abstract class MatchComponent
    {
        public Darter Winner { get; set; }
        public MatchComponentStatus Status { get; set; }
    }
}
