using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ������ Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// ����� ����� �� Render (��� ��������� 1000 �� ������������)
var port = Environment.GetEnvironmentVariable("PORT") ?? "1000";
app.Urls.Add($"http://*:{port}");

// ������������ pipeline
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
