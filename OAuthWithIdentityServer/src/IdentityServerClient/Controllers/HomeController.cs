using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using IdentityServerClient.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace IdentityServerClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Contact()
        {
            try
            {
                var doc = await DiscoveryClient.GetAsync("http://localhost:53297/");
                var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
                var oauthServerDetails = await HttpContext.Authentication.GetAuthenticateInfoAsync("oidc");
                var introspectionClient = new IntrospectionClient(doc.IntrospectionEndpoint, "mvc", "superSecretPassword");
                var respons = await introspectionClient.SendAsync(new IntrospectionRequest { Token = accessToken });
                var isActice = respons.IsActive;
                var claims = respons.Claims;
                string slackUserId = null;
                string userId = null;
                string userName = null;
                foreach (var item in claims)
                {
                    if (item.Type == "sub")
                        userId = item.Value;
                    if (item.Type == "slack_user_id")
                        slackUserId = item.Value;
                    if (item.Type == "email")
                        userName = item.Value;
                }
               ApplicationUser user = new ApplicationUser()
                {
                    UserName = userName,
                    Email = userName,
                    Id = userId,
                    SlackUserId = slackUserId
                };
                UserLoginInfo info = new UserLoginInfo(oauthServerDetails.Description.DisplayName, accessToken, oauthServerDetails.Description.DisplayName);
                var result = await _userManager.CreateAsync(user);
                var loginResult = await _userManager.AddLoginAsync(user, info);
                ViewData["Message"] = "Your contact page.";

                var APIProjectUrl = "http://localhost:53297/";
                var client = new HttpClient();
                client.SetBearerToken(accessToken);
                var url = string.Format("{0}{1}{2}", APIProjectUrl, "user/", userId);
                var userDetails = await client.GetAsync(url);
                var content = await userDetails.Content.ReadAsStringAsync();
                var userAC = JsonConvert.DeserializeObject<ApplicationUser>(content);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
