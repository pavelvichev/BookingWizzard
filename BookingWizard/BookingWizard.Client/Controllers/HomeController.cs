using BookingWizard.Client.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookingWizard.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var serverClient = _httpClientFactory.CreateClient();

            var discovery = await serverClient.GetDiscoveryDocumentAsync("https://localhost:7037");

            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "BookingWizard"
            }) ;

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:44328/secret");

            var content =  await response.Content.ReadAsStringAsync();
            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                message = content
            }) ;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}