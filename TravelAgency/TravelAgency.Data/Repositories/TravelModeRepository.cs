using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public class TravelModeRepository
    {
        private readonly string fileName;

        public TravelModeRepository(string fileName = "travelmode.json")
        {
            this.fileName = fileName;
        }

        public IEnumerable<TravelModeModel> GetAll()
        {
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName, Encoding.UTF8);
                return JsonConvert.DeserializeObject<TravelModeModel[]>(json);
            }
            return new TravelModeModel[0];
        }

        public TravelModeModel Insert(TravelModeModel model)
        {
            var modes = GetAll().ToList();
            if (modes.Count > 0)
                model.Id = modes.Max(m => m.Id) + 1;
            else
                model.Id = 1;
            modes.Add(model);
            WriteData(modes);
            return model;
        }

        public TravelModeModel Update(TravelModeModel model)
        {
            var modes = GetAll().ToList();
            var modelToModify = modes.SingleOrDefault(m => m.Id == model.Id);
            if (modelToModify == null)
                throw new KeyNotFoundException($"Travel mode with id {model.Id} not found");
            var index = modes.IndexOf(modelToModify);
            modes[index] = model;
            WriteData(modes);
            return model;
        }
        public void Delete(int id)
        {
            var modes = GetAll().ToList();
            var modelToDelete = modes.SingleOrDefault(m => m.Id == id);
            if (modelToDelete == null)
                throw new KeyNotFoundException($"Travel mode with id {id} not found");
            modes.Remove(modelToDelete);
            WriteData(modes);
        }

        private void WriteData(IEnumerable<TravelModeModel> models)
        {
            var json = JsonConvert.SerializeObject(models);
            File.WriteAllText(fileName, json, Encoding.UTF8);
        }
    }
}
