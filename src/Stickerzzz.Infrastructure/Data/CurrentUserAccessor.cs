using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;


namespace Stickerzzz.Infrastructure.Data
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
