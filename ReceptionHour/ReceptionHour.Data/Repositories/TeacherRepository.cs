using Microsoft.EntityFrameworkCore;
using ReceptionHour.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptionHour.Data.Repositories
{
    public class TeacherRepository: GenericRepository<TeacherModel>
    {
        public TeacherRepository(): base(new ReceptionHourDbContext())
        {

        }
        
    }
}
