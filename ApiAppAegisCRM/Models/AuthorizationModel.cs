using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAppAegisCRM.Models
{
    public class AuthorizationModel: BaseModel
    {
        public string UtilityCode { get; set; }
        public bool ReturnValue { get; set; }
    }
}