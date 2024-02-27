using BusinessClientsManager.Data;
using BusinessClientsManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ClientDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClientsDB"));
});
builder.Services.AddScoped<IClientRepo, ClientRepo>();
builder.Services.AddScoped<IBusinessClientService, BusinessClientService>();
builder.Services.AddHttpClient("APIs:PostIt:ClientName", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["APIs:PostIt:Url"]);
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
