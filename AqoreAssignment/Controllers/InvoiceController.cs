using DatabaseModel;
using DatabaseModel.DatabaseEntity;
using Microsoft.AspNetCore.Mvc;
using Repository.Implementation;
using Repository.Interface;

namespace AqoreAssignment.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase

    {

        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }



        [HttpPost("GenerateInvoice")]
        public async Task<ActionResult<InvoiceDTO>> GenerateInvoice(InvoiceInput transactionIds)
        {
            try
            {
                var invoice = await _invoiceRepository.GenerateInvoice(transactionIds);            
                return invoice.Any() ? Ok(invoice ) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllInvoice")]
        public async Task<ActionResult<InvoiceDTO>> GetAllInvoice()
        {
            try
            {
                var invoice = await _invoiceRepository.GetAllInvoices();
                return invoice.Any() ? Ok(invoice) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
