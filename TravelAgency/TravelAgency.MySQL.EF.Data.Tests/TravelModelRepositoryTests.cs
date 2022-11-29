using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Xml.Linq;
using TravelAgency.Data.Models;
using TravelAgency.Data.Repositories;
using TravelAgency.MySQL.EF.Data;
using TravelAgency.MySQL.EF.Data.Tests;

namespace TravelAgency.Data.Tests
{
    public class TravelModelRepositoryTests: IDisposable
    {
        TravelAgencyDbContext dbContext;
        public TravelModelRepositoryTests()
        {
            Init();
        }

        private void Init()
        {
            dbContext = new TestDbContext();
            //dbContext.Database.Migrate();
            dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetAll()
        {
            var repository = new TravelModeRepository(dbContext);

            IEnumerable<TravelModeModel> modes = repository.GetAll();

            Assert.Equal(3, modes.Count());
            Assert.Equal("busz", modes.Single(m => m.Id == 1).Name);
            Assert.Equal("rep�l�", modes.Single(m => m.Id == 2).Name);
            Assert.Equal("haj�", modes.Single(m => m.Id == 5).Name);
        }

        [Theory]
        [InlineData("aut�")]
        [InlineData("�n�ll�")]
        public void NewMode(string name)
        {
            var repository = new TravelModeRepository(dbContext);
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
        public void Modify()
        {
            var repository = new TravelModeRepository(dbContext);
            var model = new TravelModeModel()
            {
                Id = 2,
                Name = "rep�l�g�p"
            };

            TravelModeModel updatedModel = repository.Update(model);
            Assert.Equal(2, updatedModel.Id);
            Assert.Equal("rep�l�g�p", updatedModel.Name);
            
            IEnumerable<TravelModeModel> modes = repository.GetAll();
            Assert.Equal(3, modes.Count());
            Assert.Equal("rep�l�g�p", modes.Single(m => m.Id == 2).Name);
        }

        [Fact]
        public void Delete()
        {
            var repository = new TravelModeRepository(dbContext);

            repository.Delete(5);
            IEnumerable<TravelModeModel> modes = repository.GetAll();
            Assert.Equal(2, modes.Count());
            Assert.DoesNotContain(modes, m => m.Id == 5);
        }

    }
}