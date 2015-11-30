namespace WebflixApplication
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using WebflixApplication.Models;

    public partial class WebflixWarehouseContext : DbContext
    {
        public WebflixWarehouseContext()
            : base("name=Model1")
        {
        }

        public virtual DbSet<CLIENT_W> CLIENT_W { get; set; }
        public virtual DbSet<DATE_W> DATE_W { get; set; }
        public virtual DbSet<FILM_W> FILM_W { get; set; }
        public virtual DbSet<LOCATION_W> LOCATION_W { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CLIENT_W>()
                .Property(e => e.IDCLIENT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CLIENT_W>()
                .Property(e => e.NOM)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT_W>()
                .Property(e => e.PRENOM)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT_W>()
                .Property(e => e.GROUPEAGE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CLIENT_W>()
                .Property(e => e.CODEPOSTAL)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT_W>()
                .Property(e => e.VILLE)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT_W>()
                .Property(e => e.PROVINCE)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT_W>()
                .HasMany(e => e.LOCATION_W)
                .WithRequired(e => e.CLIENT_W)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DATE_W>()
                .Property(e => e.IDDATE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DATE_W>()
                .Property(e => e.HEURE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DATE_W>()
                .Property(e => e.JOUR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DATE_W>()
                .Property(e => e.MOIS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DATE_W>()
                .Property(e => e.ANNEE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DATE_W>()
                .HasMany(e => e.LOCATION_W)
                .WithRequired(e => e.DATE_W)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FILM_W>()
                .Property(e => e.IDFILM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILM_W>()
                .Property(e => e.CODE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILM_W>()
                .Property(e => e.TITRE)
                .IsUnicode(false);

            modelBuilder.Entity<FILM_W>()
                .Property(e => e.ANNEEPARUTION)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILM_W>()
                .Property(e => e.ORIGINE)
                .IsUnicode(false);

            modelBuilder.Entity<FILM_W>()
                .HasMany(e => e.LOCATION_W)
                .WithRequired(e => e.FILM_W)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LOCATION_W>()
                .Property(e => e.IDLOCATION)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOCATION_W>()
                .Property(e => e.IDFILM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOCATION_W>()
                .Property(e => e.IDCLIENT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOCATION_W>()
                .Property(e => e.IDDATE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOCATION_W>()
                .Property(e => e.GROUPEAGE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOCATION_W>()
                .Property(e => e.PROVINCE)
                .IsUnicode(false);

            modelBuilder.Entity<LOCATION_W>()
                .Property(e => e.JOUR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOCATION_W>()
                .Property(e => e.MOIS)
                .HasPrecision(38, 0);
        }
    }
}
