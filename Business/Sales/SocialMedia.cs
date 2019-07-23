using Business.Common;
using DataAccessEntity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Sales
{
    public class SocialMedia
    {
        public List<Entity.Sales.GetSocialMedia> GetAllSocialMedia(Entity.Sales.GetSocialMediaParam Param)
        {
            List<Entity.Sales.GetSocialMedia> AllSocialMediaList = new List<Entity.Sales.GetSocialMedia>();
            GetSocialMediaParamDbModel p = new GetSocialMediaParamDbModel();
            Param.CopyPropertiesTo(p);
            SocialMediaDataAccess.GetAllSocialMedia(p).CopyListTo(AllSocialMediaList);
            return AllSocialMediaList;
        }
        public int SaveSocialMedia(Entity.Sales.SocialMedia Model)
        {
            SocialMediaDbModel DbModel = new SocialMediaDbModel();
            Model.CopyPropertiesTo(DbModel);
            return SocialMediaDataAccess.SaveSocialMedia(DbModel);
        }
    }
}
