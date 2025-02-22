using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repositories.IRepositories;

namespace YumBlazor.Repositories
{
    
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductRepository(ApplicationDbContext db,IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task <bool> DeleteAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(c=> c.Id == id);
            var imgPath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
            if(File.Exists(imgPath))
            {
                File.Delete(imgPath);
            }
            if(product!=null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product == null)
            {

                return new Product();
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var categories = await _db.Products.Include(u=>u.Category).ToListAsync();
            return categories;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productFromDb = await _db.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
            if(productFromDb != null)
            {
                productFromDb = product;
                _db.Update(productFromDb);
                await _db.SaveChangesAsync();
                return productFromDb;
            }
            return product;
        }
    }
}
