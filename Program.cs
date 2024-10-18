using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5203", "http://*:5203");
var configuration = builder.Configuration;

builder.Services.AddDbContext<DataContext>(
	options => options.UseMySql(
		configuration["ConnectionString"],
		ServerVersion.AutoDetect(configuration["ConnectionString"])
	)
);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters{
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["TokenAuthentication:Issuer"],
            ValidAudience = configuration["TokenAuthentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                configuration["TokenAuthentication:SecretKey"]
            ))
        };
    })
;
// para acceder al context sin una llamada http y poder usar el user.claims
builder.Services.AddHttpContextAccessor(); 

// builder.Services.AddAuthorization(options =>       // no necesito roles para mi sistema de propietario
//     {
//         options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
//     });
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseCors(x => x
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
