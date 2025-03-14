using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiM2M.DTO.Jointly
{
    public class JointlyCheckBeneficiarioResponseDataModel
    {
        public bool success { get; set; }
        public DataStruct data { get; set; }

        public class DataStruct
        {
            public bool exists { get; set; }
            public bool ricarica { get; set; }
            public string access_token { get; set; }
        }
    }
}
