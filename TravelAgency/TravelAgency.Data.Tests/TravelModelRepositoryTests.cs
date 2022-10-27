using System.Text;
using System.Xml.Linq;
using TravelAgency.Data.Models;
using TravelAgency.Data.Repositories;

namespace TravelAgency.Data.Tests
{
    public class TravelModelRepositoryTests : IDisposable
    {
        string fileName;

        public TravelModelRepositoryTests()
        {
            Init();
        }

        private void Init()
        {
            fileName = $"travelModes{Guid.NewGuid()}.json";
            var json = "[" +
                "{'id':1, 'name': 'busz'}," +
                "{'id':2, 'name': 'repülõ'}," +
                "{'id':5, 'name': 'hajó'}]";
            File.WriteAllText(fileName, json.Replace('\'', '"'), Encoding.UTF8);
        }

        public void Dispose()
        {
            File.Delete(fileName);
        }

        [Fact]
        public void GetAll()
        {
            var repository = new TravelModeRepository(fileName);

            IEnumerable<TravelModeModel> modes = repository.GetAll();

            Assert.Equal(3, modes.Count());
            Assert.Equal("busz", modes.Single(m => m.Id == 1).Name);
            Assert.Equal("repülõ", modes.Single(m => m.Id == 2).Name);
            Assert.Equal("hajó", modes.Single(m => m.Id == 5).Name);
        }

        [Fact]
        public void GetAllFromNotExistingFile()
        {
            var repository = new TravelModeRepository("not existing.json");

            IEnumerable<TravelModeModel> modes = repository.GetAll();

            Assert.Empty(modes);
        }

        [Theory]
        [InlineData("autó")]
        [InlineData("önálló")]
        public void NewMode(string name)
        {
            var repository = new TravelModeRepository(fileName);
            var model = new TravelModeModel()
            {
                Name = name
            };
            TravelModeModel insertedModel = repository.Insert(model);

            Assert.Equal(6, insertedModel.Id);
            Assert.Equal(name, insertedModel.Name);
            IEnumerable<TravelModeModel> modes = repository.GetAll();
            Assert.Equal(4, modes.Count());
            Assert.Equal(name, modes.Single(m => m.Id == 6).Name);
        }

        [Fact]
        public void NewModeToNotExistingFile()
        {
            File.Delete(fileName);
            TravelModeRepository repository = new(fileName);
            TravelModeModel model = new()
            {
                Name = "busz"
            };
            TravelModeModel insertedMOdel = repository.Insert(model);

            Assert.Equal(1, insertedMOdel.Id);
            Assert.Equal("busz", insertedMOdel.Name);
            IEnumerable<TravelModeModel> modes = repository.GetAll();
            Assert.Single(modes);
            Assert.Equal("busz", modes.Single(m => m.Id == 1).Name);
        }

        [Fact]
        public void Modify()
        {
            var repository = new TravelModeRepository(fileName);
            var model = new TravelModeModel()
            {
                Id = 2,
                Name = "repülõgép"
            };

            TravelModeModel updatedModel = repository.Update(model);
            Assert.Equal(2, updatedModel.Id);
            Assert.Equal("repülõgép", updatedModel.Name);

            IEnumerable<TravelModeModel> modes = repository.GetAll();
            Assert.Equal(3, modes.Count());
            Assert.Equal("repülõgép", modes.Single(m => m.Id == 2).Name);
        }

        [Fact]
        public void Delete()
        {
            var repository = new TravelModeRepository(fileName);

            repository.Delete(5);
            IEnumerable<TravelModeModel> modes = repository.GetAll();
            Assert.Equal(2, modes.Count());
            Assert.DoesNotContain(modes, m => m.Id == 5);
        }

    }
}