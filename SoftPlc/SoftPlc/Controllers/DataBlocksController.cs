using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftPlc.Interfaces;

namespace SoftPlc.Controllers
{
    [Route("api/[controller]")]
    public class DataBlocksController : Controller
    {
	    private readonly IPlcService plcService;

	    public DataBlocksController(IPlcService plcService)
	    {
		    this.plcService = plcService;
	    }

        // GET api/datablocks
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/datablocks/5
        [HttpGet("{id}")]
        public byte[] Get(int id)
        {
            return new byte[10];
        }

        // POST api/datablocks
        [HttpPost]
        public void Post([FromBody]string number, [FromBody]int size, [FromBody]byte[] data)
        {
        }

        // PUT api/datablocks/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]byte[] data)
        {
        }

        // DELETE api/datablocks/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
