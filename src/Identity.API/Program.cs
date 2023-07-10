using Identity.API.Data;

var builder = WebApplication.CreateBuilder(args);
var _config = builder.Configuration;
var _connectionString = _config["ConnectionString"];
// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddConfiguration();
builder.Services.AddScoped<IUserData>(c => new UserData(_connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
