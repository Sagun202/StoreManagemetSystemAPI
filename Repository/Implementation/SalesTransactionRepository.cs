using DatabaseModel;
using DatabaseModel.DatabaseEntity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class SalesTransactionRepository : ISalesTransactionRepository
    {
        private readonly AssignmentContext _dbContext;

        public SalesTransactionRepository(AssignmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateSalesTransaction(SalesTransaction salesTransaction)
        {
            try
            {
                string json = JsonConvert.SerializeObject(salesTransaction);
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC AddSalesTransaction @JSON", new SqlParameter("@JSON", json));
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteSalesTransaction(int id)
        {
            try
            {
                var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteSalesTransaction @TransactionID", new SqlParameter("@TransactionID", id));
                return true;

            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new Exception("Cannot delete Transaction due to existing related records.");
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SalesTransactionDTO>> GetAllSalesTransactions()
        {
            var jsonOutputParam = new SqlParameter("@JsonOutput", SqlDbType.NVarChar, -1)
            {
                Direction = ParameterDirection.Output
            };
           

            string sql = "EXEC GetAllSalesTransaction";
            await _dbContext.Database.ExecuteSqlRawAsync("EXEC GetAllSalesTransaction @JsonOutput OUTPUT", jsonOutputParam);
            var jsonResultStringResult = jsonOutputParam.Value.ToString();
            var data = JsonConvert.DeserializeObject<IEnumerable<SalesTransactionDTO>>(jsonResultStringResult);




             return data ;
        }

        public async Task<bool> UpdateSalesTransaction(SalesTransaction salesTransaction)
        {
            try
            {
                string json = JsonConvert.SerializeObject(salesTransaction);
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC UpdateSalesTransaction @JSON", new SqlParameter("@JSON", json));
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
