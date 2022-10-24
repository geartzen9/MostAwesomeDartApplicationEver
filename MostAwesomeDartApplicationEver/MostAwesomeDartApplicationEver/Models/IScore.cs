using System;

namespace MostAwesomeDartApplicationEver.Models
{
    public interface IScore
    {
        int Score { get; set; }
        int DartsThrown { get; set; }
        
        int CalculateScore();
    }
}
