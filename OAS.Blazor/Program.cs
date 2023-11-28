using MudBlazor.Services;
using Radzen;
using OAS.Blazor.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using static Stimulsoft.Report.StiOptions;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddRadzenComponents();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpRequestSender,HttpRequestSender>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
              .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme,
              options =>
              {
                  options.Authority = "https://localhost:5000/";
                  options.ClientId = "client";
                  options.ClientSecret = "secret";
                  options.UsePkce = true;
                  options.ResponseType = "code";
                  options.Scope.Add("openid");
                  options.Scope.Add("profile");
                  //options.Scope.Add("email");
                  options.Scope.Add("offline_access");

                  //Scope for accessing API
                  //options.Scope.Add("identityApi"); //invalid scope for client

                  // Scope for custom user claim
                  //options.Scope.Add("appUser_claim"); //invalid scope for client

                  // map custom user claim 
                  //options.ClaimActions.MapUniqueJsonKey("appUser_claim", "appUser_claim");

                  //options.CallbackPath = ...
                  options.SaveTokens = true;
                  options.GetClaimsFromUserInfoEndpoint = true;

              });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    builder.WebHost.UseStaticWebAssets();
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();