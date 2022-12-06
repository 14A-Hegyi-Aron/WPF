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
            try
            {
                return Ok(teacherRepository.GetAll());
            }
            catch (Exception ex)
            {
#if DEBUG
                return StatusCode(500, new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
#else
                Response.Body 
#endif

            }
        }
    }
}
