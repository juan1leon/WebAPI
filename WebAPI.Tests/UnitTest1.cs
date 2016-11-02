using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using WebAPI.Models;
using System.Web.Http.Results;
using System.Web.Http;
using System.Net;
using System.Net.Http;

namespace WebAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        List<Person> testPersons = new List<Person>()
        {
           new Person() { id = 1, FirstName = "Juan", LastName = "León" },
           new Person() { id = 2, FirstName = "Jose", LastName = "Portillo" },
           new Person() { id = 3, FirstName = "Jacqueline", LastName = "Perez" }
        };

        [TestMethod]
        public void GetPerson()
        {
            var controller = new PersonsController();

            var result = controller.GetPerson(1) as OkNegotiatedContentResult<Person>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testPersons[0].FirstName, result.Content.FirstName);
        }

        [TestMethod]
        public void GetAllPersons_test()
        {
            var controller = new PersonsController();

            var result = controller.GetAllPersons() as List<Person>;
            Assert.AreEqual(testPersons.Count, result.Count);
        }

        [TestMethod]
        public void AddPersons_test()
        {
            var controller = new PersonsController();

            var result = controller.AddPersons(new Person { id = 4, FirstName = "Agregado", LastName = "Agregado" }) as List<Person>;
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void UpdatePerson_test()
        {
            var controller = new PersonsController();

            var result = controller.UpdatePerson( new Person { id = 2, FirstName = "Updated", LastName = "Updated" }) as List<Person>;
            Assert.AreNotEqual(testPersons.Find(a => a.id == 2).FirstName, result.Find(a => a.id == 2).FirstName);
        }

        [TestMethod]
        public void DelPerson_test()
        {
            var controller = new PersonsController();
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage();

            HttpResponseMessage result = controller.DelPerson(new Person { id = 1, FirstName = "", LastName = "" });

            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }
    }
}
