using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Business.Common
{
    public class City
    {
        public City()
        {
        }

        public int Save(Entity.Common.City city)
        {
            return DataAccess.Common.City.Save(city);
        }
        public DataTable GetAll(Entity.Common.City city)
        {
            return DataAccess.Common.City.GetAll(city);
        }
        public Entity.Common.City GetById(int cityid)
        {
            return DataAccess.Common.City.GetById(cityid);
        }
        public int Delete(int cityid)
        {
            return DataAccess.Common.City.Delete(cityid);
        }
    }
}
