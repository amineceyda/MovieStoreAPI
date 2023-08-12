using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieStoreAPI.Business.Services;
using MovieStoreAPI.Data.DBContext;
using MovieStoreAPI.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IMovieService, MovieService>();

//dbContext
var dbType = builder.Configuration.GetConnectionString("DbType");
if (dbType == "MsSql")
{
    builder.Services.AddDbContext<SimDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));
}

//Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SimDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //

app.UseAuthorization();

app.MapControllers();

app.Run();
