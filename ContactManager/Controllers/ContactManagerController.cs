using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactManagerController : ControllerBase
    {
        private readonly ContactDbContext _context;
        public ContactManagerController(ContactDbContext contactDbContext)
        {
            _context = contactDbContext;
        }
        [HttpPost("add_contact")]
        public IActionResult AddContact([FromBody] ContactManagerModel contactObj)
        {
            if (contactObj == null)
            {
                return BadRequest();
            }
            else
            {
                contactObj.CreationTimestamp = DateTime.UtcNow.AddDays(1);
                contactObj.LastChangeTimestamp = DateTime.UtcNow.AddDays(1);
                _context.contactManagerModel.Add(contactObj);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Contact Added Successfully"

                });
            }
        }
        [HttpPut("update_contact")]
        public IActionResult UpdateContact([FromBody] ContactManagerModel contactObj)
        {
            try { 
            if (contactObj == null)
            {
                return BadRequest();
            }
                contactObj.LastChangeTimestamp = DateTime.UtcNow.AddDays(1);
                var user = _context.contactManagerModel.AsNoTracking().FirstOrDefault(x => x.ID == contactObj.ID);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"

                });
            }
            else
            {
                _context.Entry(contactObj).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Contact Updatded Successfully"
                });
            }
        }
            catch(Exception ex)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Invalid Email"

                });
            }
        }

        [HttpDelete("delete_contact/{id}")]
        public IActionResult DeleteContact(int id)
        {
            var user = _context.contactManagerModel.Find(id);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"

                });
            }
            else
            {
                _context.Remove(user);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Contact Deleted Successfully"
                });
            }
        }

        [HttpGet("get_all_contacts")]
        public IActionResult GetAllContacts()
        {
             var contacts = _context.contactManagerModel.AsQueryable();
            return Ok(new
            {
                StatusCode = 200,
                ContactDetail = contacts
            });
        }

        [HttpGet("get_employees/id")]
        public IActionResult GetContacts(int id)
        {
            var contacts = _context.contactManagerModel.Find(id);
            if (contacts == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"

                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    ContactDetail = contacts
                });
            }

        }
    }
}