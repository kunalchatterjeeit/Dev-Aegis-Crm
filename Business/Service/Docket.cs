using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Business.Service
{
    public class Docket
    {
        public Docket()
        { }

        public DataTable Service_Docket_Save(Entity.Service.Docket docket)
        {
            return DataAccess.Service.Docket.Service_Docket_Save(docket);
        }

        public DataTable Service_Docket_GetAll(Entity.Service.Docket docket)
        {
            return DataAccess.Service.Docket.Service_Docket_GetAll(docket);
        }

        public DataTable Service_Docket_GetLast10()
        {
            return DataAccess.Service.Docket.Service_Docket_GetLast10();
        }

        public Entity.Service.Docket Service_Docket_GetByDocketId(int docketid)
        {
            return DataAccess.Service.Docket.Service_Docket_GetByDocketId(docketid);
        }

        public int Service_Docket_Delete(int docketid)
        {
            return DataAccess.Service.Docket.Service_Docket_Delete(docketid);
        }
    }
}
