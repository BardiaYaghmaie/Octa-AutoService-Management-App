using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OAS.Blazor.Data;
using MudBlazor.Services;
using Radzen.Blazor;
using Radzen;
using OAS.Persistence;
using OAS.Application;
using OAS.Domain.Models;
using System.Reflection;
using MediatR;
using Stimulsoft.Report.Blazor;
using static Stimulsoft.Report.StiOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddRadzenComponents();
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();
var app = builder.Build();

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