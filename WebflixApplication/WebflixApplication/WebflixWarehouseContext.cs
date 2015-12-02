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

        public virtual DbSet<CLIENT_W> CLIENT_W { get; set; }
        public virtual DbSet<DATE_W> DATE_W { get; set; }
        public virtual DbSet<FILM_W> FILM_W { get; set; }
        public virtual DbSet<LOCATION_W> LOCATION_W { get; set; }


        //RECHERCHE
        public virtual String analyzeData(int groupeAge, String prov, int day, int week)
        {
            /*Stack resultStack = new Stack();
            query = query.Replace(",", "|");

            string json = null;


            //Always search by titles

            foreach (String q in query.Split(new String[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (q.Length > 3)
                {
                    var queryTrimed = q.Trim();
                    Match isNumber = Regex.Match(queryTrimed, @"^[0-9-|]*$");

                    var titlesFilm = this.getFilmByTitle(queryTrimed).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                    resultStack.Push(titlesFilm);

                    if (!isNumber.Success)
                    {
                        //search by actor
                        var actorFilm = this.getFilmByActor(queryTrimed).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                        resultStack.Push(actorFilm);

                        //search by realisator
                        var realisatorFilm = this.getFilmByRealisator(queryTrimed).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                        resultStack.Push(realisatorFilm);

                        //search by genre
                        var genreFilm = this.getFilmByGenre(queryTrimed).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                        resultStack.Push(genreFilm);

                        //search by country
                        var contryFilm = this.getFilmByCountry(queryTrimed).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                        resultStack.Push(contryFilm);

                        //search by language
                        var languageFilm = this.getFilmByLanguage(queryTrimed).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                        resultStack.Push(languageFilm);

                    }
                    else
                    {
                        //if plusieur date need to loop
                        //Search by years

                        string[] stringSeparators = new string[] { "-" };
                        String[] years = queryTrimed.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                        if (years[0].Length == 4)
                        {
                            String min = years[0];
                            String max = years[0];
                            if (years.Length > 1 && years[1].Length == 4)
                            {
                                max = years[1];
                            }

                            DateTime starts = DateTime.ParseExact(min + "-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            DateTime ends = DateTime.ParseExact(max + "-12-31", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);

                            var dateFilm = this.getFilmByDate(starts, ends).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                            resultStack.Push(dateFilm);
                        }

                    }
                }

            }

            if (resultStack.Count > 0)
            {
                var result = ((Dictionary<Decimal, FILM>)resultStack.Pop());
                while (resultStack.Count > 0)
                {
                    result = result.Concat((Dictionary<Decimal, FILM>)resultStack.Pop()).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value); ;
                }
                json = JsonConvert.SerializeObject(result);
            }

            return json;*/
            return null;
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
