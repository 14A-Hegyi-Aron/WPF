using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TravelAgency.Data.Models
{
    [Table("applies")]
    public class ApplyModel: IModelWithId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Range(0, 40)]
        public int NumberOfPeople { get; set; }
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(30)]
        public string PhoneNumber { get; set; }
        public OfferModel Offer { get; set; }
    }
}
