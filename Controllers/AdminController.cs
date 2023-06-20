using Azure.Messaging;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyGate.Models;


namespace MyGate.Controllers
{
    public class Validation
    {
        //public string? FirstName { get; set; }
        //public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string Contact { get; set; }
        //public string Token { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        MyGateContext db = new MyGateContext();

        [HttpPost]
        [Route("validate")]
        public IActionResult PostAdmin(Validation requested_data)
        {
            //Console.WriteLine(requested_data.Email);
            //string token = Convert.ToBase64String(time.Concat(key).ToArray());
            if (ModelState.IsValid) {
                try
                {
                    var data = db.Admins.Where(admin => admin.Email == requested_data.Email).FirstOrDefault();
                    if (data != null)
                    {
                        if (data.Password == requested_data.Password) return Ok(data);
                        else return NotFound("Password is incorrect. Please enter a correct password..!");
                    }
                    else return NotFound("Email not registered. Please create an account first");
                }
                catch (Exception ex) {
                    return BadRequest(ex.InnerException.Message);
                }
            }
            return BadRequest("Something went wrong..!");
        }
           

        [HttpGet]
        [Route("list")]
        public IActionResult GetAdmin() {
            var data = db.Admins.FirstOrDefault();
            return Ok(data);
        }
    }
}
