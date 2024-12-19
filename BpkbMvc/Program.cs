var builder = WebApplication.CreateBuilder(args);

// Tambahkan HttpClient untuk memanggil API
builder.Services.AddHttpClient("BpkbApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7267/api/"); // Base URL dari API
});

// Tambahkan Session untuk login
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Tambahkan MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();