﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckPromise.Data.DataContext;
using CheckPromise.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheckPromise.Webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public CheckPromiseContext Context { get; set; }
        public ValuesController(CheckPromiseContext context)
        {
            Context = context;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Promise>> Get()
        {
            return Context.Promise.ToList();
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
