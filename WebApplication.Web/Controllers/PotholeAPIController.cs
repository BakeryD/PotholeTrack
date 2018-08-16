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
	    /// Creates a new report in the system.
	    /// </summary>
	    /// <param name="report"></param>
	    /// <returns></returns>
	    [HttpPost]
        [Route("create")]
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

        /// <summary>
        /// Creates a new report in the system.
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public ActionResult AddCount(Report report)
        {
            report = dal.GetReport(report.Id);
            Console.WriteLine("hello2");
            report.Submitter = auth.GetCurrentUser().Id;
            dal.AddReport(report);

            return Ok();

            // Return a created at route to indicate where the resource can be found
            //return Ok(); //;"GetReport", null);//, new { id = report.Id }, report);
        }

    }
}