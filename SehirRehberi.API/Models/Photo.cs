using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        //Bu fotoğraf hangi şehre ait bilgisini tutması lazım
        public City City { get; set; }  // bir fotoğrefın şehri bir tane
    }
}
