using DisneyAPI.Models;
using DisneyAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DisneyAPI.Controllers
{
    [ApiController]
    [Route(template: "api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AuthenticationController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
       
        [HttpPost("registro")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            var userExist = await _userManager.FindByNameAsync(model.Username);

            if(userExist != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var user = new Usuario()
            {
                UserName = model.Username,
                Email = model.Email,
                IsActive = true,
            }; 

            var result = await _userManager.CreateAsync(user, model.Password);  
        
            if(!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Status = "Error",
                        Message = $"Creacion de usuario fallida! Error: {string.Join(",", result.Errors.Select(x => x.Description))}"

                    });
            }

            return StatusCode(StatusCodes.Status200OK,
                new
                {
                    Status = "Exitoso!",
                    Message = "Creacion exitosa del usuario!"
                });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestViewModel model)
        {

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(model.Username);

                if(currentUser.IsActive)
                {
                    return Ok(await GetToken(currentUser));
                }
            }

            return StatusCode(StatusCodes.Status401Unauthorized,
                    new
                    {
                        Status = "Error",
                        Message = $"El usuario {model.Username} no esta autorizado."

                    });
        }


        private async Task<LoginResponseViewModel> GetToken(Usuario currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSingingKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: "KeySecretaSuperLargaDeAUTORIZACION"));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7251",
                audience: "https://localhost:7251",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSingingKeys, SecurityAlgorithms.HmacSha256));

            return new LoginResponseViewModel
            {

                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo

            };
        }
    }
}
