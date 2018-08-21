﻿using System;
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

        [HttpPost]
        [Route("update")]
        public ActionResult UpdateReport(Report updatedReport)
        {
            //Passing in the JSON as a Report parameter

            Report existingReport = dal.GetReport(updatedReport.Id);
            if (updatedReport.DateInspected != existingReport.DateInspected &&
                updatedReport.DateInspected != DateTime.Today)
            {
                existingReport.DateInspected = updatedReport.DateInspected;
            }
            if (updatedReport.DateRepaired != existingReport.DateRepaired &&
                updatedReport.DateRepaired != DateTime.Today)
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
            dal.UpdateReport(existingReport);
            return Ok();
        }
    }
}