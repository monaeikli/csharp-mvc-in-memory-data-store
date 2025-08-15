using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAsync();
        Task<Product?> AddProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<bool> UpdateProduct(int id, Product update);
        Task<bool> DeleteProduct(int id);
    }
}
