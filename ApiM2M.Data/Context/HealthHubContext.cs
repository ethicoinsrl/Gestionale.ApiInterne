using ApiM2M.Data.Entities;
using ApiM2M.Data.EntitiesMap;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.AspNetCore.Http;

namespace ApiM2M.Data.Context
{
    public class HealthHubContext : DbContext
    {
        private IEnumerable<Claim> _claim;
        public HealthHubContext(DbContextOptions<HealthHubContext> options)
            : base(options)
        {
        }

        #region DbSet        
        public virtual DbSet<AnagraficaBeneficiario> AnagraficheBeneficiari { get; set; }
        public virtual DbSet<Tipologia> Tipologia { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new InvalidOperationException("DBContext non configurato");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnagraficaBeneficiarioMap());
            modelBuilder.ApplyConfiguration(new TipologiaMap());
            
            base.OnModelCreating(modelBuilder);
        }

        public void ReconfigureModel(IEnumerable<Claim> claim)
        {
            //_custom_Mutua_Mediata = custom_Mutua_Mediata;
            _claim = claim;
            var modelBuilder = new ModelBuilder(new ConventionSet());

            OnModelCreating(modelBuilder);
            // Apply the new model configuration to the context
        }
    }
}
