using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public decimal InvoiceTotalAmount { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public IList<ProductDTO> Products { get; set; }
    }
}
