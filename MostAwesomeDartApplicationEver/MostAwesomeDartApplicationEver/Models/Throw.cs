namespace MostAwesomeDartApplicationEver.Models
{
    public class Throw
    {
        public int Id { get; set; }
        public Darter Darter => Round.Darter;
        public Round Round { get; set; }
        public int Score { get; set; }
        public (HitArea, int) Hit { get; set; }
    }
}
