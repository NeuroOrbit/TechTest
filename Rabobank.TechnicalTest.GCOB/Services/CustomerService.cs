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
        private readonly IAddressRepository _addressRepository;
        private readonly ICountryRepository _countryRepository;

        /// <summary>
        /// CustomerService : CTOR
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="customerRepository"></param>
        public CustomerService(ILogger<CustomerService> logger,ICustomerRepository customerRepository, IAddressRepository addressRepository,ICountryRepository countryRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;

        }

        /// <summary>
        /// GetCustomerById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerById(int id)
        {
            _logger.LogDebug($"CustomerService:GetCustomerById({id})");
            
            // Get CustomerDto
            var customerDto =  await _customerRepository.GetAsync(id);

            // Map to Customer
            Customer customer = new Customer();

            // Get AddressDto

            // Get CountryDto

            // Update Customer 
           

            return customer;
        }

        /// <summary>
        /// CreateCustomer
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns>int customers id</returns>
        public async Task<int> CreateCustomer(Customer newCustomer)
        {
            _logger.LogDebug($"CustomerService:CreateCustomer({newCustomer.FullName})");

            // get new id
            var newCustId = await _customerRepository.GenerateIdentityAsync();

            // map customer to customerDto
            var customerDto = new CustomerDto();
            customerDto.Id = newCustId;
   


            // Insert customerDto
            await _customerRepository.InsertAsync(customerDto);



            return newCustId;
        }


    }
}



