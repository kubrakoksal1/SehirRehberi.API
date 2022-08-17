using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Models
{
    public class User
    {
        //Kullanıcının şehirleri tutulur.

        public User()   //Kullanıcının şehirleri çok tane
        {
            Cities = new List<City>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<City> Cities { get; set; } //kullanıcının eklediği şehirler listesi.

    }
}
