using BusinessClientsManager.Data;
using BusinessClientsManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
                      policy =>
                      {
                          policy.WithOrigins("*")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

builder.Services.AddDbContext<ClientDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClientsDB"));
});
builder.Services.AddScoped<IClientRepo, ClientRepo>();
builder.Services.AddScoped<IBusinessClientService, BusinessClientService>();
builder.Services.AddHttpClient(builder.Configuration["APIs:PostIt:ClientName"], c =>
{
    c.BaseAddress = new Uri(builder.Configuration["APIs:PostIt:Url"]);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
