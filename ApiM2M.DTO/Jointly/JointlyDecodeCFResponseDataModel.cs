using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiM2M.DTO.Jointly
{
    public class JointlyDecodeCFResponseDataModel
    {
        public bool Success { get; set; }
        public UserData Data { get; set; }

        public class UserData
        {
            public string Codice { get; set; }
            public string Sesso { get; set; }
            public DateTime DataNascita { get; set; }
            public string CodiceLuogo { get; set; }
            public string NazioneNascita { get; set; }
            public string LuogoNascita { get; set; }
            public string MessaggioErrore { get; set; }
            public bool Valido { get; set; }
            public string AccessToken { get; set; }
        }
    }
}
