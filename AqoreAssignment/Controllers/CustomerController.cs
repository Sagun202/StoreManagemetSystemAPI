using DatabaseModel.DatabaseEntity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace AqoreAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {   
            _customerRepository = customerRepository;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
             var data = await _customerRepository.GetAllCustomers();
            return Ok(data);
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomers(Customer customer)
        {
            try
            {
                var created =  await _customerRepository.CreateCustomer(customer);
               return created ? Ok(created) : BadRequest(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpDelete("DeleteCustomer")]

        public async Task<IActionResult> DeleteCustomers(int id)
        {
            try
            {
                var deleted = await _customerRepository.DeleteCustomer(id);
                return deleted ? Ok(deleted) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            try
            {
                var created = await _customerRepository.UpdateCustomer(customer);
                return created ? Ok(created) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
