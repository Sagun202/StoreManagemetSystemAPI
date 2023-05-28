using DatabaseModel.DatabaseEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Implementation;
using Repository.Interface;

namespace AqoreAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }





        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var data = await _productRepository.GetAllProducts();
            return Ok(data);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProducts(Product product)
        {
            try
            {
                var created = await _productRepository.CreateProduct(product);
                return created ? Ok(created) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteProduct")]

        public async Task<IActionResult> DeleteProducts(int id)
        {
            try
            {
                var deleted = await _productRepository.DeleteProduct(id);
                return deleted ? Ok(deleted) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product Product)
        {
            try
            {
                var created = await _productRepository.UpdateProduct(Product);
                return created ? Ok(created) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
