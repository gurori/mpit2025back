using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using mpit.Application.Auth;
using mpit.DataAccess.Repositories;
using mpit.Extensions;
using mpit.Mapping;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

services.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(config.GetSection(nameof(AuthorizationOptions)));

services.AddSwaggerGen();

services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000");
        policy.AllowCredentials();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

services.AddMvc();

// Add DI (Services, Mapping, DbContext)
services.AddScoped<UsersRepository>();
services.AddScoped<PermissionsRepository>();
services.AddScoped<PostsRepository>();

services.AddScoped<JwtProvider>();
services.AddScoped<PasswordHasher>();

services.AddAutoMapper(typeof(ApplicationAutoMapper));

services.AddAuthentication(config);

services.AddControllers();

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(config.GetConnectionString(nameof(ApplicationDbContext)))
);

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

app.UseCookiePolicy(
    new CookiePolicyOptions
    {
        MinimumSameSitePolicy = SameSiteMode.Strict,
        HttpOnly = HttpOnlyPolicy.Always,
        Secure = CookieSecurePolicy.Always,
    }
);

app.MapControllers();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
