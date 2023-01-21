using Rabobank.TechnicalTest.GCOB.Dtos;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerById(int id);

        Task<CustomerDto> CreateCustomer(CustomerDto newCustomer);
    }
}
