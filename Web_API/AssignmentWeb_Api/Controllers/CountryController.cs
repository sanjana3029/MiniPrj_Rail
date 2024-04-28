using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssignmentWeb_Api.Models;

namespace AssignmentWeb_Api.Controllers
{
    [RoutePrefix("api/country")]

    public class CountryController : ApiController
    {
        static List<Country> countries = new List<Country>()
        {
            new Country { ID = 1, CountryName = "India", Capital = "New Delhi" },
             new Country { ID = 2, CountryName = "Germany", Capital = "Berlin" },
             new Country { ID = 3, CountryName = "Japan", Capital = "Tokyo" },
             new Country { ID = 4, CountryName = "Canada", Capital = "Ottawa" }
        };

        [HttpGet]
        [Route("All")]
        public IEnumerable<Country>Get()
        {
            return countries;
        }

        // Get by Id

        [HttpGet]
        [Route("ById/{cid}")]
        public IHttpActionResult CountryById(int cid)
        {
            var country = countries.Find(c=>c.ID==cid);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        //Get by Name

        [HttpGet]
        [Route("GetName/{cid}")]

        public IHttpActionResult GetCountryByName(int cid)
        {
            string cname = countries.Where(c => c.ID == cid).SingleOrDefault()
                ?.CountryName;
            if(cname==null)
            {
                return NotFound();
            }
            return Ok(cname);
        }


        //Post
        // POST: api/AllPost

        [HttpPost]
        [Route("AllPost")]
        public IHttpActionResult PostAll([FromBody] Country country)
        {
            countries.Add(country);
            return Ok(countries);
        }

        // POST: api/Create======================================
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateCountryPOST([FromUri] int Id, string Countryname, string capital)
        {
            Country country = new Country
            {
                ID = Id,
                CountryName = Countryname,
                Capital = capital
            };
            countries.Add(country);
            return Ok(countries);
        }

        //Updation

        // PUT: api/Update/{cid}

        [HttpPut]
        [Route("Update/{cid}")]

        public void UpdatePut (int cid,[FromUri] Country c)
        {
            countries[cid - 1] = c;
        }

        //Deletion

        [HttpDelete]
        [Route("Delete/{cid}")]
        public void DeleteCountry(int cid)
        {
            countries.RemoveAt(cid - 1);
        }

    }
}
