using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class GetSocialMediaParam
    {
        public int LinkTypeId { get; set; }
        public int LinkId { get; set; }
    }
    public class GetSocialMedia
    {
        public int SocialMediaId { get; set; }
        public string SocialMediaName { get; set; }
        public int? SocialMediaMappingId { get; set; }
        public int? LinkId { get; set; }
        public int? LinkTypeId { get; set; }
        public string UrlLink { get; set; }
        public string Description { get; set; }
    }
    public class SocialMedia
    {
        public int Id { get; set; }
        public int SocialMediaId { get; set; }
        public int LinkTypeId { get; set; }
        public int LinkId { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
    }
}
