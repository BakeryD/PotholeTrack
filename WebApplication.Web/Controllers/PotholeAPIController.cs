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
	    // Create our dependency variables
	    private readonly IPotholeDAL dal;
	    private readonly IAuthProvider auth;

        /// <summary>
        /// When this api is instantiated, give it access to the reportDAL and our authorization provider via dependency injection
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="auth"></param>
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
		    report.Submitter = auth.GetCurrentUser().Id;
            report.DateCreated = DateTime.Now;
		   int id= dal.CreateReport(report);
            report = dal.GetReport(id);
            dal.AddReport(report);


            return Ok();
	    }

        /// <summary>
        /// Increments the report count of a report in the system.
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddReport")]
        public ActionResult AddCount(Report report)
        {
            report = dal.GetReport(report.Id);
            if (!(report.Submitter == auth.GetCurrentUser().Id))
            {
                report.Submitter = auth.GetCurrentUser().Id;
                dal.AddReport(report);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }

        }

        /// <summary>
        /// Updates the status, dates of inspection/repair for an existing pothole report
        /// </summary>
        /// <param name="updatedReport"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public ActionResult UpdateReport(Report updatedReport)
        {
            //Passing in the JSON as a Report parameter

            //Get the report as is in the database
            Report existingReport = dal.GetReport(updatedReport.Id);

            //Copy the appropriate fields if they were changed
            if (updatedReport.DateInspected != existingReport.DateInspected &&
                updatedReport.DateInspected > DateTime.Today)
            {
                existingReport.DateInspected = updatedReport.DateInspected;
            }
            if (updatedReport.DateRepaired != existingReport.DateRepaired &&
                updatedReport.DateRepaired > DateTime.Today &&
                updatedReport.DateRepaired != existingReport.DateCreated &&
                updatedReport.DateRepaired > updatedReport.DateInspected)
            {
                existingReport.DateRepaired = updatedReport.DateRepaired;
            }
            if (!String.IsNullOrEmpty(updatedReport.Description))
            {
                existingReport.Description = updatedReport.Description;

            }
            if (updatedReport.Status != existingReport.Status)
            {
                existingReport.Status = updatedReport.Status;
            }

            existingReport.Severity = updatedReport.Severity;
            //Save changes
            dal.UpdateReport(existingReport);
            return Ok();
        }
    }
}