using MudBlazor.Services;
using Radzen;
using OAS.Blazor.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using static Stimulsoft.Report.StiOptions;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Logging;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddRadzenComponents();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpRequestSender, HttpRequestSender>();
builder.Services.AddHttpContextAccessor();
IdentityModelEventSource.ShowPII = true;
builder.Services.AddAuthentication(options =>
{
    
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
              .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme,
              options =>
              {                  
                  options.MetadataAddress = $"{builder.Configuration.GetSection("AuthorityUrl").Value}/.well-known/openid-configuration"; 
                  options.RequireHttpsMetadata = false;
                  options.Authority = builder.Configuration.GetSection("AuthorityUrl").Value;
                  options.ClientId = "client";
                  options.ClientSecret = "secret";
                  options.UsePkce = true;
                  options.ResponseType = "code";
                  options.ResponseMode = "form_post";
                  options.Scope.Add("openid");
                  options.Scope.Add("profile");
                  options.Scope.Add("api1");
                  //options.Scope.Add("offline_access");
                  options.Events = new OpenIdConnectEvents
                  {
                      OnRemoteFailure = context =>
                      {
                          Console.WriteLine("Why Here ?!!");
                          Console.WriteLine("Why?:",context.Failure?.Message);
                          context.Response.Redirect("/");
                          context.HandleResponse();
                          return Task.FromResult(0);
                      }
                  };
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

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();