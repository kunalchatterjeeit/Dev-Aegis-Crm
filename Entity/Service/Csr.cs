namespace Entity.Service
{
    public class Csr
    {
        public Csr(){ }
        public long CsrId { get; set; }
        public long ServiceBookId { get; set; }
        public string CsrContent { get; set; }
        public int CreatedBy { get; set; }

    }
}
