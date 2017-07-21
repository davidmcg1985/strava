using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using RestSharp.Portable.OAuth2.Configuration;
using StravaActivities.Web;
using StravaSharp;
using TestApplicationRestsharp.Authentication;
using TestApplicationRestsharp.Models;

namespace TestApplicationRestsharp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var authenticator = CreateAuthenticator();
            var viewModel = new HomeViewModel(authenticator.IsAuthenticated);

            if (authenticator.IsAuthenticated)
            {             
                viewModel.Athlete = await GetAthleteProfile(authenticator);
            }                     

            return View(viewModel);
        }

        private async Task<AthleteViewModel> GetAthleteProfile(Authenticator authenticator)
        {
            var client = new Client(authenticator);
            var profile = await client.Athletes.GetCurrent();

            var result = new AthleteViewModel()
            {
                IsAuthenticated = authenticator.IsAuthenticated,
                Firstname = profile.FirstName,
                Lastname = profile.LastName,
                City = profile.City,
                Profile = profile.Profile,
                ProfileMedium = profile.ProfileMedium,
                Weight = profile.Weight,
                MeasurementPreference = profile.MeasurementPreference,
                Follower = profile.FollowerCount,
                FriendCount = profile.FriendCount,
                AthleteType = profile.AthleteType.ToString(),
                FullName = profile.FirstName + " " + profile.LastName
            };

            return result;
        }

        public async Task<ActionResult> ListActivities()
        {
            var authenticator = CreateAuthenticator();
            var viewModel = new HomeViewModel(authenticator.IsAuthenticated);

            if (authenticator.IsAuthenticated)
            {
                var client = new Client(authenticator);
                var activities = await client.Activities.GetAthleteActivities();
                foreach (var activity in activities)
                {
                    viewModel.Activities.Add(new ActivityViewModel(activity));
                }
                viewModel.Athlete = await GetAthleteProfile(authenticator);
            }

            return View("Index", viewModel);
        }

        Authenticator CreateAuthenticator()
        {
            var redirectUrl = $"{Request.Url.Scheme}://{Request.Url.Host}/Home/Callback";
            var config = new RuntimeClientConfiguration
            {
                IsEnabled = false,
                ClientId = Config.ClientId,
                ClientSecret = Config.ClientSecret,
                RedirectUri = redirectUrl,
                Scope = "write,view_private"
            };
            var client = new StravaClient(new RequestFactory(), config);

            return new Authenticator(client);
        }

        public async Task<ActionResult> Callback()
        {
            var authenticator = CreateAuthenticator();
            await authenticator.OnPageLoaded(Request.Url);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Login()
        {
            var authenticator = CreateAuthenticator();
            var loginUri = await authenticator.GetLoginLinkUri();
            return Redirect(loginUri.AbsoluteUri);
        }
    }
}