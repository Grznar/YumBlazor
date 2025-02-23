﻿using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;

namespace YumBlazor.Repositories.IRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

       public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }   

        public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
        {
            orderHeader.OrderDate = DateTime.Now;
            await _db.OrderHeaders.AddAsync(orderHeader);
            await _db.SaveChangesAsync();
            return orderHeader;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId = null)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                return await _db.OrderHeaders.Where(x => x.UserId== userId).ToListAsync();
            }
            return await _db.OrderHeaders.ToListAsync();
        }

        public async Task<OrderHeader> GetAsync(int orderId)
        {
            return await _db.OrderHeaders.Include(u=>u.OrderDetails)
            .FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task<OrderHeader> UpdateStatusAsync(int orderId, string status)
        {
            var orderHeader = _db.OrderHeaders.FirstOrDefault(x => x.Id == orderId);
            if(orderHeader!=null)
            {
                orderHeader.OrderStatus = status;
                await _db.SaveChangesAsync();
            }
            return orderHeader;
        }
    }
}
