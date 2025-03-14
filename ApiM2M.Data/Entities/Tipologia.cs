namespace ApiM2M.Data.Entities
{
    public class Tipologia
    {
        public Tipologia()
        {
            //AnagraficheBeneficiari = new HashSet<AnagraficaBeneficiario>();
            //AnagraficheAssociati = new HashSet<AnagraficaAssociato>();
            //AssociatiUfficializzati = new HashSet<AssociatoUfficializzato>();
            //AnagraficheSoggetti = new HashSet<AnagraficaSoggetto>();
            //PromotoriRappresentantiLegali = new HashSet<PromotoreRappresentanteLegale>();
        }
        public int IdTipologia { get; set; }
        public string Descrizione { get; set; }
        //public virtual ICollection<AnagraficaBeneficiario> AnagraficheBeneficiari { get; set; }
        //public virtual ICollection<AnagraficaAssociato> AnagraficheAssociati { get; set; }
        //public virtual ICollection<AssociatoUfficializzato> AssociatiUfficializzati { get; set; }
        //public virtual ICollection<AnagraficaSoggetto> AnagraficheSoggetti { get; set; }
        //public virtual ICollection<PromotoreRappresentanteLegale> PromotoriRappresentantiLegali { get; set; }
    }
}
