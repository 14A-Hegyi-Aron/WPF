using MySqlConnector;
using System.Text;
using System.Xml.Linq;
using TravelAgency.Data.Models;
using TravelAgency.Data.Repositories;

namespace TravelAgency.Data.Tests
{
    public class TravelModelRepositoryTests: IDisposable
    {
        string connectionString;
        string dbName;
        public TravelModelRepositoryTests()
        {
            Init();
        }
        
        private void Init()
        {
            connectionString = $"Server=localhost;Database=mysql;Uid=root;Pwd=;";

            dbName = $"travels_{Guid.NewGuid()}";
            while (dbName.Contains('-'))
                dbName = dbName.Replace("-", "_");

            using (var conn = new MySqlConnection(connectionString))
            {
                var sql = $"CREATE DATABASE {dbName} CHARACTER SET utf8 COLLATE utf8_hungarian_ci";
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();
            }

            connectionString = $"Server=localhost;Database={dbName};Uid=root;Pwd=;";
            using (var conn = new MySqlConnection(connectionString))
            {
                var sqlLines = File.ReadAllLines(@"..\..\..\..\TravelAgency.MySQL.Data\CreateDB.sql").ToList();
                var line = sqlLines.SingleOrDefault(l => l.StartsWith("use "));
                var index = sqlLines.IndexOf(line);
                var sql = string.Join("\n", sqlLines.Skip(index + 1));

                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();

                sql = "insert into travelModes (id, name) \n" +
                    "values (1, 'busz'), (2, 'repülõ'), (5, 'hajó')";

                using (var cmd = new MySqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            connectionString = $"Server=localhost;Database=mysql;Uid=root;Pwd=;";
            
            using (var conn = new MySqlConnection(connectionString))
            {
                var sql = $"DROP DATABASE {dbName}";
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();
            }
        }

        [Fact]
        public void GetAll()
        {
            var repository = new TravelModeRepository(connectionString);

            IEnumerable<TravelModeModel> modes = repository.GetAll();

            Assert.Equal(3, modes.Count());
            Assert.Equal("busz", modes.Single(m => m.Id == 1).Name);
            Assert.Equal("repülõ", modes.Single(m => m.Id == 2).Name);
            Assert.Equal("hajó", modes.Single(m => m.Id == 5).Name);
        }

        [Theory]
        [InlineData("autó")]
        [InlineData("önálló")]
        public void NewMode(string name)
        {
            var repository = new TravelModeRepository(connectionString);
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
            var repository = new TravelModeRepository(connectionString);
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
            var repository = new TravelModeRepository(connectionString);

            repository.Delete(5);
            IEnumerable<TravelModeModel> modes = repository.GetAll();
            Assert.Equal(2, modes.Count());
            Assert.DoesNotContain(modes, m => m.Id == 5);
        }

    }
}