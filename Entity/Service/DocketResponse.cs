namespace Entity.Service
{
    public class DocketResponse
    {
        public long DocketId { get; set; }
        public string ShortDocketDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string CallStatus { get; set; }
        public int CallStatusId { get; set; }
        public string ContactPerson { get; set; }
        public string AssignedEngineerName { get; set; }
    }
}
