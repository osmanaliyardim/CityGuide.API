using CityGuide.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGuide.API.Data
{
    public class AppRepository : IAppRepository
    {
        private readonly DataContext _context;

        public AppRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public List<City> GetCities()
        {
            var cities = _context.Cities.Include(x=>x.Photos).ToList();

            return cities;
        }

        public City GetCityById(int cityId)
        {
            var city = _context.Cities.Include(x => x.Photos).FirstOrDefault(x => x.Id == cityId);

            return city;
        }

        public Photo GetPhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(x => x.Id == id);

            return photo;
        }

        public List<Photo> GetPhotosByCities(int cityId)
        {
            var photos = _context.Photos.Where(x => x.CityId == cityId).ToList();

            return photos;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
