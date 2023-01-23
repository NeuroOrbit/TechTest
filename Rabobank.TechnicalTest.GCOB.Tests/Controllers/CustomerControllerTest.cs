using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using Rabobank.TechnicalTest.GCOB.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Services;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services
{
    [TestClass]
    public class CustomerControllerTest
    {
        private ILogger<CustomerController> logger;

        [TestInitialize]
        public void Initialize()
        {
            var mock = new Mock<ILogger<CustomerController>>();
            logger = mock.Object;



        }


        [TestMethod]
        public async Task GivenHaveACustomer_AndICallAServiceToGetTheCustomer_ThenTheCustomerIsReturned()
        {
            // Arrange

        }
    }
}