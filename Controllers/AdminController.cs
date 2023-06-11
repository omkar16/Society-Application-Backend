using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyGate.Models;


namespace MyGate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        MyGateContext db = new MyGateContext();
        public class Validation {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        [Route("validate")]
 
        public IActionResult PostAdmin(Validation requested_data)
        {
            Console.WriteLine(requested_data.Email);
            //string token = Convert.ToBase64String(time.Concat(key).ToArray());
            var data = db.Admins.Where(admin => admin.Email == requested_data.Email && admin.Password == requested_data.Password);
            if (data != null) return Ok(data);
            return null;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAdmin() {
            var data = db.Admins.ToList();
            return Ok(data);
        }
    }
}
