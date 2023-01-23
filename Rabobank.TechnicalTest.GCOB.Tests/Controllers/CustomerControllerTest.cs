using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using Rabobank.TechnicalTest.GCOB.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Services;
using Rabobank.TechnicalTest.GCOB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services
{
    [TestClass]
    public class CustomerControllerTest
    {
        private ILogger<CustomerController> logger;
        private Mock<ICustomerService> mockCustomerService;
        private Customer newCustomer;

        [TestInitialize]
        public void Initialize()
        {
            var mock = new Mock<ILogger<CustomerController>>();
            logger = mock.Object;

            mockCustomerService = new Mock<ICustomerService>();

            newCustomer = new Customer() { FullName = "John Smith", Street = "Pond Lane", City = "Amsterdam", Postcode = "AM66", Country = "Netherlands" };

        }


        [TestMethod]
        public async Task GivenHaveACustomer_AndICallAServiceToGetTheCustomer_ThenTheCustomerIsReturned()
        {
            // Note SUT is the controller, although, in this scenario it contains little logic.

            // Arrange
            mockCustomerService.Setup(obj => obj.GetCustomerById(1).Result).Returns(newCustomer);

            var customerController = new CustomerController(mockCustomerService.Object, logger);

            // Act
            var response = await customerController.GetCustomerById(1);

            var okObjectResult = (OkObjectResult)response.Result;
            
            // Assert
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(((Customer)okObjectResult.Value).FullName, "John Smith");

        }
    }
}