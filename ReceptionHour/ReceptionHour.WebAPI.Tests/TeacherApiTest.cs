using ReceptionHour.Data.Models;
using RestSharp;
using System.Net;

namespace ReceptionHour.WebAPI.Tests
{
    public class TeacherAPITest : IDisposable
    {
        string url = "http://localhost:5100";

        readonly RestClient restClient;

        public TeacherAPITest()
        {
            restClient = new RestClient(url);
        }

        public void Dispose()
        {
            restClient.Dispose();
        }

        [Fact]
        public void GetAll()
        {
            var teachers = GetAllTeachers();

            Assert.NotNull(teachers);
            var teacher = teachers.Single(t => t.Id == 1);
            Assert.Equal("Teszt Elek", teacher.Name);

        }

        [Fact]
        public void InsertUpdateDelete()
        {
            var originalNumberOfTeachers = GetAllTeachers()?.Length;

            var newTeacher = new TeacherModel()
            {
                Name = "Minta Géza",
                Room = "103",
                Capacity = 5
            };

            // Insert
            var request = new RestRequest("api/teacher", Method.Put);
            request.AddBody(newTeacher);
            var insertedTeacher = restClient.Put<TeacherModel>(request);

            Assert.NotNull(insertedTeacher);
            Assert.NotEqual(0, insertedTeacher.Id);
            Assert.Equal(newTeacher.Name, insertedTeacher.Name);
            Assert.Equal(originalNumberOfTeachers + 1, GetAllTeachers()?.Length);

            // Update
            var teacherToModify = new TeacherModel()
            {
                Id = insertedTeacher.Id,
                Name = "Dr. Minta Géza",
                Room = "103",
                Capacity = 7
            };

            request = new RestRequest("api/teacher", Method.Post);
            request.AddBody(teacherToModify);
            var modifiedTeacher = restClient.Post<TeacherModel>(request);

            Assert.NotNull(modifiedTeacher);
            Assert.Equal(teacherToModify.Name, modifiedTeacher.Name);
            Assert.Equal(teacherToModify.Capacity, modifiedTeacher.Capacity);
            var teachers = GetAllTeachers();
            Assert.Equal(originalNumberOfTeachers + 1, teachers?.Length);
            var teacher = teachers?.OrderByDescending(t => t.Id).First();
            Assert.Equal(teacherToModify.Name, teacher?.Name);

            // Delete
            request = new RestRequest("api/teacher", Method.Delete);
            request.AddBody(teacherToModify);
            var response = restClient.Delete(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(originalNumberOfTeachers, GetAllTeachers()?.Length);
        }

        private TeacherModel[]? GetAllTeachers()
        {
            var restRequest = new RestRequest("api/teacher", Method.Get);
            return restClient.Get<TeacherModel[]>(restRequest);
        }


    }
}