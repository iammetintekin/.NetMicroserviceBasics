using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;            
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByEmailAsync(context.UserName);
            if(existUser is null)
            { 
                context.Result.CustomResponse = new Dictionary<string, object>
                {
                    { 
                        "errors", 
                        new List<string>
                        {
                            "Kullanıcı bulunamadı."
                        } 
                    }
                };
                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
            if (!passwordCheck)
            {
                context.Result.CustomResponse = new Dictionary<string, object>
                {
                    {
                        "errors",
                        new List<string>
                        {
                            "Kullanıcı bulunamadı."
                        }
                    }
                };
                return;
            }
            // identity server tokeni üretmesi gerektiğinii buradan anlıyor.
            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
