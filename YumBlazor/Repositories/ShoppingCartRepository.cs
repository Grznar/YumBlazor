using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repositories.IRepositories;

namespace YumBlazor.Repositories
{
    
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var carItems = await _db.ShoppingCarts.Where(c => c.UserId == userId).ToListAsync();
            _db.ShoppingCarts.RemoveRange(carItems);
            return await _db.SaveChangesAsync() >= 0;
        }
        public async Task<bool> UpdateCartAsync(string userId, int productId, int updateBy)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
            if(cart != null)
            {
                cart.Count += updateBy;
                if (cart.Count<=0)
                {
                    _db.ShoppingCarts.Remove(cart);
                }
                
                
            }
            else
            {
                cart = new ShoppingCart()
                {
                    ProductId = productId,
                    UserId = userId,
                    Count = updateBy    
                };
                await _db.ShoppingCarts.AddAsync(cart);

            }
            return await _db.SaveChangesAsync() >= 0;
        }
        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await _db.ShoppingCarts.Where(c => c.UserId == userId).Include(u=>u.Product).ToListAsync();
        }

        public async Task<int> GetTotalCartCountAsync(string? userId)
        {
           int cartCount = 0;
           var carItems =  await _db.ShoppingCarts.Where(c => c.UserId == userId).ToListAsync();
            foreach(var item in carItems)
            {
                cartCount += item.Count;
            }
            return cartCount;
        }
    }
}
