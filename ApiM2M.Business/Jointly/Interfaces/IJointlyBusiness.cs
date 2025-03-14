using ApiM2M.DTO.Jointly;

namespace ApiM2M.Business.Jointly.Interfaces
{
    public interface IJointlyBusiness
    {
        Task<bool> CheckBeneficiario(string cf, string ca);
        Task<JointlyDecodeCFResponseDataModel> GetBeneficiario(string cf);
    }
}