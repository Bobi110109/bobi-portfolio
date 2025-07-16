using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Добави Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Вземи порта от Render (или използвай 1000 по подразбиране)
var port = Environment.GetEnvironmentVariable("PORT") ?? "1000";
app.Urls.Add($"http://*:{port}");

// Конфигурирай pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
