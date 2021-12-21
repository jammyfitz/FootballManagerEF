namespace FootballManagerEF.Models
{
    public class PlayerCalculationWithScore
    {
        public PlayerMatch PlayerMatch { get; set; }

        public PlayerCalculation PlayerCalculation { get; set; }

        public decimal Score { get; set; }
    }
}
