using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptionHour.Data.Models
{
    [Table("meetings")]
    public class MeetingModel
    {
        public int Id { get; set; }
        [Required]
        public TeacherModel Teacher { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public string ParentName { get; set; }

        public int TeacherId { get; set; }
    }
}
