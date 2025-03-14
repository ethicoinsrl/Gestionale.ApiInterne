using System.Reflection;
using ApiM2M.Business.Jointly.Interfaces;
using ApiM2M.Data.Context;
using ApiM2M.DTO.Jointly;
using Microsoft.EntityFrameworkCore;
using PhoneNumbers;

namespace ApiM2M.Business.Jointly.Business
{
    public class JointlyBusiness : IJointlyBusiness
    {
        private readonly HealthHubContext _context;
        public JointlyBusiness(HealthHubContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckBeneficiario(string cf, string ca)
        {
            int.TryParse(ca, out int idCa);
            var result = await _context.AnagraficheBeneficiari.FirstOrDefaultAsync(x => x.CodiceFiscale == cf && x.IdAdesioneContratto == idCa);

            if (result == null) { return false; }
            if (string.IsNullOrEmpty(result.Telefono) && string.IsNullOrEmpty(result.Cellulare)) { return false; }
            var phoneNumber = NormalizeItalianMobilePhone(result.Telefono ?? "", result.Cellulare ?? "");
            if (!isValidPhoneNumber(phoneNumber)) { return false; }

            return true;
        }

        public async Task<JointlyDecodeCFResponseDataModel> GetBeneficiario(string cf)
        {
            var result = await _context.AnagraficheBeneficiari.FirstOrDefaultAsync(x => x.CodiceFiscale == cf);
            if (result == null)
            {
                return new JointlyDecodeCFResponseDataModel() { Success = false, Data = new JointlyDecodeCFResponseDataModel.UserData() };
            }

            var tipologia = await _context.Tipologia.FirstOrDefaultAsync(x => x.IdTipologia == result.IdTipologia);
            string sesso = tipologia == null ? "" : tipologia.Descrizione ?? "";

            return new JointlyDecodeCFResponseDataModel()
            {
                Success = true,
                Data = new JointlyDecodeCFResponseDataModel.UserData()
                {
                    Codice = result.IdAnagraficaBeneficiario.ToString(),
                    Sesso = sesso,
                    DataNascita = result.DataNascita ?? DateTime.MinValue,
                    CodiceLuogo = result.Localita ?? "",
                    NazioneNascita = result.IdNazioneNascita.ToString() ?? "",
                    LuogoNascita = result.LuogoNascita ?? "",
                    MessaggioErrore = "",
                    Valido = true,
                    AccessToken = ""
                }
            };

        }

        #region Private_Methods
        private bool isValidPhoneNumber(string phone)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            if (string.IsNullOrEmpty(phone)) return false;

            PhoneNumber numberMobile = phoneNumberUtil.Parse(phone, "IT");
            if (phoneNumberUtil.IsValidNumber(numberMobile))
            {
                string normalizedNumber = phoneNumberUtil.Format(numberMobile, PhoneNumberFormat.INTERNATIONAL);
                PhoneNumberType type = phoneNumberUtil.GetNumberType(numberMobile);
                if (type == PhoneNumberType.MOBILE) { return true; }
            }

            return false;
        }
        private string NormalizeItalianMobilePhone(string phone, string mobile)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();

            if (!string.IsNullOrEmpty(mobile))
            {
                PhoneNumber numberMobile = phoneNumberUtil.Parse(mobile, "IT");
                if (phoneNumberUtil.IsValidNumber(numberMobile))
                {
                    string normalizedNumber = phoneNumberUtil.Format(numberMobile, PhoneNumberFormat.INTERNATIONAL);
                    PhoneNumberType type = phoneNumberUtil.GetNumberType(numberMobile);
                    if (type == PhoneNumberType.MOBILE)
                    {
                        return normalizedNumber;
                    }
                }
            }

            if (!string.IsNullOrEmpty(phone))
            {
                PhoneNumber numberPhone = phoneNumberUtil.Parse(phone, "IT");
                if (phoneNumberUtil.IsValidNumber(numberPhone))
                {
                    string normalizedNumber = phoneNumberUtil.Format(numberPhone, PhoneNumberFormat.INTERNATIONAL);
                    PhoneNumberType type = phoneNumberUtil.GetNumberType(numberPhone);
                    if (type == PhoneNumberType.MOBILE)
                    {
                        return normalizedNumber;
                    }
                }
            }

            return string.IsNullOrEmpty(mobile) ? phone : mobile;
        }
        #endregion
    }
}
