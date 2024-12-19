using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BpkbMvc.Models;
using Microsoft.AspNetCore.Http;

namespace BpkbMvc.Controllers
{
    public class TrBpkbController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TrBpkbController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BpkbApiClient");
                var response = await client.GetAsync("tr_bpkb/locations");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var storageLocations = JsonSerializer.Deserialize<List<MsStorageLocation>>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    ViewBag.StorageLocations = storageLocations;
                }
                else
                {
                    ViewBag.StorageLocations = new List<MsStorageLocation>();
                }

                // Mendapatkan nama pengguna yang sedang login dari session
                var loggedInUser = HttpContext.Session.GetString("UserName") ?? "Unknown";
                ViewBag.CreatedBy = loggedInUser;
                ViewBag.LastUpdatedBy = loggedInUser;
                ViewBag.CreatedOn = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                ViewBag.LastUpdatedOn = ViewBag.CreatedOn;
            }
            catch
            {
                ViewBag.StorageLocations = new List<MsStorageLocation>();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrBpkb model)
        {
            // Validasi apakah lokasi dipilih
            if (string.IsNullOrEmpty(model.LocationId))
            {
                ModelState.AddModelError("LocationId", "Please select a valid location.");
            }

            // Jika model tidak valid, kembali ke halaman Create
            if (!ModelState.IsValid)
            {
                await PopulateStorageLocations();
                return View(model);
            }

            try
            {
                var client = _httpClientFactory.CreateClient("BpkbApiClient");

                // Mendapatkan nama pengguna yang sedang login dari session
                var loggedInUser = HttpContext.Session.GetString("UserName") ?? "Unknown";
                model.CreatedBy = loggedInUser;
                model.LastUpdatedBy = loggedInUser;
                model.CreatedOn = DateTime.UtcNow;
                model.LastUpdatedOn = DateTime.UtcNow;

                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("tr_bpkb", content);

                // Tangani Response dari API
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "TrBpkb");
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error: {responseContent}");
                    // Menambahkan log atau penanganan error lebih lanjut jika diperlukan
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An unexpected error occurred: {ex.Message}");
            }

            // Populasi data lokasi jika terjadi error
            await PopulateStorageLocations();
            return View(model);
        }

        private async Task PopulateStorageLocations()
        {
            var client = _httpClientFactory.CreateClient("BpkbApiClient");
            var response = await client.GetAsync("tr_bpkb/locations");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var storageLocations = JsonSerializer.Deserialize<List<MsStorageLocation>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.StorageLocations = storageLocations;
            }
            else
            {
                ViewBag.StorageLocations = new List<MsStorageLocation>();
            }
        }
    }
}
