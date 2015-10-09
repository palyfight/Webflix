namespace WebflixApplication
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using WebflixApplication.Models;
    using System.Data;
    using Oracle.ManagedDataAccess.Client;
    using System.Collections.Generic;

    public partial class WebflixContext : DbContext
    {
        public WebflixContext()
            : base("name=WebflixContext")
        {
        }

        public virtual DbSet<ADRESSE> ADRESSEs { get; set; }
        public virtual DbSet<ANNONCE> ANNONCEs { get; set; }
        public virtual DbSet<CARTECREDIT> CARTECREDITs { get; set; }
        public virtual DbSet<CLIENT> CLIENTs { get; set; }
        public virtual DbSet<COPIE> COPIEs { get; set; }
        public virtual DbSet<EMPLOYE> EMPLOYEs { get; set; }
        public virtual DbSet<FILM> FILMs { get; set; }
        public virtual DbSet<FILMGENRE> FILMGENREs { get; set; }
        public virtual DbSet<FILMPAY> FILMPAYS { get; set; }
        public virtual DbSet<FORFAIT> FORFAITs { get; set; }
        public virtual DbSet<GENRE> GENREs { get; set; }
        public virtual DbSet<LOCATION> LOCATIONs { get; set; }
        public virtual DbSet<PAY> PAYS { get; set; }
        public virtual DbSet<PERSONNE> PERSONNEs { get; set; }
        public virtual DbSet<PERSONNESFILM> PERSONNESFILMs { get; set; }
        public virtual DbSet<ROLE> ROLEs { get; set; }
        public virtual DbSet<SCENARISTE> SCENARISTEs { get; set; }

        public virtual List<FILM> getFilmByTitle(String title)
        {
            var titleParameter = new OracleParameter("titres", OracleDbType.Varchar2, title, ParameterDirection.Input);
            var result = new OracleParameter("resultset", OracleDbType.RefCursor, ParameterDirection.Output);

            return this.Database.SqlQuery<FILM>("BEGIN rechercheFilmTitre(:result, :titres); END;", result, titleParameter).ToList();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ADRESSE>()
                .Property(e => e.IDADRESSE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ADRESSE>()
                .Property(e => e.NUMCIVIQUE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ADRESSE>()
                .Property(e => e.RUE)
                .IsUnicode(false);

            modelBuilder.Entity<ADRESSE>()
                .Property(e => e.VILLE)
                .IsUnicode(false);

            modelBuilder.Entity<ADRESSE>()
                .Property(e => e.PROVINCE)
                .IsUnicode(false);

            modelBuilder.Entity<ADRESSE>()
                .Property(e => e.CODEPOSTAL)
                .IsUnicode(false);

            modelBuilder.Entity<ANNONCE>()
                .Property(e => e.IDANNONCE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ANNONCE>()
                .Property(e => e.LIEN)
                .IsUnicode(false);

            modelBuilder.Entity<ANNONCE>()
                .Property(e => e.IDFILM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARTECREDIT>()
                .Property(e => e.IDCARTECREDIT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARTECREDIT>()
                .Property(e => e.TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<CARTECREDIT>()
                .Property(e => e.NUMERO)
                .IsUnicode(false);

            modelBuilder.Entity<CARTECREDIT>()
                .Property(e => e.CVV)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.IDCLIENT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.IDPERSONNE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.IDCARTECREDIT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.IDFORFAIT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<COPIE>()
                .Property(e => e.IDCOPIE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<COPIE>()
                .Property(e => e.IDFILM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPLOYE>()
                .Property(e => e.IDEMPLOYE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPLOYE>()
                .Property(e => e.MATRICULE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPLOYE>()
                .Property(e => e.IDPERSONNE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILM>()
                .Property(e => e.IDFILM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILM>()
                .Property(e => e.TITRE)
                .IsUnicode(false);

            modelBuilder.Entity<FILM>()
                .Property(e => e.DUREEFILM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILM>()
                .Property(e => e.POSTER)
                .IsUnicode(false);

            modelBuilder.Entity<FILM>()
                .Property(e => e.NBCOPIE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILM>()
                .Property(e => e.LANGUEORIGINALE)
                .IsUnicode(false);

            modelBuilder.Entity<FILM>()
                .Property(e => e.IDREALISATEUR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILM>()
                .Property(e => e.CODE)
                .IsUnicode(false);

            modelBuilder.Entity<FILMGENRE>()
                .Property(e => e.IDFILMGENRE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILMGENRE>()
                .Property(e => e.IDFILM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILMGENRE>()
                .Property(e => e.IDGENRE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILMPAY>()
                .Property(e => e.IDFILMPAYS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILMPAY>()
                .Property(e => e.IDPAYS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FILMPAY>()
                .Property(e => e.IDFILM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FORFAIT>()
                .Property(e => e.IDFORFAIT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FORFAIT>()
                .Property(e => e.NOM)
                .IsUnicode(false);

            modelBuilder.Entity<FORFAIT>()
                .Property(e => e.PRIX)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FORFAIT>()
                .Property(e => e.LOCATIONSMAX)
                .HasPrecision(38, 0);

            modelBuilder.Entity<FORFAIT>()
                .Property(e => e.DUREEMAX)
                .HasPrecision(38, 0);

            modelBuilder.Entity<GENRE>()
                .Property(e => e.IDGENRE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<GENRE>()
                .Property(e => e.TITRE)
                .IsUnicode(false);

            modelBuilder.Entity<LOCATION>()
                .Property(e => e.IDLOCATION)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOCATION>()
                .Property(e => e.IDCOPIE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOCATION>()
                .Property(e => e.IDCLIENT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PAY>()
                .Property(e => e.IDPAYS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PAY>()
                .Property(e => e.NOM)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONNE>()
                .Property(e => e.IDPERSONNE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PERSONNE>()
                .Property(e => e.NOM)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONNE>()
                .Property(e => e.PRENOM)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONNE>()
                .Property(e => e.COURRIEL)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONNE>()
                .Property(e => e.TELEPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONNE>()
                .Property(e => e.MOTDEPASSE)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONNE>()
                .Property(e => e.IDADRESSE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PERSONNESFILM>()
                .Property(e => e.IDPERSONNEFILM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PERSONNESFILM>()
                .Property(e => e.NOM)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONNESFILM>()
                .Property(e => e.PRENOM)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONNESFILM>()
                .Property(e => e.LIEUDENAISSANCE)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONNESFILM>()
                .Property(e => e.PHOTO)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONNESFILM>()
                .Property(e => e.CODE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PERSONNESFILM>()
                .HasMany(e => e.FILMs)
                .WithOptional(e => e.PERSONNESFILM)
                .HasForeignKey(e => e.IDREALISATEUR);

            modelBuilder.Entity<PERSONNESFILM>()
                .HasMany(e => e.ROLEs)
                .WithOptional(e => e.PERSONNESFILM)
                .HasForeignKey(e => e.IDACTEUR);

            modelBuilder.Entity<ROLE>()
                .Property(e => e.IDROLE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ROLE>()
                .Property(e => e.PERSONNAGE)
                .IsUnicode(false);

            modelBuilder.Entity<ROLE>()
                .Property(e => e.IDACTEUR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ROLE>()
                .Property(e => e.IDFILM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<SCENARISTE>()
                .Property(e => e.IDSCENARISTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<SCENARISTE>()
                .Property(e => e.NOM)
                .IsUnicode(false);

            modelBuilder.Entity<SCENARISTE>()
                .Property(e => e.IDFILM)
                .HasPrecision(38, 0);
        }
    }
}
