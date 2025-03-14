using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ApiM2M.Data.Entities;

namespace ApiM2M.Data.EntitiesMap
{
    public class AnagraficaBeneficiarioMap : IEntityTypeConfiguration<AnagraficaBeneficiario>
    { 
        public void Configure(EntityTypeBuilder<AnagraficaBeneficiario> entity)
        {
            entity.ToTable("AnagraficheBeneficiari");
            entity.HasKey(t => t.IdAnagraficaBeneficiario);

            entity.Property(t => t.IdAnagraficaBeneficiario).HasColumnName("IdAnagraficaBeneficiario");
            entity.Property(t => t.IdAdesioneContratto).HasColumnName("IdAdesioneContratto");
            entity.Property(t => t.IdTipoRegolaValidazione).HasColumnName("IdTipoRegolaValidazione");
            entity.Property(t => t.IdNazioneNascita).HasColumnName("IdNazioneNascita");
            entity.Property(t => t.IdTipoParentela).HasColumnName("IdTipoParentela");
            entity.Property(t => t.IdTipologia).HasColumnName("IdTipologia");
            entity.Property(t => t.IdTipoQualifica).HasColumnName("IdTipoQualifica");
            entity.Property(t => t.IdAdesioneContrattoDipendente).HasColumnName("IdAdesioneContrattoDipendente");
            entity.Property(t => t.IdNazioneCF).HasColumnName("IdNazioneCF");
            entity.Property(t => t.DataNascita).HasColumnName("DataNascita").HasColumnType("datetime");
            entity.Property(t => t.DataInizio).HasColumnName("DataInizio").HasColumnType("datetime");
            entity.Property(t => t.DataFine).HasColumnName("DataFine").HasColumnType("datetime");
            entity.Property(t => t.DataFineRapportoEnte).HasColumnName("DataFineRapportoEnte").HasColumnType("datetime");
            entity.Property(t => t.DataAperturaRaccoltaDati).HasColumnName("DataAperturaRaccoltaDati").HasColumnType("datetime");
            entity.Property(t => t.DataComunicazione).HasColumnName("DataComunicazione").HasColumnType("datetime");
            entity.Property(t => t.DataInserimento).HasColumnName("DataInserimento").HasColumnType("datetime");
            entity.Property(t => t.DataRicezioneEnte).HasColumnName("DataRicezioneEnte").HasColumnType("datetime");
            entity.Property(t => t.FiscalmenteCarico).HasColumnName("FiscalmenteCarico");
            entity.Property(t => t.Convivente).HasColumnName("Convivente");
            entity.Property(t => t.Disabile).HasColumnName("Disabile");
            entity.Property(t => t.AnagraficaConfermata).HasColumnName("AnagraficaConfermata");
            entity.Property(t => t.Privacy).HasColumnName("Privacy");
            entity.Property(t => t.AutorizzazioneAddebito).HasColumnName("AutorizzazioneAddebito");
            entity.Property(t => t.BeneficiarioAssociato).HasColumnName("BeneficiarioAssociato");
            entity.Property(t => t.QuestionarioConsegnato).HasColumnName("QuestionarioConsegnato");
            entity.Property(t => t.CodiceNucleo).HasColumnName("CodiceNucleo");
            entity.Property(t => t.Nome).HasColumnName("Nome");
            entity.Property(t => t.Cognome).HasColumnName("Cognome");
            entity.Property(t => t.CodiceFiscale).HasColumnName("CodiceFiscale");
            entity.Property(t => t.LuogoNascita).HasColumnName("LuogoNascita");
            entity.Property(t => t.DescrizioneMalattiePregresse).HasColumnName("DescrizioneMalattiePregresse");
            entity.Property(t => t.Cellulare).HasColumnName("Cellulare");
            entity.Property(t => t.IntestatarioCC).HasColumnName("IntestatarioCC");
            entity.Property(t => t.OrarioContatto).HasColumnName("OrarioContatto");
            entity.Property(t => t.Cap).HasColumnName("Cap");
            entity.Property(t => t.Provincia).HasColumnName("Provincia");
            entity.Property(t => t.Iban).HasColumnName("Iban");
            entity.Property(t => t.Telefono).HasColumnName("Telefono");
            entity.Property(t => t.Qualifica).HasColumnName("Qualifica");
            entity.Property(t => t.UserName).HasColumnName("UserName");
            entity.Property(t => t.Professione).HasColumnName("Professione");
            entity.Property(t => t.Email).HasColumnName("Email");
            entity.Property(t => t.Note).HasColumnName("Note");
            entity.Property(t => t.Via).HasColumnName("Via");
            entity.Property(t => t.NumeroCivico).HasColumnName("NumeroCivico");
            entity.Property(t => t.Localita).HasColumnName("Localita");

            /*
            entity.HasOne(t => t.AdesioneContratto)
                .WithMany(t => t.AnagraficheBeneficiari)
                .HasForeignKey(d => d.IdAdesioneContratto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AnagraficheBeneficiari_AdesioneContratto");

            entity.HasOne(t => t.TipoRegolaValidazione)
                .WithMany(t => t.AnagraficheBeneficiari)
                .HasForeignKey(d => d.IdTipoRegolaValidazione)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AnagraficheBeneficiari_TipoRegolaValidazione");

            entity.HasOne(t => t.Paese)
                .WithMany(t => t.AnagraficheBeneficiari)
                .HasForeignKey(d => d.IdNazioneNascita)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AnagraficheBeneficiari_Paese");

            entity.HasOne(t => t.TipoParentela)
                .WithMany(t => t.AnagraficheBeneficiari)
                .HasForeignKey(d => d.IdTipoParentela)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AnagraficheBeneficiari_TipoParentela");

            entity.HasOne(t => t.Tipologia)
                .WithMany(t => t.AnagraficheBeneficiari)
                .HasForeignKey(d => d.IdTipologia)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AnagraficheBeneficiari_Tipologia");

            entity.HasOne(t => t.TipoQualifica)
                .WithMany(t => t.AnagraficheBeneficiari)
                .HasForeignKey(d => d.IdTipoQualifica)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AnagraficheBeneficiari_TipoQualifica");

            entity.HasOne(t => t.AdesioneContrattoDipendente)
                .WithMany(t => t.AnagraficheBeneficiariDipendenti)
                .HasForeignKey(d => d.IdAdesioneContrattoDipendente)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AnagraficheBeneficiari_AdesioneContrattoDip");

            */
        }
    }
}