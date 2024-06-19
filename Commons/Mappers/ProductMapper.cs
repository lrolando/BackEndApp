using Commons.DTOs;
using Entities;
using Riok.Mapperly.Abstractions;

namespace Commons.Mappers
{
    [Mapper]
    public partial class ProductMapper
    {

        public partial IEnumerable<ProductResponse> ProductsToProductsResponse(IEnumerable<Product> products);

        public partial ProductResponse ProductToProductResponse(Product product);
        
        public partial Product ProductRequestToProduct(ProductRequest product);

    }
}
