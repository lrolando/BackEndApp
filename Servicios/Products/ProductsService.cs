using Commons.DTOs;
using Commons.Mappers;
using Entities;
using Microsoft.Extensions.Configuration;
using Persistence.Repository;

namespace Services.ProductsCRUD
{
    public class ProductsService : IProductsService
    {

        private readonly IRepository<Product> productsRepository;

        public ProductsService(IRepository<Product> productsRepository, IConfiguration config)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<Response<IEnumerable<ProductResponse>>> GetAll()
        {
            var response = new Response<IEnumerable<ProductResponse>>();

            try
            {
                var products = await productsRepository.GetAll();

                if (products != null)
                {
                    var mapper = new ProductMapper();
                    var productsResponse = mapper.ProductsToProductsResponse(products);

                    response.message = "";
                    response.data = productsResponse;
                    response.status = 200;
                }
                else
                {
                    response.message = "No se encontraron productos";
                    response.status = 400;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message + ex.InnerException;
                response.status = 400;
            }
            return response;
        }

        public async Task<Response<ProductResponse>> GetById(int productId)
        {
            var response = new Response<ProductResponse>();

            try
            {
                var product = await productsRepository.GetById(productId);

                if (product != null)
                {
                    var mapper = new ProductMapper();
                    var productResponse = mapper.ProductToProductResponse(product);

                    response.message = "";
                    response.data = productResponse;
                    response.status = 200;
                }
                else
                {
                    response.message = "No se encontro el producto";
                    response.status = 400;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message + ex.InnerException;
                response.status = 400;
            }
            return response;
        }

        public async Task<Response<ProductResponse>> Update(int id, ProductRequest product)
        {
            var response = new Response<ProductResponse>();

            try
            {
                var mapper = new ProductMapper();
                var prod = mapper.ProductRequestToProduct(product);

                prod.Id = id;

                productsRepository.Update(prod);

                response.message = "Producto modificado";
                response.status = 200;
            }
            catch (Exception ex)
            {
                response.message = ex.Message + ex.InnerException;
                response.status = 400;
            }
            return response;
        }

        public async Task<Response<ProductResponse>> Add(ProductRequest product)
        {
            var response = new Response<ProductResponse>();

            try
            {
                var mapper = new ProductMapper();
                var prod = mapper.ProductRequestToProduct(product);

                prod.LoadDate = DateTime.Now;

                productsRepository.Add(prod);

                response.message = "Producto agregado";
                response.status = 200;
            }
            catch (Exception ex)
            {
                response.message = ex.Message + ex.InnerException;
                response.status = 400;
            }
            return response;
        }

        public async Task<Response<ProductResponse>> Delete(int productId)
        {
            var response = new Response<ProductResponse>();

            try
            {
                var product = new Product(){ Id = productId};

                productsRepository.Delete(product);

                response.message = "Producto eliminado";
                response.status = 200;
            }
            catch (Exception ex)
            {
                response.message = ex.Message + ex.InnerException;
                response.status = 400;
            }
            return response;
        }

    }
}
