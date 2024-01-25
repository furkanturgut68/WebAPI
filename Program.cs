using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// eklemek istendilen veritaný entegresi gerçekleþtirildi
builder.Services.AddDbContext<ProductContext>(x => x.UseSqlite("Data Source=products.db"));

// Add services to the container.
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
