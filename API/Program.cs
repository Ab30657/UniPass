using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().Build();
    });
});
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DataContext>(opt =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        opt.UseSqlite(connectionString);
    });
}
else
{
    builder.Services.AddDbContext<DataContext>(opt =>
    {
        var connectionString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_DefaultConnection");
        opt.UseNpgsql(connectionString);
    });
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
}

builder.Services.AddIdentity<AppUser, AppUserRole>(x =>
{ }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapControllers();
}
else if (app.Environment.IsStaging())
{
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToController("Index", "Fallback");
        });
}

app.Run();
