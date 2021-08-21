using AutoMapper;
using CityGuide.API.Data;
using CityGuide.API.DTOs;
using CityGuide.API.Models;
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
        private IMapper _mapper;

        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("cities")]
        public IActionResult GetCities()
        {
            // Sadece son kullanıcıya göstermek istediğimiz kolonları gönderiyoruz (DTO ile yapılır)
            // AutoMapper ile daha kısa yapılabiliyor
            var cities = _appRepository.GetCities();
                //.Select(x=> new CityForListDto { Id = x.Id, Name = x.Name, Description = x.Description, PhotoUrl = x.Photos.FirstOrDefault(x=>x.IsMain==true).Url }).ToList();

            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);

            return Ok(citiesToReturn);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(City city)
        {
            _appRepository.Add(city);
            _appRepository.SaveAll();

            return Ok(city);
        }

        [HttpGet]
        [Route("detail")]
        public IActionResult GetCitiesById(int cityId)
        {
            var city = _appRepository.GetCityById(cityId);

            var cityToReturn = _mapper.Map<CityForDetailDto>(city);

            return Ok(cityToReturn);
        }

        [HttpGet]
        [Route("photos")]
        public IActionResult GetPhotsByCity(int cityId)
        {
            var photos = _appRepository.GetPhotosByCities(cityId);

            return Ok(photos);
        }
    }
}
