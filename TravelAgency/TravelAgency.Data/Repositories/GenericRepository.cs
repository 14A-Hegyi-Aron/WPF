using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public abstract class GenericRepository<T> where T : class, IModelWithId
    {
        protected readonly string fileName;

        public GenericRepository(string fileName)
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
            var list = GetAll().ToList();
            if (list.Count > 0)
                model.Id = list.Max(i => i.Id) + 1;
            else
                model.Id = 1;
            list.Add(model);
            WriteData(list);
            return model;
        }

        public virtual T Update(T model)
        {
            var list = GetAll().ToList();
            var modelToModify = list.SingleOrDefault(m => m.Id == model.Id);
            if (modelToModify == null)
                throw new KeyNotFoundException();
            var index = list.IndexOf(modelToModify);
            list.RemoveAt(index);
            list.Insert(index, model);
            WriteData(list);
            return model;
        }

        public virtual void Delete(int id)
        {
            var list = GetAll().ToList();
            var modelToDelete = list.SingleOrDefault(m => m.Id == id);
            if (modelToDelete == null)
                throw new KeyNotFoundException();
            list.Remove(modelToDelete);
            WriteData(list);
        }

        private void WriteData(IEnumerable<T> list)
        {
            var json = JsonConvert.SerializeObject(list);
            File.WriteAllText(fileName, json, Encoding.UTF8);

        }
    }
}
