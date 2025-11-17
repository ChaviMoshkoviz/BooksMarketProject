using books;
using Books.core.Repositories;
using Books.core.Service;
using Books.data;
using Books.service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<IDataContext, DataContext>();--
builder.Services.AddDbContext<DataContext>();

builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
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
