using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL;
using WebApplication.Web.Models.Account;


namespace WebApplication.Web.Controllers
{
    [Route("api/record")]
    [ApiController]
    public class PotholeAPIController : ControllerBase
    {
	    // Create our dependency variable
	    private readonly IPotholeDAL dal;

	    public PotholeAPIController(IPotholeDAL dal)
	    {
		    this.dal = dal;
	    }

	    /// <summary>
	    /// Creates a new city in the system.
	    /// </summary>
	    /// <param name="report"></param>
	    /// <returns></returns>
	    [HttpPost]
	    public ActionResult Create(Report report)
	    {
		    dal.CreateReport(report);

		    // Return a created at route to indicate where the resource can be found
		    return CreatedAtRoute("GetReport", new { id = report.Id }, report);
	    }

	}
}