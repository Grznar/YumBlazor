﻿using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repositories.IRepositories;

namespace YumBlazor.Repositories
{
    
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task <bool> DeleteAsync(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c=> c.Id == id);
            if(category!=null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {

                return new Category();
            }
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await _db.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var categoryFromDb = await _db.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if(categoryFromDb != null)
            {
                categoryFromDb = category;
                _db.Update(categoryFromDb);
                await _db.SaveChangesAsync();
                return categoryFromDb;
            }
            return category;
        }
    }
}
