using DatabaseModel;
using DatabaseModel.DatabaseEntity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{

 
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AssignmentContext _dbContext;

        public InvoiceRepository(AssignmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<InvoiceDTO>> GenerateInvoice(InvoiceInput  salesTrasactionIds)
        {
            try
            {
                var jsonOutputParam = new SqlParameter("@JsonOutput", SqlDbType.NVarChar, -1)
                {
                    Direction = ParameterDirection.Output
                };

                var jsonInput = new SqlParameter("@JSON", JsonConvert.SerializeObject(salesTrasactionIds))  ;

                await _dbContext.Database.ExecuteSqlRawAsync("EXEC GenerateInvoice @JSON, @JsonOutput OUTPUT",jsonInput,jsonOutputParam);

                string jsonResult = jsonOutputParam.Value.ToString();
                var data = JsonConvert.DeserializeObject<IEnumerable<InvoiceDTO>>(jsonResult);


                return data;
            }
            catch (SqlException ex)
            {
                throw new Exception( ex.Message);
            }
        }

        public async Task<IEnumerable<InvoiceDTO>> GetAllInvoices()
        {
            try
            {
                var jsonOutputParam = new SqlParameter("@JsonOutput", SqlDbType.NVarChar, -1)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync("EXEC GetAllInvoice @JsonOutput OUTPUT", jsonOutputParam);

                string jsonResult = jsonOutputParam.Value.ToString();
                var data = JsonConvert.DeserializeObject<IEnumerable<InvoiceDTO>>(jsonResult);

                return data;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
