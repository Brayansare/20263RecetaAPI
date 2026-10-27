using System.Text;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using _20263RecetaAPI.Context;
using _20263RecetaAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar la segurudad de Identity(Microsoft)
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Conexion a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Configurar JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones => opciones.
    TokenValidationParameters = new TokenValidationParameters
    {//aqui esta configuracion de dos pasos bloqueo de 3 intentos 
        ValidateIssuer = false, //no verifica quien lo emitio
        ValidateAudience = false, //no verifica para quine es
        ValidateLifetime = true,// si verifica que no este vencido
        ValidateIssuerSigningKey = true,//si verifica la fira o sea que no se hya modificado
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["LlaveJWT"]
            !)),
        ClockSkew = TimeSpan.Zero
    });


// Add services to the container.
//vamos a utilizar automaper para pasar datos de nuestro modelo a nuestro dto y viceversa
builder.Services.AddAutoMapper(cfg =>
{

}, typeof(Program));

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
//es haxia que parte de mi endpoint se va a ir, redirige el http a https
app.UseHttpsRedirection(); 
// es para saber si va a tener autorizacion
app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
