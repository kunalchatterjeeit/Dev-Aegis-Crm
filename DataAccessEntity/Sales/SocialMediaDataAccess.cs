using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class SocialMediaDataAccess
    {
        public static List<GetSocialMediaDbModel> GetAllSocialMedia(GetSocialMediaParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetSocialMediaDbModel>(
                                "exec dbo.[usp_Sales_SocialMedia_GetAll] @LinkTypeId,@LinkId",
                                new Object[]
                                {
                                    new SqlParameter("LinkTypeId",Param.LinkTypeId),
                                    new SqlParameter("LinkId", Param.LinkId)
                                }
                             ).ToList();
            }
        }
        public static int SaveSocialMedia(SocialMediaDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_SocialMedia_Save] @Id,@SocialMediaId,@LinkTypeId,@LinkId,@URL,@Description",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("SocialMediaId", Model.SocialMediaId),
                                    new SqlParameter("LinkTypeId", Model.LinkTypeId),
                                    new SqlParameter("LinkId", Model.LinkId),
                                    new SqlParameter("URL", Model.URL),
                                    new SqlParameter("Description", Model.Description)
                                }
                             );
            }
        }
    }
}
