using Backend;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient("ip-api", (serviceProvider, client) =>
{
    var externalApiOptions = serviceProvider.GetRequiredService<IOptions<ExternalApiOptions>>().Value;

    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.BaseAddress = new Uri(externalApiOptions.ApiPath);
    client.DefaultRequestHeaders.Add("ApiKey", externalApiOptions.ApiKey);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions<ExternalApiOptions>().BindConfiguration("ExternalApiOptions");
builder.Services.AddScoped<IGeoLocService, GeoLocService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:44351", "http://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
