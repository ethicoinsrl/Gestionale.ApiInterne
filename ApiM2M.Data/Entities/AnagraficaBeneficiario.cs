using System.ComponentModel.DataAnnotations.Schema;

namespace ApiM2M.Data.Entities
{
    public partial class AnagraficaBeneficiario
    { 
        public AnagraficaBeneficiario()
        {
            /*AdesioniProdottiBeneficiari = new HashSet<AdesioneProdottoBeneficiario>();
            AdesioniRegolazioni = new HashSet<AdesioneRegolazione>();
            TracciatiEntiBilateraliDipendenti = new HashSet<TracciatoEnteBilateraleDipendente>();
            TracciatiEntiBilateraliCessazioni = new HashSet<TracciatoEnteBilateraleCessazione>();
            Pratiche = new HashSet<Pratica>();
            Prenotazioni = new HashSet<Prenotazione>();
            */
        }
        public int IdAnagraficaBeneficiario { get; set; }
        public int IdAdesioneContratto { get; set; }
        public int? IdTipoRegolaValidazione { get; set; }
        public int? IdNazioneNascita { get; set; }
        public int? IdTipoParentela { get; set; }
        public int? IdTipologia { get; set; }
        public int? IdTipoQualifica { get; set; }
        public int? IdAdesioneContrattoDipendente { get; set; }
        public int? IdNazioneCF { get; set; }
        public DateTime? DataNascita { get; set; }
        public DateTime? DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public DateTime? DataFineRapportoEnte { get; set; }
        public DateTime? DataAperturaRaccoltaDati { get; set; }
        public DateTime? DataComunicazione { get; set; }
        public DateTime? DataInserimento { get; set; }
        public DateTime? DataRicezioneEnte { get; set; }
        public bool FiscalmenteCarico { get; set; }
        public bool Convivente { get; set; }
        public bool Disabile { get; set; }
        public bool AnagraficaConfermata { get; set; }
        public bool Privacy { get; set; }
        public bool AutorizzazioneAddebito { get; set; }
        public bool BeneficiarioAssociato { get; set; }
        public bool QuestionarioConsegnato { get; set; }
        public string CodiceNucleo { get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public string? CodiceFiscale { get; set; }
        public string? LuogoNascita { get; set; }
        public string? DescrizioneMalattiePregresse { get; set; }
        public string? Cellulare { get; set; }
        public string? IntestatarioCC { get; set; }
        public string? OrarioContatto { get; set; }
        public string? Cap { get; set; }
        public string? Provincia { get; set; }
        public string? Iban { get; set; }
        public string? Telefono { get; set; }
        public string? Qualifica { get; set; }
        public string? UserName { get; set; }
        public string? Professione { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
        public string? Via { get; set; }
        public string? NumeroCivico { get; set; }
        public string? Localita { get; set; }

        [NotMapped]
        public int IdAnagraficaBeneficiarioOld { get; set; }
        /*
        public virtual AdesioneContratto AdesioneContratto { get; set; }
        public virtual TipoRegolaValidazione TipoRegolaValidazione { get; set; }
        public virtual Paese Paese { get; set; }
        public virtual TipoParentela TipoParentela { get; set; }
        public virtual Tipologia Tipologia { get; set; }
        public virtual TipoQualifica TipoQualifica { get; set; }
        public virtual AdesioneContratto AdesioneContrattoDipendente { get; set; }
        public virtual ICollection<AdesioneProdottoBeneficiario> AdesioniProdottiBeneficiari { get; set; }
        public virtual ICollection<AdesioneRegolazione> AdesioniRegolazioni { get; set; }
        public virtual ICollection<Pratica> Pratiche { get; set; }
        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
        public virtual ICollection<TracciatoEnteBilateraleCessazione> TracciatiEntiBilateraliCessazioni { get; set; }
        public virtual ICollection<TracciatoEnteBilateraleDipendente> TracciatiEntiBilateraliDipendenti { get; set; }
        */
    }
}
