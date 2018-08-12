using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models.Account;

namespace WebApplication.Web.DAL
{
    public interface IPotholeDAL
    {
	    /// <summary>
	    /// Retrieves a report from the system by reportId.
	    /// </summary>
	    /// <param name="reportId"></param>
	    /// <returns>The report for this pothole</returns>
	    Report GetReport(int reportId);

	    /// <summary>
	    /// Creates a new report.
	    /// </summary>
	    /// <param name="report"></param>
	    void CreateReport(Report report);

		/// <summary>
		/// Updates a report.
		/// </summary>
		/// <param name="reportId"></param>
		void UpdateReport(Report reportId);

        /// <summary>
        /// Returns a list of all recorded potholes
        /// </summary>
        /// <returns></returns>
        IList<Report> GetAllReports();

	}
}
