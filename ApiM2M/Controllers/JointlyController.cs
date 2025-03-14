using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiM2M.Business.Jointly.Interfaces;
using ApiM2M.DTO.Jointly;
using ApiM2M.Helpers;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TestApiM2M.Controllers
{
    [ApiController]
    [Route("Jointly")]
    [Authorize]
    public class JointlyController : Controller
    {
        private readonly IJointlyBusiness _jointlyBusiness;
        
        public JointlyController(IJointlyBusiness jointlyBusiness)
        {
            _jointlyBusiness = jointlyBusiness;
        }

        [Route("checkBeneficiario")]
        [HttpPost]
        public async Task<IActionResult> checkBeneficiario(JointlyCheckBeneficiarioRequestDataModel model)
        {
            try
            {
                var result = await _jointlyBusiness.CheckBeneficiario(model.Cf, model.Ca);
                var response = new JointlyCheckBeneficiarioResponseDataModel()
                {
                    success = true,
                    data = new JointlyCheckBeneficiarioResponseDataModel.DataStruct()
                    {
                        exists = result,
                        ricarica = false,
                        access_token = new Helper().createAccessToken(model.Cf)
                    }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Errore durante la verifica del beneficiario");
            }
            return StatusCode(500, "Errore durante la verifica del beneficiario");
        }

        [Route("decodeCodiceFiscale")]
        [HttpPost]
        public async Task<IActionResult> decodeCodiceFiscale(JointlyDecodeCFRequestDataModel model)
        {
            try
            {
                var response = await _jointlyBusiness.GetBeneficiario(model.Cf);
                response.Data.AccessToken = new Helper().createAccessToken(model.Cf);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Errore durante il reperimento del beneficiario");
            }

            return StatusCode(500, "Errore durante il reperimento del beneficiario");
        }

        [Route("insertBeneficiario")]
        [HttpPost]
        public IActionResult insertBeneficiario(JointlyPurchaseRequestDataModel model)
        {
            /*
             * Inserimento del beneficiario se inesistente
             * Aggiornamento dei dati del beneficiario se esistente
             * Inserimento dell'acquisto effettuato dal portale jointly
             */
            return Ok(MockResponse(model));
        }


        private JointlyPurchaseResponseDataModel MockResponse(JointlyPurchaseRequestDataModel model)
        {
            return new JointlyPurchaseResponseDataModel()
            {
                success = true,
                data = new JointlyPurchaseResponseDataModel.Data()
                {
                    id = 0,
                    codiceFiscaleBeneficiario = model.cf,
                    codiceProdotto = model.cp,
                    codiceFrazionamento = "",
                    prezzo = 0,
                    numeroPezzi = 0,
                    data = DateTime.Now,
                }
            };
        }
        
    }
}
