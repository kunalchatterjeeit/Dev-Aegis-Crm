
namespace Entity.Common
{
    public class Auth
    {
        public int UserId { get; set; }
        public string IP { get; set; }
        public LoginStatus Status { get; set; }
        public string Client { get; set; }
        public string FailedUserName { get; set; }
        public string FailedPassword { get; set; }
        public string Source { get; set; }
    }
}
