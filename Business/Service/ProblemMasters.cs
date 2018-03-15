using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.Service
{
    public class ProblemMasters
    {
        public ProblemMasters()
        { }

        public int Save(Entity.Service.ProblemMasters problemObserved)
        {
            return DataAccess.Service.ProblemMasters.Save(problemObserved);
        }

        public DataTable GetAll(int type)
        {
            return DataAccess.Service.ProblemMasters.GetAll(type);
        }

        public Entity.Service.ProblemMasters GetById(int id)
        {
            return DataAccess.Service.ProblemMasters.GetById(id);
        }

        public int Delete(int id)
        {
            return DataAccess.Service.ProblemMasters.Delete(id);
        }
    }
}
