using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
//using TravelAgency.Data.Repositories;

namespace TravelAgency.Data.Models
{
    [Table("offers")]
    public class OfferModel: IModelWithId
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Destination { get; set; }
        [Required]
        public TravelModeModel Mode { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public HotelModel Hotel { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int MaxParticipants { get; set; }
        public byte[] Photo { get; set; }

        public int ModeId { get; set; }
        public int HotelId { get; set; }

        // These are not required, but they are useful for the UI
        //public string ModeName { get; set; }
        //public string HotelName { get; set; }

        //[NotMapped]
        //public bool NincsRáSzükség { get; set; }
    }
}
