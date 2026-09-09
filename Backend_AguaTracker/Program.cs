using Backend_AguaTracker.Repository;
using Backend_AguaTracker.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddService(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Configure JWT Bearer options here
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            // Configure token validation parameters
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddCors(options =>
{
    // Le damos un nombre a nuestra regla
    options.AddPolicy("PermitirMiFrontend", policy =>
    {

        // Aquí pones la URL exacta donde correrá tu frontend (Live Server, por ejemplo)
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyHeader()  // Permite enviar tokens JWT y JSON
              .AllowAnyMethod(); // Permite hacer GET, POST, PUT, DELETE
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("PermitirMiFrontend");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
