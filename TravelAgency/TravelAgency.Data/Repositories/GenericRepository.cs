using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public abstract class GenericRepository<T> where T : class, IModelWithId
    {
        protected readonly string fileName;

        public GenericRepository(string fileName = "travelmode.json")
        {
            this.fileName = fileName;
        }

        public virtual IEnumerable<T> GetAll()
        {
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName, Encoding.UTF8);
                return JsonConvert.DeserializeObject<T[]>(json);
            }
            return new T[0];
        }

        public virtual T Insert(T model)
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

        public virtual T Update(T model)
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
        public virtual void Delete(int id)
        {
            var modes = GetAll().ToList();
            var modelToDelete = modes.SingleOrDefault(m => m.Id == id);
            if (modelToDelete == null)
                throw new KeyNotFoundException($"Travel mode with id {id} not found");
            modes.Remove(modelToDelete);
            WriteData(modes);
        }

        private void WriteData(IEnumerable<T> data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fileName, json, Encoding.UTF8);
        }
    }
}
