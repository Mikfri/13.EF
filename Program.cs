using ItemRazorV1.EFDbContext;
using ItemRazorV1.Models;
using ItemRazorV1.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IItemService, ItemService>();
builder.Services.AddSingleton<UserService, UserService>();  // Registrer den nye service DbService i Program.cs, så den kan benyttes af ItemService og UserService.
//builder.Services.AddTransient<JsonFileService>(); <-- gammel service..
builder.Services.AddTransient<JsonFileService<Item>>(); // <-- NY JSON service ITEM
builder.Services.AddTransient<JsonFileService<User>>(); // <-- NY JSON service USER
builder.Services.AddDbContext<ItemDbContext>();         // <-- Registrering af serviceklassEN
//builder.Services.AddSingleton<DbService, DbService>();  // Registrer den nye service DbService i Program.cs, så den kan benyttes af ItemService og UserService.
builder.Services.AddTransient<DbGenericService<Item>, DbGenericService<Item>>();
builder.Services.AddTransient<DbGenericService<User>, DbGenericService<User>>();
builder.Services.AddTransient<DbGenericService<Order>, DbGenericService<Order>>();  // HACK mulig ændring? (OPG13)

//------------------------
builder.Services.Configure<CookiePolicyOptions>(options => {
    // This lambda determines whether user consent for non-essential cookies is needed for a given request. options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.LoginPath = "/Login/LogInPage";

});
builder.Services.AddMvc().AddRazorPagesOptions(options => {
    options.Conventions.AuthorizeFolder("/Item");

}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
//------------------------


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


app.UseAuthentication();        //Hvorfor er det vigtigt den står præcis her: imellem Routing og Authorization?


app.UseAuthorization();

app.MapRazorPages();

app.Run();
