using Microsoft.AspNetCore.Mvc;
using NZ_Walks.UI.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace NZ_Walks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();

            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7188/api/Regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());
            }
            catch (Exception ex)
            {
                // Log Exception
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7188/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8,"application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response is not null)
            {
                return RedirectToAction("Index","Regions");   
            }

            return View(response); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7188/api/Regions/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7188/api/Regions/"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7188/api/Regions/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Regions");

            }
            catch (Exception ex) 
            { 
                // Console
            }

            return View("Edit");
        }
    }
}
