using BCommerce.Core.DomainClasses;
using BCommerce.Insfrastructure.Data;
using Microsoft.AspNetCore.Identity;
using BCommerce.Insfrastructure.Extensions;
using BCommerce.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BCommerce.Web.Options;
using BCommerce.Web.Middlewares;
using BCommerce.Application;
using BCommerce.Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrasture(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddScoped<ApplicationUserContext, ApplicationUserContext>();

builder.Services.AddScoped<SeedData>();

builder.Services.AddScoped<QueryHelper>();


//servicelere identity nin bağımlılıklarının eklenmesi
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(t =>
{
    t.Password.RequireDigit = true;
    t.Password.RequiredLength = 6;
    t.Password.RequireUppercase = false;
    t.Password.RequireLowercase = false;
    t.Password.RequireNonAlphanumeric = true;
    t.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

//AddErrorDescriber<CustomIdentityErrorDescriber>()

string Issuer = builder.Configuration["JWT:Issuer"];
string Audience =builder.Configuration["JWT:Audience"];
string SigningKey = builder.Configuration["JWT:SigningKey"];



//options pattern (appsettingsden  JWT  BÖLÜMÜNÜ okuyup JWTOptions nesnesine atar. İsteyen yerler injection yonetımıyle buna ulaşır.)
//"JWT": {
//    "Issuer": "https://localhost:44380",
//    "Audience": "https://localhost:44380",
//    "SigningKey": "5dfa84d0-667f-412a-9b7e-87c80db2baa3"
//  },
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.JWT));


#region [.NET IDDENTITU JWT AUTENTİCATİONI AKTİF HALE GETİR]
//jwt tokenı cozen mekanızma
// Configure Authentication
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidIssuer = Issuer,
        ValidateAudience = false,
        ValidAudience = Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey))
    };

});

#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


//request mvc pipeline ına girmeden bu middleware e girsin
app.AddWBeererControlMiddleware();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.AddVisitorControl();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{


    SeedData seedData = scope.ServiceProvider.GetRequiredService<SeedData>();
    await seedData.Seed();

}


app.Run();
