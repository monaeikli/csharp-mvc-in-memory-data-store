using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _db;

        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<Product> AddProduct(Product entity)
        {
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _db.Products.ToListAsync();

        }

        public async Task<Product> GetProductById(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<bool> UpdateProduct(int id, Product update)
        {
            var existing = await _db.Products.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            existing.Name = update.Name;
            existing.Price = update.Price;

            _db.Products.Update(existing);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var existing = await _db.Products.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            _db.Products.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
