using JwtAuthAspNet7WebAPI.Core.DbContext;
using JwtAuthAspNet7WebAPI.Core.Entities;
using JwtAuthAspNet7WebAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JwtAuthAspNet7WebAPI.Core.Services
{
    public class ProcurementService : IProcurementService
    {
        private readonly ApplicationDbContext _context;

        public ProcurementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<object> GetTotalOrderValueByProductAsync(string productName)
        {
            if (productName.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                var price = await _context.Orders.SumAsync(o => o.Price);
                var totalPrice = await _context.Orders.SumAsync(o => o.Price * o.Quantity);
                var totalCount = await _context.Orders.CountAsync();
                var averagePrice = totalCount > 0 ? price / totalCount : 0;

                return new
                {
                    Price = averagePrice,
                    Quantity = await _context.Orders.SumAsync(o => o.Quantity),
                    Total = totalPrice
                };
            }
            else
            {
                var totalPrice = await _context.Orders
                    .Where(o => o.ProductName == productName)
                    .SumAsync(o => o.Price * o.Quantity);

                return new
                {
                    Price = await _context.Orders
                        .Where(o => o.ProductName == productName)
                        .SumAsync(o => o.Price),
                    Quantity = await _context.Orders
                        .Where(o => o.ProductName == productName)
                        .SumAsync(o => o.Quantity),
                    Total = totalPrice
                };
            }
        }




    }
}
