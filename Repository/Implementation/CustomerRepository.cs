using DatabaseModel.DatabaseEntity;
using Repository;
using Repository.Interface;

using DatabaseModel.DatabaseEntity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;


namespace Repository.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AssignmentContext _dbContext;
        public CustomerRepository(AssignmentContext dbContext)

        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateCustomer(Customer customer)
        {
            try
            {
                string json = JsonConvert.SerializeObject(customer);
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC AddCustomer @JSON", new SqlParameter("@JSON", json));
                return true;
            }
            catch (SqlException ex)
            {              
               throw new Exception(ex.Message);             
            }
        


        }

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteCustomer @CustomerID", new SqlParameter("@CustomerID", id));
                return true;

            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new Exception("Cannot delete customer due to existing related records."); 
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            try
            {
                string sql = "EXEC GetAllCustomers";
                return await _dbContext.Customers.FromSqlRaw(sql).ToListAsync();
            }
            catch(SqlException e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            try
            {
                string json = JsonConvert.SerializeObject(customer);
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC UpdateCustomer @JSON", new SqlParameter("@JSON", json));
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
