using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class City
    {
        public City()
        { }

        private int cityId = 0;
        private string cityName = string.Empty;
        private int districtId = 0;
        private int countryId = 0;
        private string sTD = string.Empty;
        private int stateId = 0;

        public int StateId
        {
            get { return stateId; }
            set { stateId = value; }
        }

        public int CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }

        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }

        public int DistrictId
        {
            get { return districtId; }
            set { districtId = value; }
        }

        public int CountryId
        {
            get { return countryId; }
            set { countryId = value; }
        }
        public string STD
        {
            get { return sTD; }
            set { sTD = value; }
        }

    }
}
