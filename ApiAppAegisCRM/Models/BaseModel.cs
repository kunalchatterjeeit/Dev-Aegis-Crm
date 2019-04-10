using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAppAegisCRM.Models
{
    public class BaseModel
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public string DeviceId { get; set; }
    }
}