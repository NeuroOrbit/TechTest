using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Repositories;
using Rabobank.TechnicalTest.GCOB.Services;
using System.Linq;
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

            // Get AddressDto
            var addressDto = await _addressRepository.GetAsync(customerDto.AddressId);
            
            // Get CountryDto
            var countryDto = await _countryRepository.GetAsync(addressDto.CountryId);

            // Map to Customer
            Customer customer = new Customer() 
                { Id = customerDto.Id, FullName = $"{customerDto.FirstName} {customerDto.LastName}",
                Street = addressDto.Street, City = addressDto.City, Postcode = addressDto.Postcode,
                Country = countryDto.Name};
                     

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
            customerDto.FirstName = newCustomer.FullName.Split(' ')[0];
            customerDto.LastName = newCustomer.FullName.Split(' ').Last();

            // get countries
            var countries = await _countryRepository.GetAllAsync();

            // map customer to addressDto
            var addressDto = new AddressDto();
            addressDto.Id = await _addressRepository.GenerateIdentityAsync();
            customerDto.AddressId = addressDto.Id;
            addressDto.Street = newCustomer.Street;
            addressDto.City = newCustomer.City;
            addressDto.Postcode= newCustomer.Postcode;
            addressDto.CountryId = countries.First(x => x.Name.ToLower().Equals(newCustomer.Country.ToLower())).Id;

            // insert addressDto
            await _addressRepository.InsertAsync(addressDto);
            // Insert customerDto
            await _customerRepository.InsertAsync(customerDto);

            return newCustId;
        }


    }
}



