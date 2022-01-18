using Daily_Blazor_App.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();

builder.Services.AddLocalization();

var supportedCultures = new List<CultureInfo>
{
   new CultureInfo("de"),
   new CultureInfo("en")
}; builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture =
       new Microsoft.AspNetCore.Localization.RequestCulture("en");
    options.SupportedUICultures = supportedCultures;
});



// Not working yet
//string APP_ID = builder.Configuration["Back4App:APP_ID"];
//string APP_URI = builder.Configuration["Back4App:APP_URI"];
//string NETKEY = builder.Configuration["Back4App:NETKEY"];

//Back4AppConfig Config = new Back4AppConfig();
//Config.APP_ID = APP_ID;
//Config.APP_URI = APP_URI;
//Config.NETKEY = NETKEY; ;

//builder.Services.AddSingleton<Back4AppConfig>(Config);

var app = builder.Build();

app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();