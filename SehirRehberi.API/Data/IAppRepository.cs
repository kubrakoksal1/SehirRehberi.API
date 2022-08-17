using SehirRehberi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Data
{
    public interface IAppRepository
    {
        //where T:class  ===> Referans tipi belirler,  Sakın veri tipi geçiyim deme diye bir uyarıdır.
        void Add<T>(T entity) where T:class; // veritabanına ekleme
        void Delete<T>(T entity) where T : class;
        bool SaveAll();  //Kaydet

        // veri okuma
        List<City> GetCities(); //şehirleri listeleme
        List<Photo> GetPhotosByCity(int cityId); // bir şehrin fotoğraflarını çekiyoruz.
        City GetCityById(int cityId); //şehrin detayına gitmek için şehrin data'sını çekeceğiz
        Photo GetPhoto(int id);
    
    }
}
