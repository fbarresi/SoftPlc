using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftPlc.Interfaces;
using SoftPlc.Models;

namespace SoftPlc.Controllers
{
	/// <inheritdoc />
	[Route("api/[controller]")]
	public class DataBlocksController : Controller
    {
	    private readonly IPlcService plcService;

	    /// <inheritdoc />
	    public DataBlocksController(IPlcService plcService)
	    {
		    this.plcService = plcService;
	    }
		/// <summary>
		/// Get the datablocks information of the current soft plc instance
		/// </summary>
		/// <returns></returns>
        // GET api/datablocks
        [HttpGet]
        public IEnumerable<DatablockDescription> Get()
        {
	        return plcService.GetDatablocksInfo();
        }
		/// <summary>
		/// Get the actual datablock configuration
		/// </summary>
		/// <param name="id">The datablock id</param>
		/// <returns></returns>
        // GET api/datablocks/5
        [HttpGet("{id}")]
        public DatablockDescription Get(int id)
        {
	        return plcService.GetDatablock(id);
        }
		/// <summary>
		/// Create a new datablock
		/// </summary>
		/// <param name="id">The datablock id</param>
		/// <param name="size">The datablock size</param>
        // POST api/datablocks
        [HttpPost]
        public void Post(int id, int size)
        {
	        plcService.AddDatablock(id, size);
        }
		/// <summary>
		/// Update the content of a datablock
		/// </summary>
		/// <param name="id">The datablock id</param>
		/// <param name="data">The datablock content in form of array of bytes</param>
		// PUT api/datablocks/5
		[HttpPut("{id}")]
        public void Put(int id, [FromBody]byte[] data)
        {
	        plcService.UpdateDatablockData(id, data);
        }
		/// <summary>
		/// Delete a datablock from the current plc instance
		/// </summary>
		/// <param name="id">The datablock id</param>
        // DELETE api/datablocks/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
	        plcService.RemoveDatablock(id);
        }
    }
}
