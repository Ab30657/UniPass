var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors();
    app.UseHttpsRedirection();
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
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToController("Index", "Fallback");
        });
}

app.Run();
