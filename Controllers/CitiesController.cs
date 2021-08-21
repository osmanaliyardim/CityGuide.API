using CityGuide.API.Data;
using CityGuide.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IAppRepository _appRepository;

        public CitiesController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public IActionResult GetCities()
        {
            // Sadece son kullanıcıya göstermek istediğimiz kolonları gönderiyoruz (DTO ile yapılır)
            // AutoMapper ile daha kısa yapılabiliyor
            var cities = _appRepository.GetCities()
                .Select(x=> new CityForListDto { Id = x.Id, Name = x.Name, Description = x.Description, PhotoUrl = x.Photos.FirstOrDefault(x=>x.IsMain==true).Url }).ToList();

            return Ok(cities);
        }
    }
}
