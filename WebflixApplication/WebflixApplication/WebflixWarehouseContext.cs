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
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using System.Web.Helpers;
    using System.Collections;

    public partial class WebflixWarehouseContext : DbContext
    {
        public WebflixWarehouseContext()
            : base("name=Model1")
        {
        }

        public virtual List<LOCATION_W> getClientsByAge(int groupeAge)
        {
            var ageParameter = new OracleParameter("groupeages", OracleDbType.Int32, groupeAge, ParameterDirection.Input);
            return this.Database.SqlQuery<LOCATION_W>("SELECT * FROM LOCATION_W WHERE groupeage = :groupeages", ageParameter).ToList();
        }

        public virtual List<LOCATION_W> getClientsByProvince(String prov)
        {
            var provParameter = new OracleParameter("provinces", OracleDbType.Varchar2, prov, ParameterDirection.Input);
            return this.Database.SqlQuery<LOCATION_W>("SELECT * FROM LOCATION_W WHERE province = :provinces", provParameter).ToList();
        }

        public virtual List<LOCATION_W> getDayOfTheWeek(int dayOfWeek)
        {
            var dayParameter = new OracleParameter("jours", OracleDbType.Int32, dayOfWeek, ParameterDirection.Input);
            return this.Database.SqlQuery<LOCATION_W>("SELECT * FROM LOCATION_W WHERE jour = :jours", dayParameter).ToList();
        }

        public virtual List<LOCATION_W> getMonth(int month)
        {
            var monthParameter = new OracleParameter("mois", OracleDbType.Int32, month, ParameterDirection.Input);
            return this.Database.SqlQuery<LOCATION_W>("SELECT * FROM LOCATION_W WHERE mois = :mois", monthParameter).ToList();
        }

        public virtual List<LOCATION_W> getAllLocations()
        {
            return this.Database.SqlQuery<LOCATION_W>("SELECT * FROM LOCATION_W").ToList();
        }

        public virtual DbSet<CLIENT_W> CLIENT_W { get; set; }
        public virtual DbSet<DATE_W> DATE_W { get; set; }
        public virtual DbSet<FILM_W> FILM_W { get; set; }
        public virtual DbSet<LOCATION_W> LOCATION_W { get; set; }


        //RECHERCHE
        public virtual String analyzeData(int groupeAge, String prov, int day, int week)
        {
            Stack resultStack = new Stack();
            string json = "";

            if (groupeAge != -1)
            {
                var groupesAgeClient = this.getClientsByAge(groupeAge).GroupBy(item => item.IDLOCATION).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(groupesAgeClient);
            }

            if (!prov.Equals("ALL"))
            {
                var provClient = this.getClientsByProvince(prov).GroupBy(item => item.IDLOCATION).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(provClient);
            }

            if (day != -1)
            {
                var dayOfWeek = this.getDayOfTheWeek(day).GroupBy(item => item.IDLOCATION).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(dayOfWeek);
            }

            if (week != -1)
            {
                var genreFilm = this.getMonth(week).GroupBy(item => item.IDLOCATION).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(genreFilm);
            }

            if(groupeAge == -1 && prov.Equals("ALL") && day == -1 && week == -1)
            {
                var allFilms = this.getAllLocations().GroupBy(item => item.IDLOCATION).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(allFilms);
            }

            if (resultStack.Count > 0)
            {
                var result = ((Dictionary<Decimal, LOCATION_W>)resultStack.Pop());
                while (resultStack.Count > 0)
                {
                    result = result.Concat((Dictionary<Decimal, LOCATION_W>)resultStack.Pop()).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value); ;
                }
                json = JsonConvert.SerializeObject(result);
            }
            return json;
        }

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
