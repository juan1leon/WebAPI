using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PersonsController : ApiController
    {
        List<Person> Persons = new List<Person>()
        {
           new Person() { id = 1, FirstName = "Juan", LastName = "León" },
           new Person() { id = 2, FirstName = "Jose", LastName = "Portillo" },
           new Person() { id = 3, FirstName = "Jacqueline", LastName = "Perez" }
        };

        public IEnumerable<Person> GetAllPersons()
        {
            return Persons;
        }

        public IHttpActionResult GetPerson(int id)
        {
            var person = Persons.FirstOrDefault((p) => p.id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public IEnumerable<Person> AddPersons([FromBody] Person item)
        {
            Persons.Add(item);

            return Persons;
        }

        [HttpPut]
        public IEnumerable<Person> UpdatePerson([FromBody] Person item)
        {
            Persons.Where(a => a.id == item.id).First().FirstName = item.FirstName;
            Persons.Where(a => a.id == item.id).First().LastName = item.LastName;

            return Persons.Where(a => a.id == item.id);
        }

        [HttpDelete]
        public IHttpActionResult DelPerson([FromBody] Person item)
        {
            Persons.RemoveAt(item.id);

            return Content(HttpStatusCode.NoContent, "");
        }
    }
}
