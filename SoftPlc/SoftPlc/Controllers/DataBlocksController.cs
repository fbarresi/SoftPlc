using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftPlc.Interfaces;
using SoftPlc.Models;

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
        public IEnumerable<DatablockDescription> Get()
        {
	        return plcService.GetDatablocksInfo();
        }

        // GET api/datablocks/5
        [HttpGet("{id}")]
        public DatablockDescription Get(int id)
        {
	        return plcService.GetDatablock(id);
        }

        // POST api/datablocks
        [HttpPost]
        public void Post(int id, int size)
        {
	        plcService.AddDatablock(id, size);
        }

		// PUT api/datablocks/5
		[HttpPut("{id}")]
        public void Put(int id, [FromBody]byte[] data)
        {
	        plcService.UpdateDatablockData(id, data);
        }

        // DELETE api/datablocks/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
	        plcService.RemoveDatablock(id);
        }
    }
}
