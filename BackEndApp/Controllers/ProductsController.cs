using Commons.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ProductsCRUD;

namespace BackEndApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<ProductResponse>>>> GetAll()
        {
            ActionResult response;

            var productResponse = await productsService.GetAll();

            if (productResponse.status == 200)
            { response = Ok(productResponse); }
            else
            { response = BadRequest(productResponse); }

            return response;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ProductResponse>>> GetById(int id)
        {
            ActionResult response;

            var productResponse = await productsService.GetById(id);

            if (productResponse.status == 200)
            { response = Ok(productResponse); }
            else
            { response = BadRequest(productResponse); }

            return response;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ActionResult<Response<ProductResponse>>> Add(ProductRequest product)
        {
            ActionResult response;

            var productResponse = await productsService.Add(product);

            if (productResponse.status == 200)
            { response = Ok(productResponse); }
            else
            { response = BadRequest(productResponse); }

            return response;
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<ProductResponse>>> Update(int id, [FromBody] ProductRequest product)
        {
            ActionResult response;

            var productResponse = await productsService.Update(id, product);

            if (productResponse.status == 200)
            { response = Ok(productResponse); }
            else
            { response = BadRequest(productResponse); }

            return response;
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<ProductResponse>>> Delete(int id)
        {
            ActionResult response;

            var productResponse = await productsService.Delete(id);

            if (productResponse.status == 200)
            { response = Ok(productResponse); }
            else
            { response = BadRequest(productResponse); }

            return response;
        }
    }
}
