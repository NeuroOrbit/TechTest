using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        /// <summary>
        /// CustomerController : CTOR
        /// </summary>
        /// <param name="customerService"></param>
        /// <param name="logger"></param>
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }


        /// <summary>
        /// GetCustomerById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);

            return Ok(customer);
        }


        /// <summary>
        /// CreateCustomer
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="postcode"></param>
        /// <param name="country"></param>
        /// <returns>New Customer Id</returns>
        [HttpPost("{fullName}/{street}/{city}/{postcode}/{country}")]
        public async Task<ActionResult<int>> CreateCustomer(string fullName,
            string street, string city, string postcode, string country)
        {
            var customerToCreate = new Customer() {FullName = fullName, Street = street, City = city, Postcode = postcode, Country = country};

            var newCustomer = await _customerService.CreateCustomer(customerToCreate);

            return Ok(newCustomer);
        }


    }//end class
}//end ns
