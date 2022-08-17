using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Models
{
    public class City
    {
        //Şehrin fotoğrafları
        public City()
        {
            Photos=new List<Photo>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //
        public List<Photo>Photos{get; set;} //bir şehrin fotoğrafları çok tane

       //şehrin kullanıcısı(ekleyeni)
       
        public User User { get; set; } //şehrin kullanıcısı bir tane

    }
}
