using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarlabsCRUD.Data;
using StarlabsCRUD.Models;
using StarlabsCRUD.Services;

namespace StarlabsCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly PersonService _personService;

        public PersonController(DataContext context, PersonService personService)
        {
            _context = context;
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Person.ToListAsync();
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            try
            {
                _personService.AddPerson(person);
                return Ok("Added Successfully");
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{personId}")]
        public IActionResult UpdatePerson(int personId, Person updatedPerson)
        {
            try
            {
                _personService.UpdatePerson(personId, updatedPerson);
                return Ok("Updated Successfully");
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
            catch (ApplicationException appException)
            {
                // Log the exception or handle it in an appropriate way
                return StatusCode(500, appException.Message);
            }
        }

        [HttpDelete("{personId}")]
        public IActionResult DeletePerson(int personId)
        {
            try
            {
                _personService.DeletePerson(personId);
                return Ok("Deleted");
            }
            catch (ApplicationException appException)
            {
                // Log the exception or handle it in an appropriate way
                return StatusCode(500, appException.Message);
            }
        }


    }
}
