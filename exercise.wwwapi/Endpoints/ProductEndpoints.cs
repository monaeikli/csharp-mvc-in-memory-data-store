using exercise.wwwapi.DTO;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetProducts);
            products.MapPost("/", AddProduct);
            products.MapGet("/{id}", GetProductById);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", (DeleteProduct));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static async Task<IResult> GetProducts(IProductRepository repository)
        {
            var results = await repository.GetAsync();
            return TypedResults.Ok(results);
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> AddProduct(IProductRepository repository, NewProduct model)
        {
            Product entity = new Product();
            entity.Name = model.Name;
            entity.Price = model.Price;

            await repository.AddProduct(entity);

            return TypedResults.Created($"https://localhost:7197/products/{entity.Id}", entity);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProductById(IProductRepository repository, int id)
        {
            var result = await repository.GetProductById(id);
            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, int id, NewProduct model)
        {
            var updated = await repository.UpdateProduct(id, new Product
            {
                Id = id,
                Name = model.Name,
                Price = model.Price
            });

            return updated ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository repository, int id) // NEW
        {
            var ok = await repository.DeleteProduct(id);
            return ok ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
