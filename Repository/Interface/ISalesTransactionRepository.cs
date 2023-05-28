using DatabaseModel;
using DatabaseModel.DatabaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ISalesTransactionRepository
    {
        Task<IEnumerable<SalesTransactionDTO>> GetAllSalesTransactions();
        Task<bool> CreateSalesTransaction(SalesTransaction salesTransaction);
        Task<bool> UpdateSalesTransaction(SalesTransaction salesTransaction);
        Task<bool> DeleteSalesTransaction(int id);
    }
}
