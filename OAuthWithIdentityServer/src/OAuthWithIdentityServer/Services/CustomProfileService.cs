using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using OAuthWithIdentityServer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthWithIdentityServer.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        public CustomProfileService(IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, UserManager<ApplicationUser> userManager)
        {
            _claimsFactory = claimsFactory;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(userId);
            var principal = await _claimsFactory.CreateAsync(user);
            var claim = principal.Claims.ToList();
            claim = claim.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
            claim.Add(new System.Security.Claims.Claim("email", user.Email));
            claim.Add(new System.Security.Claims.Claim("slack_user_id", user.SlackUserId));
            context.IssuedClaims = claim;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(userId);
            context.IsActive = user != null;
        }
    }
}
