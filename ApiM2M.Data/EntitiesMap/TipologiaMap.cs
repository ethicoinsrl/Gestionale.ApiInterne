using ApiM2M.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiM2M.Data.EntitiesMap
{
    public class TipologiaMap : IEntityTypeConfiguration<Tipologia>
    {
        public void Configure(EntityTypeBuilder<Tipologia> entity)
        {
            entity.ToTable("Tipologie");
            entity.HasKey(e => e.IdTipologia).HasName("PK_IdTipologia");

            entity.Property(e => e.Descrizione).IsUnicode(false);
        }
    }
}