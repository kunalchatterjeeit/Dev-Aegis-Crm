using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Common
{
    public class Constants
    {
        public Constants()
        { }

        public enum Customer { 
            ID = 0,
            Name = 1
        }

        public enum CustomerType
        {
            None = 0,
            APlus = 1,
            A = 2,
            B = 3
        }

        public static string NoImgaeUrl
        {
            get
            {
                return "~/images/not-available.png";
            }
        }
    }
}
