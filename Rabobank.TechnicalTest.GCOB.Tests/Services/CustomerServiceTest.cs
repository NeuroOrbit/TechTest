using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Controllers;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Repositories;
using Rabobank.TechnicalTest.GCOB.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services
{
    [TestClass]
    public class CustomerServiceTest
    { 
        private ILogger<CustomerService> logger;
        private Mock<ICustomerRepository> mockCustomerRepo;
        private CustomerDto customerDto;
        private Mock<IAddressRepository> mockAddressRepo;
        private AddressDto addressDto;
        private Mock<ICountryRepository> mockCountryRepo;
        private CountryDto countryDto;

        [TestInitialize]
        public void Initialize()
        {
            var mock = new Mock<ILogger<CustomerService>>();
            logger = mock.Object;

            mockCustomerRepo = new Mock<ICustomerRepository>();
            customerDto = new CustomerDto() { Id = 1, FirstName = "Keith", LastName = "Wright", AddressId = 1 };

            mockAddressRepo = new Mock<IAddressRepository>();
            addressDto = new AddressDto() { Id = 1, Street = "Saints Road", City = "Amsterdam", Postcode = "AM25", CountryId = 1 };

            mockCountryRepo = new Mock<ICountryRepository>();
            countryDto = new CountryDto() { Id = 1, Name = "Netherlands" };

        }


        [TestMethod]
        public async Task GivenHaveACustomer_AndICallAServiceToGetTheCustomer_ThenTheCustomerIsReturned()
        {
            // Arrange
            mockCustomerRepo.Setup(obj => obj.GetAsync(1).Result).Returns(customerDto);
            mockAddressRepo.Setup(obj => obj.GetAsync(1).Result).Returns(addressDto);
            mockCountryRepo.Setup(obj => obj.GetAsync(1).Result).Returns(countryDto);

            var customerService = new CustomerService(logger, mockCustomerRepo.Object, mockAddressRepo.Object, mockCountryRepo.Object);

            // Act
            var response = await customerService.GetCustomerById(1);

            // Assert
            Assert.AreEqual(response.FullName, "Keith Wright");

        }

        [TestMethod]
        public async Task GivenInsertACustomer_AndICallAServiceToInsertTheCustomer_ThenTheCustomerIsInSerted_AndTheNewCustomerIdIsReturned()
        {
            // Arrange
            List<CountryDto> countryDtos = new List<CountryDto>();
            countryDtos.Add(new CountryDto { Name = "Netherlands", Id= 1 });
            countryDtos.Add(new CountryDto { Id = 2, Name = "Poland" });

            mockCustomerRepo.Setup(obj => obj.GenerateIdentityAsync().Result).Returns(2);
            mockCountryRepo.Setup(obj => obj.GetAllAsync().Result).Returns(countryDtos);
            
            var customerService = new CustomerService(logger, mockCustomerRepo.Object, mockAddressRepo.Object, mockCountryRepo.Object);

            var newCustomer = new Customer() { FullName = "John Smith", Street = "Pond Lane", City = "Amsterdam", Postcode = "AM66", Country = "Netherlands" };
            // Act
            var response = await customerService.CreateCustomer(newCustomer);

            // Assert
            Assert.AreEqual(response,2);
        }
    }
}