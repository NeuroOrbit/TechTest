using Rabobank.TechnicalTest.GCOB.Dtos;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerById(int id);

        Task<int> CreateCustomer(Customer newCustomer);
    }
}
