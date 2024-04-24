using Game_LeaderBoard.MongoRepository.GenericRepository;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://5ldq7r3n-7211.euw.devtunnels.ms", "https://t1cz6pgb-7211.euw.devtunnels.ms", "http://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();


builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.AddSingleton<IMongoSettings>(provider =>
    provider.GetRequiredService<IOptions<MongoSettings>>().Value);
builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

app.Run();
