using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BpkbMvc.Models;
using System.Text.Json.Serialization;

namespace BpkbMvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Menampilkan halaman login
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            // Validasi model (username dan password harus diisi)
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please fill in all fields.";
                return View(model);
            }

            try
            {
                var client = _httpClientFactory.CreateClient("BpkbApiClient");

                // Serialize model menjadi JSON
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Kirim POST request ke API login
                var response = await client.PostAsync("login", content);

                // Baca response body
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Status Code: " + response.StatusCode);
                Console.WriteLine("Response Body: " + responseContent);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        // Deserialize response ke object LoginResponse
                        var result = JsonSerializer.Deserialize<LoginResponse>(responseContent);

                        // Validasi jika result null atau Username kosong
                        if (result == null || string.IsNullOrEmpty(result.Username))
                        {
                            ViewBag.ErrorMessage = "Login failed: Invalid response from server.";
                            return View(model);
                        }

                        // Simpan username ke session
                        HttpContext.Session.SetString("UserName", result.Username);

                        // Redirect ke halaman transaksi
                        return RedirectToAction("Create", "TrBpkb");
                    }
                    catch (JsonException ex)
                    {
                        // Jika terjadi error deserialisasi
                        Console.WriteLine("JSON Deserialization Error: " + ex.Message);
                        ViewBag.ErrorMessage = "Login failed: Invalid response format.";
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid credentials. Please try again.";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // Tangani error yang tidak terduga
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Membersihkan session saat logout
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }

    // Model untuk menampung response dari API
    public class LoginResponse
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}