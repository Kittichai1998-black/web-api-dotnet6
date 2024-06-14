using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using web_api_dotnet6.Data;

var builder = WebApplication.CreateBuilder(args);


var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

// Add services to the container.
//builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Default"),
        new MySqlServerVersion(new Version(8, 0, 36)))
                //.LogTo(Console.WriteLine, LogLevel.Information)
                //.EnableSensitiveDataLogging()
                //.EnableDetailedErrors()
                );

//builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!);


//builder.Services.AddTransient<MySqlConnection>(_ =>
//    new MySqlConnection(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//builder.Services.AddDbContextPool<DbContext>(options =>
//    options.UseInMemoryDatabase(databaseName: "MyInMemoryDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwagger();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
