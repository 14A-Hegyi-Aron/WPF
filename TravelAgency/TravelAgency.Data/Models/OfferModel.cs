﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgency.Data.Repositories;

namespace TravelAgency.Data.Models
{
    public class OfferModel: IModelWithId
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public int ModeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int HotelId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int MaxParticipants { get; set; }
        public byte[] Photo { get; set; }

        [JsonIgnore]
        public string ModeName
        {
            get
            {
                var repo = new TravelModeRepository();
                var mode = repo.GetAll().SingleOrDefault(t => t.Id == this.ModeId);
                if (mode == null)
                    return null;
                return mode.Name;
            }
        }

        [JsonIgnore]
        public string HotelName
        {
            get
            {
                var repo = new HotelRepository();
                return repo.GetAll().SingleOrDefault(h => h.Id == this.HotelId)?.Name;
            }
        }


    }
}
