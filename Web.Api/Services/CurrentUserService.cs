using Microsoft.AspNetCore.Http;
using Application.Common.Interfaces;
using System.Security.Claims;

namespace Web.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserName = httpContextAccessor.HttpContext?.User?.Identity.Name;
            if (long.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var _userId))
            {
                UserId = _userId;
            }

            IsAuthenticated = UserId != null;
        }

        public long? UserId { get; }
        public string UserName { get; }

        public bool IsAuthenticated { get; }
    }
}
