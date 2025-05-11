using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(a => a.Id);

            // Настраиваем связь с OrderItemEntity
            builder.HasMany(o => o.OrderItems)  // Указываем навигационное свойство
                   .WithOne()                     // У товара есть один заказ
                   .HasForeignKey(oi => oi.OrderId); // Внешний ключ в OrderItemEntity
        }
    }
}
