using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Commands;
using ProductService.Application.Mappers;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Infastructure.Persistence.DbContexts;
using ProductService.Infastructure.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Контроллеры
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb")); // Или UseSqlServer(...) при наличии БД

// Репозитории
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// MediatR — подключение обработчиков команд и запросов
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

// AutoMapper (если используешь)
builder.Services.AddAutoMapper(typeof(ProductProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Products.Any())
    {
        dbContext.Products.AddRange(
            new ProductEntity { Id = new Guid("7cc3847b-0096-4405-8b60-f3acedc330fc"), Name = "Product 1", Price = 10.99m, StockQuantity = 5},
            new ProductEntity { Id = new Guid("d34d6dff-92f1-4aba-93db-0cd1ae972ed7"), Name = "Product 2", Price = 20.50m, StockQuantity = 20}
        );
        dbContext.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
