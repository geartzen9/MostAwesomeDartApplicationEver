namespace MostAwesomeDartApplicationEver.Models
{
    public class Throw
    {
        public int Id { get; set; }
        public Darter Darter => Round.Darter;
        public Round Round { get; set; }
        public int Score { get; set; }
        public Hit Hit { get; set; }
    }
}
