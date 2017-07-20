using System.Threading.Tasks;
using System.Web.Mvc;
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
                var client = new Client(authenticator);
                var activities = await client.Activities.GetAthleteActivities();
                foreach (var activity in activities)
                {
                    viewModel.Activities.Add(new ActivityViewModel(activity));
                }
            }

            return View(viewModel);
        }

        public async Task<ActionResult> Login()
        {
            var authenticator = CreateAuthenticator();
            var loginUri = await authenticator.GetLoginLinkUri();
            return Redirect(loginUri.AbsoluteUri);
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
    }
}