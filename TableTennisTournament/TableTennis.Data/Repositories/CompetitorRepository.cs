using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TableTennis.Data.Models;

namespace TableTennis.Data.Repositories
{
    /// <summary>
    /// CRUD műveletek végrehajtása a versenyzőkkel kapcsolatban
    /// </summary>
    public class CompetitorRepository
    {
        private readonly string fileName = "competitor.json";

        public CompetitorRepository(string fileName = "competitor.json")
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// A fájlban szerepelő minden versenyző adatát visszaadja
        /// </summary>
        /// <returns>A versenyzők adatai</returns>
        public IEnumerable<CompetitorModel> GetAll()
        {
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                var competitors = JsonConvert.DeserializeObject<CompetitorModel[]>(json);
                return competitors;
            }
            return new CompetitorModel[0]; 
        }

        /// <summary>
        /// A fájlban szerepelő versenyző adatokban keres
        /// </summary>
        /// <param name="filter">Keresési feltételek</param>
        /// <returns>A versenyzők adatai</returns>
        public IEnumerable<CompetitorModel> Search(Func<CompetitorModel, bool> filter)
        {
            return GetAll().Where(filter); 
        }

        /// <summary>
        /// Új versenyzőt ad hozzá a nevezőkhöz
        /// </summary>
        /// <param name="model">Új versenyző adatai</param>
        /// <returns>A fájlba került versenyző adatok</returns>
        public CompetitorModel Create(CompetitorModel model)
        {
            var competitors = GetAll().ToList();
            if (competitors.Any(c => c.Number == model.Number))  //Number egyediségének figyelése
            {
                throw new Exception("Ilyen rajtszámú versenyző már van a nevezettek között");
            }
            model.Id = 1;
            if (competitors.Count > 0)
                model.Id = competitors.Max(c => c.Id) + 1;
            competitors.Add(model);
            var json = JsonConvert.SerializeObject(competitors);
            File.WriteAllText(fileName, json);

            return model;
        }

        /// <summary>
        /// Versenyző adatinak módosítása
        /// </summary>
        /// <param name="model">Módosított adatok</param>
        /// <returns>A fájlba került versenyző adatok</returns>
        public CompetitorModel Update(CompetitorModel model)
        {
            var competitors = GetAll().ToList();
            var competitorToUpdate = competitors.SingleOrDefault(c => c.Id == model.Id);
            if (competitorToUpdate == null)
                throw new KeyNotFoundException();

            if (competitorToUpdate.Number != model.Number)
                if (competitors.Any(c => c.Number == model.Number))  //Number egyediségének figyelése
                {
                    throw new Exception("Ilyen rajtszámú versenyző már van a nevezettek között");
                }

            competitors.Remove(competitorToUpdate);
            competitors.Add(model);
            var json = JsonConvert.SerializeObject(competitors);
            File.WriteAllText(fileName, json);

            return model;
        }

        /// <summary>
        /// Versenyző törlése
        /// </summary>
        /// <param name="id">Törlnedő versenyző belső azonosítója</param>
        public void Delete(int id)
        {
            var competitors = GetAll().ToList();
            var competitorToUpdate = competitors.SingleOrDefault(c => c.Id == id);
            if (competitorToUpdate == null)
                throw new KeyNotFoundException();

            competitors.Remove(competitorToUpdate);
            var json = JsonConvert.SerializeObject(competitors);
            File.WriteAllText(fileName, json);
        }
    }
}
