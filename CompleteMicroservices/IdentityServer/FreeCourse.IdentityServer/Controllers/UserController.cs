using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace FreeCourse.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)] // Claim bazlı yetkilendirme startup dosyasında eklendi.
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignUpDto Model)
        {

            var user = new ApplicationUser
            {
                Email = Model.Email,
                City = Model.City,
                Country = Model.Country,
                UserName = Model.Username
            };

            var hashedPassword = _userManager.PasswordHasher.HashPassword(user,Model.Password);
            user.PasswordHash = hashedPassword;

            var result = await _userManager.CreateAsync(user);

            if(!result.Succeeded)
            {
                return BadRequest(new ResponseObject<string>($"{Model.Email}",  StatusCodes.Status400BadRequest,  false, result.Errors.Select(s=>s.Description).ToList()));
            }

            return Ok(new ResponseObject<string>($"{Model.Email}",StatusCodes.Status200OK,true));
        }

        [HttpGet(Name ="GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if(userIdClaim == null)
            {
                return BadRequest(new ResponseObject<string>($"", StatusCodes.Status400BadRequest, false, new List<string>
                {
                    "Kullanıcı bulunamadı."
                }));
            }
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if(user == null)
            {
                return BadRequest(new ResponseObject<string>($"", StatusCodes.Status400BadRequest, false, new List<string>
                {
                    "Kullanıcı bulunamadı."
                }));
            }
            return Ok(new ResponseObject<ApplicationUser>(user, StatusCodes.Status200OK, true));
        }
    }
}
