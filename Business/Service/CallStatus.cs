using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.Service
{
    public class CallStatus
    {
        public CallStatus()
        { }

        public int Save(Entity.Service.CallStatus callStatus)
        {
            return DataAccess.Service.CallStatus.Save(callStatus);
        }

        public DataTable GetAll(int calltype)
        {
            return DataAccess.Service.CallStatus.GetAll(calltype);
        }

        public Entity.Service.CallStatus GetById(int callStatusId)
        {
            return DataAccess.Service.CallStatus.GetById(callStatusId);
        }
    }
}
