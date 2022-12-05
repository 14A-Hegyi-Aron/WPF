using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptionHour.Data.Models
{
    [Table("teachers")]
    public class TeacherModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Room { get; set; }
        public int Capacity { get; set; }
    }
}
