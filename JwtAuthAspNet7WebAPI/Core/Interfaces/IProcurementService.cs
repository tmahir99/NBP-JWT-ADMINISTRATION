using JwtAuthAspNet7WebAPI.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JwtAuthAspNet7WebAPI.Core.Interfaces
{
    public interface IProcurementService
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
        Task<object> GetTotalOrderValueByProductAsync(string productName);

    }
}
