using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly CustomerContext _customerContext;

        public CustomersController(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        
        // GET api/customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(long id)
        {
            var item = _customerContext.Customers.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _customerContext.Customers.Add(customer);
            _customerContext.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Customer item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var customer = _customerContext.Customers.FirstOrDefault(t => t.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.FirstName = item.FirstName;
            customer.LastName = item.LastName;
            customer.DateOfBirth = item.DateOfBirth;
            customer.Email = item.Email;
            customer.PhoneNumber = item.PhoneNumber;

            _customerContext.Customers.Update(customer);
            _customerContext.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _customerContext.Customers.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _customerContext.Customers.Remove(todo);
            _customerContext.SaveChanges();
            return new NoContentResult();
        }
    }
}
