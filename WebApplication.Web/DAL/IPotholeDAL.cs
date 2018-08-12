using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models.Account;

namespace WebApplication.Web.DAL
{
    interface IPotholeDAL
    {
	    /// <summary>
	    /// Retrieves a user from the system by username.
	    /// </summary>
	    /// <param name="reportId"></param>
	    /// <returns>The report for this pothole</returns>
	    Report GetReport(string reportId);

	    /// <summary>
	    /// Creates a new report.
	    /// </summary>
	    /// <param name="reportId"></param>
	    void CreateReport(Report reportId);

		/// <summary>
		/// Updates a report.
		/// </summary>
		/// <param name="reportId"></param>
		void UpdateReport(Report reportId);

	   

	}
}
