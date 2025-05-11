using OrderService.Application.DTOs.Response;
using OrderService.Application.DTOs.Response.Base;
using OrderService.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Infastructure.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly HttpClient _httpClient;

        public OrderItemService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MicroserviceClient");
        }

        public async Task<CommandResponse<CheckStockQuantityResponse>> CheckStockQuantity(Guid id, int quantity)
        {
            // Отправляем запрос с проверкой количества
            var response = await _httpClient.GetAsync($"/api/products/{id}/check-stock?quantity={quantity}");
            response.EnsureSuccessStatusCode();

            // Десериализуем ответ
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр свойств
            };

            var result = JsonSerializer.Deserialize<CommandResponse<CheckStockQuantityResponse>>(content, options);
            return result ?? throw new InvalidOperationException("Не удалось десериализовать ответ.");
        }

        public async Task<CommandResponse<OrderItemResponse>> GetOrderItemResponse(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/products/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<CommandResponse<OrderItemResponse>>();
            return content!;
        }

        public async Task<CommandResponse<OrderItemResponse>> RemoveFromStock(Guid id, int quantity)
        {
            var content = new
            {
                id = id,
                quantity = quantity
            };

            // Serialize the content
            var jsonContent = JsonSerializer.Serialize(content);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the request to remove from stock
            var response = await _httpClient.PostAsync("/api/products/remove-from-stock", httpContent);
            response.EnsureSuccessStatusCode();

            // Deserialize the response
            var responseContent = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<CommandResponse<OrderItemResponse>>(responseContent, options);
            return result ?? throw new InvalidOperationException("Failed to deserialize the response.");
        }
    }
}
