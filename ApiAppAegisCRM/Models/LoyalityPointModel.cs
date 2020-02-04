namespace ApiAppAegisCRM.Models
{
    public class LoyalityPointModel : BaseModel
    {
        public LoyalityPointModel() { }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Point { get; set; }
        public string Reason { get; set; }
    }
}