using CityGuide.API.Data;
using CityGuide.API.DTOs;
using CityGuide.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController1 : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthController1(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            if (await _authRepository.UserExists(userForRegisterDto.UserName)) ModelState.AddModelError("UserName", "Username already exists!");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userToRegister = new User
            {
                UserName = userForRegisterDto.UserName
            };

            var registeredUser = await _authRepository.Register(userToRegister, userForRegisterDto.Password);

            // Created status code 201
            return StatusCode(201);
        }
    }
}
