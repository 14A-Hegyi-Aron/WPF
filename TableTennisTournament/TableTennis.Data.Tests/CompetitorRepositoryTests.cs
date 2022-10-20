using Newtonsoft.Json.Bson;
using TableTennis.Data.Models;
using TableTennis.Data.Repositories;

namespace TableTennis.Data.Tests
{
    public class CompetitorRepositoryTests : IDisposable
    {
        [Fact]
        public void ReadFromNotExistingFile()
        {
            // Arrange
            var repository = new CompetitorRepository("notExists.test.json");
            if (File.Exists("notExists.test.json"))
                File.Delete("notExists.test.json");

            // Act
            var competitors = repository.GetAll();


            // Assert
            Assert.Empty(competitors);
        }

        [Fact]
        public void ReadAllTwoCompetitors()
        {
            // Arrange
            var repository = new CompetitorRepository("TwoCompetitors.test.json");
            startingStatus("TwoCompetitors.test.json");

            // Act
            var competitors = repository.GetAll();

            // Assert
            Assert.Equal(2, competitors.Count());
            var c1 = competitors.Single(c => c.Id == 1);
            Assert.Equal(101, c1.Number);
            Assert.Equal("Béla", c1.Name);
            Assert.Equal(new DateTime(2000, 1, 1), (DateTime)c1.BirthDate);
            Assert.Equal(8, c1.Rank);

            var c2 = competitors.Single(c => c.Id == 2);
            Assert.Equal("Géza", c2.Name);
        }

        [Fact]
        public void NewCompetitor()
        {
            // Arrange
            var repository = new CompetitorRepository("NewCompetitors.test.json");
            startingStatus("NewCompetitors.test.json");

            var competitor = new CompetitorModel()
            {
                Number = 103,
                Name = "Réka",
                BirthDate = new DateTime(2002, 1, 1),
                Rank = 6
            };

            // Act
            var newComp = repository.Create(competitor);

            // Assert
            var competitors = repository.GetAll();
            Assert.Equal(3, competitors.Count());
            var c3 = competitors.Single(c => c.Id == 3);
            Assert.Equal(103, c3.Number);
            Assert.Equal("Réka", c3.Name);
            Assert.Equal(new DateTime(2002, 1, 1), (DateTime)c3.BirthDate);
            Assert.Equal(6, c3.Rank);
        }

        [Theory]
        [InlineData(104, "András", 2003, 5)]
        [InlineData(105, "Cecil", 2004, 4)]
        public void NewCompetitorTheory(int number, string name, int year, int rank)
        {
            // Arrange
            var fileName = $"{Guid.NewGuid()}.test.json";
            var repository = new CompetitorRepository(fileName);
            startingStatus(fileName);

            var competitor = new CompetitorModel()
            {
                Number = number,
                Name = name,
                BirthDate = new DateTime(year, 1, 1),
                Rank = rank
            };

            // Act
            var newComp = repository.Create(competitor);

            // Assert
            var competitors = repository.GetAll();
            Assert.Equal(3, competitors.Count());
            var c3 = competitors.Single(c => c.Id == 3);
            Assert.Equal(number, c3.Number);
            Assert.Equal(name, c3.Name);
            Assert.Equal(new DateTime(year, 1, 1), c3.BirthDate);
            Assert.Equal(rank, c3.Rank);
        }

        [Fact]
        public void NewCompetitorWithSameNumber()
        {
            var repository = new CompetitorRepository("NewCompetitors.test.json");
            startingStatus("NewCompetitors.test.json");

            var competitor = new CompetitorModel()
            {
                Number = 101, // same number as Béla and error
                Name = "Réka",
                BirthDate = new DateTime(2002, 1, 1),
                Rank = 6
            };

            var ex = Assert.Throws<Exception>(() => repository.Create(competitor));
            Assert.Contains("már van", ex.Message);

        }

        [Fact]
        public void ModifyCompetitor()
        {
            var fileName = $"{Guid.NewGuid()}.test.json";
            var repository = new CompetitorRepository(fileName);
            startingStatus(fileName);

            var comp = repository.GetAll().Single(c => c.Id == 2);
            comp.Name = "Gizi";
            comp.Rank = 2;

            repository.Update(comp);
            var c2 = repository.GetAll().Single(c => c.Id == 2);
            Assert.Equal("Gizi", c2.Name);
            Assert.Equal(2, c2.Rank);
        }

        [Fact]
        public void DeleteCompetitor()
        {
            var fileName = $"{Guid.NewGuid()}.test.json";
            var repository = new CompetitorRepository(fileName);
            startingStatus(fileName);

            var comp = repository.GetAll().Single(c => c.Id == 2);
            repository.Delete(comp.Id);

            var competitors = repository.GetAll();
            Assert.Single(competitors);
            Assert.Equal(1, competitors.First().Id);
        }

        public void Dispose()
        {
            Directory.GetFiles(".", "*.test.json").ToList().ForEach(File.Delete);
        }


        private void startingStatus(string fileName)
        {
            var s1 = "{ 'id': 1, 'number': 101, 'name': 'Béla', 'BirthDate':'2000-01-01', 'Rank':'8' }".Replace("'", "\"");
            var s2 = "{ 'id': 2, 'number': 102, 'name': 'Géza', 'BirthDate':'2001-01-01', 'Rank':'7' }".Replace("'", "\"");
            File.WriteAllText(fileName, $"[{s1},{s2}]");
        }
    }
}