using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using MyMoviesList.Server.Data;
using MyMoviesList.Shared.Models;
using System.Reflection.Emit;
using System;
using System.Text;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

//Cadena de conexion
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Console.WriteLine($"connection string: {connectionString}");
//builder.Services.AddDbContext<AppicationDbContext>(options => options.UseSqlServer(connectionString));

var connectionString = "server=webapp-alfredo.database.windows.net;database=moviesalfredo;user id=alfhes;password=alfi_chido1;multipleactiveresultsets=true;trustservercertificate=true;";
Console.WriteLine($"connection string: {connectionString}");

builder.Services.AddDbContext<AppicationDbContext>(options =>
    options.UseSqlServer("server=webapp-alfredo.database.windows.net;database=moviesalfredo;user id=alfhes;password=alfi_chido1;multipleactiveresultsets=true;trustservercertificate=true;"));


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppicationDbContext>()
    .AddDefaultTokenProviders();

//Identity authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            //ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtkey"])),
            ClockSkew = TimeSpan.Zero
        };

    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        //.WithOrigins("https://localhost:7064") // Add your Blazor web app's origin

    });
});






var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.environment.isdevelopment())
//{
//    app.useswagger();
//    app.useswaggerui();
//}

app.UseCors();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();



app.UseRouting();


//Agregamos el middleware (uso) de autenticación
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
//app.MapFallbackToFile("index.html");

app.Run();
