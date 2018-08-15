using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL;
using WebApplication.Web.Models.Account;
using WebApplication.Web.Providers.Auth;


namespace WebApplication.Web.Controllers
{
    [Route("api/record")]
    [ApiController]
    public class PotholeAPIController : ControllerBase
    {
	    // Create our dependency variable
	    private readonly IPotholeDAL dal;
	    private readonly IAuthProvider auth;

	    public PotholeAPIController(IPotholeDAL dal, IAuthProvider auth)
	    {
			this.dal = dal;
		    this.auth = auth;
	    }

	    /// <summary>
	    /// Creates a new city in the system.
	    /// </summary>
	    /// <param name="report"></param>
	    /// <returns></returns>
	    [HttpPost]
	    public ActionResult Create(Report report)
	    {
			Console.WriteLine("hello");
		    report.Submitter = auth.GetCurrentUser().Id;
            report.DateCreated = DateTime.Now;
		    dal.CreateReport(report);

			return Ok();

		    // Return a created at route to indicate where the resource can be found
		    //return Ok(); //;"GetReport", null);//, new { id = report.Id }, report);
	    }

	}
}