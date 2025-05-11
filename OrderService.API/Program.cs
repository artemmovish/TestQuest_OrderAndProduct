using OrderService.Infastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Interfaces;
using OrderService.Infastructure.Repositories;
using OrderService.Application.Commands;
using OrderService.Application.Mappers;
using OrderService.Application.Interface;
using OrderService.Infastructure.Services;
using OrderService.Domain.Entites;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb")); // Или UseSqlServer(...) при наличии БД

// Репозитории
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

// AutoMapper
builder.Services.AddAutoMapper(typeof(OrderProfile));

// Сервисы
builder.Services.AddScoped<IOrderItemService, OrderItemService>();

// Регистрация HttpClient
builder.Services.AddHttpClient("MicroserviceClient", client =>
{
    // Укажите базовый адрес вашего микросервиса
    client.BaseAddress = new Uri("https://localhost:7225");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Orders.Any())
    {
        var orderId = Guid.NewGuid();

        dbContext.Orders.Add(
            new OrderEntity
            {
                Id = orderId,
                CreatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItemEntity>()
                {
                    new OrderItemEntity()
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        ProductId = new Guid("7cc3847b-0096-4405-8b60-f3acedc330fc"),
                        Quantity = 15
                    }
                }
            }
        );
        dbContext.SaveChanges();
    }
}

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
