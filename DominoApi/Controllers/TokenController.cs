using AutoMapper;
using Domino.Core.Entities;
using Domino.Core.Interfaces;
using Domino.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DominoContext _contexto;

     
        public TokenController(IConfiguration configuration, DominoContext contexto)
        {
          
            _configuration = configuration;
            _contexto = contexto;
        }
        [HttpPost]
        public IActionResult Authentication(User user)
        {
            if (IsValidUser(user))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }

            return NotFound("El usuario no fue encontrado");

        }

        private bool IsValidUser(User login)
        {
           
            var user =_contexto.Users.Where(x => x.Email == login.Email).FirstOrDefault();

            if(user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GenerateToken()
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            var claims = new[]
           {
                new Claim(ClaimTypes.Name, "Sebastian"),
                new Claim(ClaimTypes.Email, "jsloperagdv@gmail.com"),
                
            };

            
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(10)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
       
        }

    }
}
