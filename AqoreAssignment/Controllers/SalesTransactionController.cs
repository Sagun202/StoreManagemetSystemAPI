using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using DatabaseModel.DatabaseEntity;
using Microsoft.AspNetCore.Http;
using Repository.Implementation;
using DatabaseModel;

namespace AqoreAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesTransactionController : ControllerBase
    {
     
        private readonly ISalesTransactionRepository _salesTransactionRepository;

        public SalesTransactionController(ISalesTransactionRepository salesTransactionRepository)
        {
            _salesTransactionRepository = salesTransactionRepository;
        }


        [HttpGet("GetAllSalesTransactions")]
        public async Task<ActionResult<IEnumerable<SalesTransactionDTO>>> GetAllSaleTransactions()
        {
            try
            {
                var data = await _salesTransactionRepository.GetAllSalesTransactions();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost("CreateSaleTransaction")]
        public async Task<IActionResult> CreateSalesTransaction(SalesTransaction salesTransaction)
        {
            try
            {
                var created = await _salesTransactionRepository.CreateSalesTransaction(salesTransaction);
                return created ? Ok(created) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSalesTransaction")]

        public async Task<IActionResult> DeleteSalesTransaction(int id)
        {
            try
            {
                var deleted = await _salesTransactionRepository.DeleteSalesTransaction(id);
                return deleted ? Ok(deleted) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSalesTransaction")]
        public async Task<IActionResult> UpdateSalesTransaction(SalesTransaction  salesTransaction)
        {
            try
            {
                var created = await _salesTransactionRepository.UpdateSalesTransaction(salesTransaction);
                return created ? Ok(created) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
