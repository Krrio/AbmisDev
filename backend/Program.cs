using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//Konfiguracja Ustawien JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>{
    options.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});
// Dodanie serwisów do kontenera DI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=>{
    // Dodanie definicji bezpieczeństwa dla JWT w Swaggerze
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Description = "JWT Authorization header using the Bearer scheme",
    Name = "Authorization", // Nazwa nagłówka, który będzie zawierał token JWT
    In = ParameterLocation.Header, // Token będzie przekazywany w nagłówku HTTP
    Type = SecuritySchemeType.ApiKey, // Typ schematu bezpieczeństwa
    Scheme = "Bearer" // Nazwa schematu
});

// Dodanie wymagań bezpieczeństwa dla Swaggera, aby każdy endpoint wymagał tokenu JWT
c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme, // Odniesienie do schematu bezpieczeństwa
                Id = "Bearer" // Nazwa schematu zdefiniowana powyżej
            }
        },
        new string[] {} // Puste stringi oznaczają brak dodatkowych wymagań
    }
});
});

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy =>
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()); // Obsługuje tokeny lub dane uwierzytelniające
    });

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbContext"))
);
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();

