using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Account;

namespace WebApplication.Web.DAL
{
   public interface IClaimDAL
    {
	    /// <summary>
	    /// Retrieves a claim from the system by claimId.
	    /// </summary>
	    /// <param name="claimId"></param>
	    /// <returns>The report for this pothole</returns>
	    Claim GetClaim(int claimId);

		/// <summary>
		/// Creates a new claim.
		/// </summary>
		/// <param name="claim"></param>
		void CreateClaim(Claim claim);

	    /// <summary>
	    /// Updates a claim.
	    /// </summary>
	    /// <param name="claimId"></param>
	    void UpdateClaim(Claim claimId);

	}
}
