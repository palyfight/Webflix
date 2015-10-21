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

        public virtual List<FILM> getFilmByDate(DateTime min, DateTime max)
        {
            var dateMin = new OracleParameter("min", OracleDbType.Date, min.Date, ParameterDirection.Input);
            var dateMax = new OracleParameter("max", OracleDbType.Date, max.Date, ParameterDirection.Input);
            var result = new OracleParameter("resultset", OracleDbType.RefCursor, ParameterDirection.Output);

            return this.Database.SqlQuery<FILM>("BEGIN rechercheDateParution(:result, :min, :max); END;", result, dateMin, dateMax).ToList();
        }

        public virtual List<FILM> getFilmByActor(String actor)
        {
            var actorParameter = new OracleParameter("noms", OracleDbType.Varchar2, actor, ParameterDirection.Input);
            var result = new OracleParameter("resultset", OracleDbType.RefCursor, ParameterDirection.Output);

            return this.Database.SqlQuery<FILM>("BEGIN rechercheFilmActeur(:result, :noms); END;", result, actorParameter).ToList();
        }

        public virtual List<FILM> getFilmByRealisator(String realisator)
        {
            var realisatorParameter = new OracleParameter("noms", OracleDbType.Varchar2, realisator, ParameterDirection.Input);
            var result = new OracleParameter("resultset", OracleDbType.RefCursor, ParameterDirection.Output);

            return this.Database.SqlQuery<FILM>("BEGIN rechercheFilmRealisateur(:result, :noms); END;", result, realisatorParameter).ToList();
        }

        public virtual List<FILM> getFilmByGenre(String genre)
        {
            var genreParameter = new OracleParameter("genres", OracleDbType.Varchar2, genre, ParameterDirection.Input);
            var result = new OracleParameter("resultset", OracleDbType.RefCursor, ParameterDirection.Output);

            return this.Database.SqlQuery<FILM>("BEGIN rechercheFilmGenre(:result, :genres); END;", result, genreParameter).ToList();
        }

        public virtual List<FILM> getFilmByCountry(String country)
        {
            var countryParameter = new OracleParameter("paysRechercher", OracleDbType.Varchar2, country, ParameterDirection.Input);
            var result = new OracleParameter("resultset", OracleDbType.RefCursor, ParameterDirection.Output);

            return this.Database.SqlQuery<FILM>("BEGIN rechercheFilmPays(:result, :paysRechercher); END;", result, countryParameter).ToList();
        }

        public virtual List<FILM> getFilmByLanguage(String language)
        {
            var languageParameter = new OracleParameter("langue", OracleDbType.Varchar2, language, ParameterDirection.Input);
            var result = new OracleParameter("resultset", OracleDbType.RefCursor, ParameterDirection.Output);

            return this.Database.SqlQuery<FILM>("BEGIN rechercheFilmLangue(:result, :langue); END;", result, languageParameter).ToList();
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

        //RECHERCHE
        public virtual String searchMovie(String query)
        {
            Stack resultStack = new Stack();
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

            return json;

        }

        public virtual String AdvanceSearchMovie(String title, String actor, String realisator, String genre, String country, String language, String dates)
        {
            Stack resultStack = new Stack();
            string json = "";

            if (title.Length > 0)
            {
                var titlesFilm = this.getFilmByTitle(title).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(titlesFilm);

            }

            if (actor.Length > 0)
            {
                actor = actor.Replace(",", "|");
                var actorFilm = this.getFilmByActor(actor).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(actorFilm);

            }

            if (realisator.Length > 0)
            {
                var realisatorFilm = this.getFilmByRealisator(realisator).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(realisatorFilm);

            }


            if (genre.Length > 0)
            {
                genre = genre.Replace(",", "|");
                var genreFilm = this.getFilmByGenre(genre).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(genreFilm);

            }


            if (country.Length > 0)
            {
                country = country.Replace(",", "|");
                var contryFilm = this.getFilmByCountry(country).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(contryFilm);

            }


            if (language.Length > 0)
            {
                var languageFilm = this.getFilmByLanguage(language).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                resultStack.Push(languageFilm);

            }


            if (dates.Length > 0)
            {
                Match isNumber = Regex.Match(dates, @"^[0-9-|]*$");

                if (isNumber.Success)
                {

                    DateTime starts = DateTime.ParseExact(dates + "-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                    DateTime ends = DateTime.ParseExact(dates + "-12-31", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);

                    var dateFilm = this.getFilmByDate(starts, ends).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                    resultStack.Push(dateFilm);
                }

            }

            if (resultStack.Count > 0)
            {
                var result = ((Dictionary<Decimal, FILM>)resultStack.Pop());
                while (resultStack.Count > 0)
                {
                    result = result.Keys.Intersect(((Dictionary<Decimal, FILM>)resultStack.Pop()).Keys).ToDictionary(d => d, d => result[d]); 
                }
                json = JsonConvert.SerializeObject(result);
            }

            return json;
        }
    }
}
