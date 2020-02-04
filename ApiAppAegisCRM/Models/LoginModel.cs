namespace ApiAppAegisCRM.Models
{
    public class LoginModel : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}