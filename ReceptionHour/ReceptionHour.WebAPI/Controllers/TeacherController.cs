using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReceptionHour.Data;
using ReceptionHour.Data.Models;
using ReceptionHour.Data.Repositories;

namespace ReceptionHour.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherRepository teacherRepository;
        public TeacherController(ReceptionHourDbContext dbContext)
        {
            teacherRepository = new TeacherRepository(dbContext);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return this.Run(() =>
            {
                return Ok(teacherRepository.GetAll());
            });
        }

        [HttpPut]
        public IActionResult Insert(TeacherModel model)
        {
            return this.Run(() =>
            {
                return Ok(teacherRepository.Insert(model));
            });
        }

        [HttpPost]
        public IActionResult Update(TeacherModel model)
        {
            return this.Run(() =>
            {
                return Ok(teacherRepository.Update(model));
            });
        }

        [HttpDelete]
        public IActionResult Delete(TeacherModel model)
        {
            return this.Run(() =>
            {
                teacherRepository.Delete(model);
                return Ok();
            });
        }
    }
}
