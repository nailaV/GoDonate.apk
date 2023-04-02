using GoDonate.Data;
using GoDonate.Modul.SignalRHelper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddDbContext<GoDonateDbContext>(opcije => opcije.UseSqlServer(config.GetConnectionString("baza")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
    options => options
        .SetIsOriginAllowed(x => _ = true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotifikacijeHub>("/notifikacije-putanja");
});

app.Run();
