using DatabaseModel.DatabaseEntity;


namespace Repository.Interface
{
    public interface ICustomerRepository
    {
        Task <IEnumerable<Customer>> GetAllCustomers();
        Task<bool> CreateCustomer(Customer customer);

        Task<bool> DeleteCustomer(int id);

        Task<bool> UpdateCustomer(Customer customer);

    }
}
