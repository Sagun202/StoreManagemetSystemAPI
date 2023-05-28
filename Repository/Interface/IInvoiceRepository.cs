using DatabaseModel;
using DatabaseModel.DatabaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IInvoiceRepository
    {

        Task<IEnumerable<InvoiceDTO>> GetAllInvoices();
        Task<IEnumerable<InvoiceDTO>> GenerateInvoice(InvoiceInput salesTrasactionIds);
    }
}
