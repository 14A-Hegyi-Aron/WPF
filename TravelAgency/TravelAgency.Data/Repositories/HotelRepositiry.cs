using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    internal class HotelRepositiry
    {
        private readonly string fileName;

        public HotelRepositiry(string fileName = "hotels.json")
        {
            this.fileName = fileName;
        }

        public IEnumerable<HotelModel> GetAll()
        {
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName, Encoding.UTF8);
                return JsonConvert.DeserializeObject<IEnumerable<HotelModel>>(json);
            }
            return new List<HotelModel>();
        }

        public HotelModel Insert(HotelModel model)
        {
            var hotels = GetAll().ToList();
            if (hotels.Count > 0)
                model.Id = hotels.Max(h => h.Id) + 1;
            else
                model.Id = 1;
            hotels.Add(model);
            WriteData(hotels);
            return model;
        }

        public HotelModel Update(HotelModel model)
        {
            var hotels = GetAll().ToList();
            var modelToModify = hotels.SingleOrDefault(h => h.Id == model.Id);
            if (modelToModify == null)
                throw new KeyNotFoundException($"Hotel with id {model.Id} not found");
            var index = hotels.IndexOf(modelToModify);
            hotels[index] = model;
            WriteData(hotels);
            return model;
        }

        public void Delete(int id)
        {
            var hotels = GetAll().ToList();
            var modelToDelete = hotels.SingleOrDefault(h => h.Id == id);
            if (modelToDelete == null)
                throw new KeyNotFoundException($"Hotel with id {id} not found");
            hotels.Remove(modelToDelete);
            WriteData(hotels);
        }

        private void WriteData(IEnumerable<HotelModel> hotels)
        {
            var json = JsonConvert.SerializeObject(hotels, Formatting.Indented);
            File.WriteAllText(fileName, json, Encoding.UTF8);
        }
    }
}
