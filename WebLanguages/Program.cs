using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();   //Add the method AddViewLocation

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");   //Add route of folder resources

// Declarate cultures
const string DefaultCulture = "en-us";     // Add culture for start
var SupportCultures = new[] {       // Array for declare the cultures
    new CultureInfo(DefaultCulture),
    new CultureInfo("es"),
    new CultureInfo("ja")    
};

// Using cultures
builder.Services.Configure<RequestLocalizationOptions>(options => {     
    options.DefaultRequestCulture = new RequestCulture(DefaultCulture);     // Using the default culture
    options.SupportedCultures = SupportCultures;        // Add the support culture
    options.SupportedUICultures = SupportCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

}

//Fuction to set the app to work with the supported cultures
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
