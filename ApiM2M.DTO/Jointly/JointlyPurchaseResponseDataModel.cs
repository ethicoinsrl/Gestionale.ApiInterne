using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiM2M.DTO.Jointly
{
    public class JointlyPurchaseResponseDataModel
    {
        public bool success { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public int id { get; set; }
            public string codiceFiscaleBeneficiario { get; set; }
            public string codiceProdotto { get; set; }
            public string codiceFrazionamento { get; set; }
            public decimal prezzo { get; set; }
            public int numeroPezzi { get; set; }
            public DateTime data { get; set; }
        }
    }
}
