using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiM2M.DTO.Jointly
{
    public class JointlyPurchaseRequestDataModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string? company_location_id { get; set; }
        public string provider { get; set; }
        public string user_typology { get; set; }
        public string badge_number { get; set; }
        public string cid { get; set; }
        public string cf { get; set; }
        public string filler1 { get; set; }
        public string filler2 { get; set; }
        public string filler3 { get; set; }
        public string last_login { get; set; }
        public int? newsletter { get; set; } // Nullable perché può essere null nel JSON
        public bool wallet_enabled { get; set; }
        public string location_name { get; set; }
        public string? token { get; set; }
        public string privacyAccepted { get; set; }
        public string birth_date { get; set; }
        public string birth_city { get; set; }
        public string sex { get; set; }
        public string ca { get; set; }
        public string cp { get; set; }
    }
}
