using Microsoft.EntityFrameworkCore;
using ReceptionHour.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptionHour.Data.Repositories
{
    public class MeetingRepository : GenericRepository<MeetingModel>
    {
        public MeetingRepository() : base(new ReceptionHourDbContext())
        {

        }

        public IEnumerable<MeetingModel> Search(TeacherModel teacher, DateTime? date)
        {
            return dbContext.Set<MeetingModel>()
                .Include(m => m.Teacher)
                .Where(m => (teacher == null || m.Teacher.Id == teacher.Id)
                && (date == null || m.Date == date))
                .ToList();
        }

        public override MeetingModel Insert(MeetingModel entity)
        {
            dbContext.Set<MeetingModel>().Add(entity);
            dbContext.SaveChanges();
            return dbContext.Set<MeetingModel>()
                .Include(m => m.Teacher)
                .SingleOrDefault(m => m.Id == entity.Id);
        }
    }
}
