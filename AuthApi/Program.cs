using AuthApplication;
using AuthInfrastructure.Repositories;
using AuthDomain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// JWT secret (à stocker ailleurs en prod)
var jwtKey = builder.Configuration["JwtKey"] ?? "super-long-secret-key-at-least-32-chars!";

// Ajouter CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddScoped(provider =>
    new LoginService(provider.GetRequiredService<IUserRepository>(), jwtKey));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Utiliser CORS avant les contrôleurs
app.UseCors("AllowLocalhost4200");

app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
