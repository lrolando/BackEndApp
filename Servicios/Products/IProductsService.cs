using Commons.DTOs;
using Entities;

namespace Services.ProductsCRUD
{
    public interface IProductsService
    {

        public Task<Response<IEnumerable<ProductResponse>>> GetAll();

        public Task<Response<ProductResponse>> GetById(int id);

        public Task<Response<ProductResponse>> Update(int id, ProductRequest product);

        public Task<Response<ProductResponse>> Add(ProductRequest product);

        public Task<Response<ProductResponse>> Delete(int id);
    }
}
