using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Repositories;
using Rabobank.TechnicalTest.GCOB.Services;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Services
{
    public class CustomerService : ICustomerService
    {
        private ILogger _logger;
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// CustomerService : CTOR
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="customerRepository"></param>
        public CustomerService(ILogger<CustomerService> logger,ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// CreateCustomer
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns></returns>
        public Task<CustomerDto> CreateCustomer(CustomerDto newCustomer)
        {
            _logger.LogDebug($"CustomerService:CreateCustomer({newCustomer.FirstName} {newCustomer.LastName})");

            // get new id
            var newCustId = _customerRepository.GenerateIdentityAsync();
            newCustomer.Id = newCustId.Result;

            // insert customer
            _customerRepository.InsertAsync(newCustomer);
            
            return Task.FromResult(newCustomer);
        }

        /// <summary>
        /// GetCustomerById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<CustomerDto> GetCustomerById(int id)
        {
            _logger.LogDebug($"CustomerService:GetCustomerById({id})");
            
            var customer = _customerRepository.GetAsync(id);

            return Task.FromResult(customer.Result);
        }
    }
}



