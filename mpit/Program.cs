using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using mpit.Application.Auth;
using mpit.DataAccess.Repositories;
using mpit.Mapping;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

services.AddCors(o => {
    o.AddDefaultPolicy(p => {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.AllowCredentials();
        p.WithOrigins("http://localhost:3000", "https://localhost:3000");
    });
});

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Add DI (Services, Mapping, DbContext)
services.AddScoped<UsersRepository>();

services.AddScoped<PasswordHasher>();

services.AddAutoMapper(typeof(ApplicationAutoMapper));

services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(config.GetConnectionString(nameof(ApplicationDbContext))));

var app = builder.Build();


using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.EnsureCreatedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
});

app.MapControllers();

app.UseCors();

app.UseAuthorization();

app.Run();