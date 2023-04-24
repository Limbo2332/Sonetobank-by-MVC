using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using CourseWork_Update.Data;
using CourseWork_Update.Services;
using CourseWork_Update.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IPutDataService, PutDataService>();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration.GetValue<string>("Authentication:Google:ClientId");
        googleOptions.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Google:ClientSecret");
    })
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.ClientId = builder.Configuration.GetValue<string>("Authentication:Facebook:ClientId");
        facebookOptions.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Facebook:ClientSecret");
    })
    .AddTwitter(twitterOptions =>
    {
        twitterOptions.ConsumerKey = builder.Configuration.GetValue<string>("Authentication:Twitter:ConsumerKey");
        twitterOptions.ConsumerSecret = builder.Configuration.GetValue<string>("Authentication:Twitter:ConsumerSecret");
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=MainPage}/{id?}");
app.MapRazorPages();

app.Run();
