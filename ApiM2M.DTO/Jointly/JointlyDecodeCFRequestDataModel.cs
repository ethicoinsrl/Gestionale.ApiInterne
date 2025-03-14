namespace ApiM2M.DTO.Jointly
{
    public class JointlyDecodeCFRequestDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string? CompanyLocationId { get; set; }
        public string? Provider { get; set; }
        public string UserTypology { get; set; }
        public string BadgeNumber { get; set; }
        public string Cid { get; set; }
        public string Cf { get; set; }
        public string Filler1 { get; set; }
        public string Filler2 { get; set; }
        public string Filler3 { get; set; }
        public DateTime LastLogin { get; set; }
        public int Newsletter { get; set; }
        public bool WalletEnabled { get; set; }
        public string LocationName { get; set; }
        public string Token { get; set; }
    }
}
