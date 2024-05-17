using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerInfoController : ControllerBase
    {
        private static List<CustomerInfo> customerInfos = new List<CustomerInfo>()
        {
            new CustomerInfo { 
                Id = 1, 
                Password = "examplePassword1", 
                Username = "exampleUsername1", 
                Name = "Gian" 
            },

            new CustomerInfo {
                Id = 2, 
                Password ="examplePassword",
                Username = "exampleUsername2",
                Name = "Karlo" 
            },
            new CustomerInfo
            {
                Id = 3,
                Password = "examplePassword",
                Username = "exampleUsername3",
                Name = "Becera"
            }
        };

        public class CustomerInfo
        {
            public int Id { get; set; }
            public string ?Password { get; set; }
            public string ?Username { get; set; }
            public string ?Name { get; set; }
        }

        // GET
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(customerInfos);
        }

        // POST
        [HttpPost]
        public IActionResult Create(CustomerInfo customerInfo)
        {
            customerInfos.Add(customerInfo);
            return CreatedAtAction(nameof(GetById), new { id = customerInfo.Id }, customerInfo);
        }

        // GET ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customerInfo = customerInfos.Find(c => c.Id == id);
            if (customerInfo == null)
            {
                return NotFound();
            }
            return Ok(customerInfo);
        }

        // INSERT
        [HttpPost("insert")]
        public IActionResult Insert(CustomerInfo customerInfo)
        {
            var existingCustomerID = customerInfos.Find(c => c.Id == customerInfo.Id);
            var existingCustomerPassword = customerInfos.Find(c => c.Password == customerInfo.Password);
            var existingCustomerUser = customerInfos.Find(c => c.Username == customerInfo.Username);

            if (existingCustomerID != null)
            {
                return Conflict("ID already exists.");
            }

            if (existingCustomerPassword != null)
            {
                return Conflict("Password already exists.");
            }

            if (existingCustomerUser != null)
            {
                return Conflict("Username already exists.");
            }

            customerInfos.Add(customerInfo);
            return CreatedAtAction(nameof(GetById), new { id = customerInfo.Id }, customerInfo);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customerInfo = customerInfos.Find(c => c.Id == id); 
            if (customerInfo == null)
            {
                return NotFound();
            }
            customerInfos.Remove(customerInfo);
            return NoContent();
        }
    }
}
