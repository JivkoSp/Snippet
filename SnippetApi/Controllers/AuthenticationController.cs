using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SnippetApi.Data.Context;
using SnippetApi.Models;
using SnippetApi.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SnippetApi.Controllers
{
    [ApiController]
    [Route("snippet/authenticate")]
    public class AuthenticationController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private IConfiguration _config;
        private AppDbContext _context;

        private AuthResult GenerateJwtToken(IdentityUser identityUser)
        {
            var key = Encoding.UTF8.GetBytes(_config.GetSection("JwtConfig:Secret").Value);

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {

                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var jwtToken = jwtTokenHandler.CreateToken(tokenDescriptor);
            var parsedToken = jwtTokenHandler.WriteToken(jwtToken);

            return new AuthResult()
            {
                Token = parsedToken,
                Success = true
            };
        }

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
            _context = appDbContext;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResult), 200)]
        [ProducesResponseType(typeof(AuthResult), 400)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

                    if (result.Succeeded)
                    {
                        var token = GenerateJwtToken(user);
                        return Ok(token);
                    }
                }
            }

            return BadRequest(new AuthResult()
            {

                Success = false,
                Errors = new List<string>()
                {
                  "Something went wrong"
                }
            });
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResult), 200)]
        [ProducesResponseType(typeof(AuthResult), 400)]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            bool isUserRegistered = false;

            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(registerDto.Email);

                if (user == null)
                {
                    var newUser = new IdentityUser()
                    {
                        UserName = registerDto.Name,
                        Email = registerDto.Email,
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(newUser, registerDto.Password);

                    if (result.Succeeded)
                    {
                        var token = GenerateJwtToken(newUser);
                        return Ok(token);
                    }
                }
                else { isUserRegistered = true; }          
            }

            return BadRequest(new AuthResult()
            {

                Success = false,
                Errors = new List<string>()
                {
                   isUserRegistered == true ? "User is already registered" : "Server error"
                }
            });
        }
    }
}
