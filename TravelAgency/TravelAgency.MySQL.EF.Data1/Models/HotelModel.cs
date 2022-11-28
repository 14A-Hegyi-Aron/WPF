using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TravelAgency.Data.Models
{
    [Table("hotels")]
    public class HotelModel: IModelWithId
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Range(0, 7)]
        public int Stars { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        [Required]
        [MaxLength(200)]
        public string WebPageUrl { get; set; }
        public string Description { get; set; }
    }
}
