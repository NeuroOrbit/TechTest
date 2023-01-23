using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Repositories;
using Rabobank.TechnicalTest.GCOB.Services;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        private ILogger<InMemoryCustomerRepository> logger;
        private CustomerDto customerDto;

        [TestInitialize]
        public void Initialize()
        {
            var mock = new Mock<ILogger<InMemoryCustomerRepository>>();
            logger = mock.Object;

            customerDto = new CustomerDto() { Id = 1, FirstName = "Keith", LastName = "Wright", AddressId = 1 };

        }


        [TestMethod]
        public async Task GivenHaveACustomer_AndIGetTheCustomerFromTheDB_ThenTheCustomerIsRetrieved()
        {
            // Arrange
            var customerRepo = new InMemoryCustomerRepository(logger);

            await customerRepo.InsertAsync(customerDto);

            // Act
            var response = await customerRepo.GetAsync(1);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Id, 1);
            Assert.AreEqual(response.FirstName, "Keith");

        }
    }
}
