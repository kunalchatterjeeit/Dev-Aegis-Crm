namespace ApiAppAegisCRM.Models
{
    public class LoyalityPointModel : BaseModel
    {
        public LoyalityPointModel() { }
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal Point { get; set; }
        public string Reason { get; set; }
    }
}