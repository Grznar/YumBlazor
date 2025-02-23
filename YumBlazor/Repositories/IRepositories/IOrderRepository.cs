using YumBlazor.Data;

namespace YumBlazor.Repositories.IRepositories
{
    public interface IOrderRepository
    {
        public Task<OrderHeader> CreateAsync(OrderHeader orderHeader);
        public Task<OrderHeader> GetAsync(int orderId);
        public Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId=null);
        public Task<OrderHeader> UpdateStatusAsync(int orderId,string status);
    }
}
