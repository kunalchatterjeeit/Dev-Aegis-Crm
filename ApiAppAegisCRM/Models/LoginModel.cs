using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAppAegisCRM.Models
{
    public class LoginModel:BaseModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LoginStatus { get; set; }
        public string Name { get; set; }
        public bool IsLoginBlocked { get; set; }
    }
}