using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Shared.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        // claimden dataları okucaz httpcontextde
        private readonly IHttpContextAccessor _httpContext;
        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;            
        }
        public string GetUserId => _httpContext.HttpContext.User.FindFirst("sub").Value;
    }
}
