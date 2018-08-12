using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class Claim
    {
	    /// <summary>
	    /// The claim's id.
	    /// </summary>
	    [Required]
	    public int Id { get; set; }

	    /// <summary>
	    /// The submitter's user ID.
	    /// </summary>
	    [Required]
	    public int Submitter { get; set; }

	    /// <summary>
	    /// The pothole record ID
	    /// </summary>
	    [Required]
	    public int PotholeRecord { get; set; }

	    /// <summary>
	    /// The date the claim is submitted.
	    /// </summary>
	    [Required]
	    public DateTime DateSubmitted { get; set; }

		/// <summary>
		/// The status of the claim.
		/// </summary>
		[Required]
		public bool Status { get; set; }

	    /// <summary>
	    /// The description of the claim.
	    /// </summary>
	    [Required]
	    public string Description { get; set; }

	}
}
