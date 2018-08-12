﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models.Account
{
    public class Report
    {

	    /// <summary>
	    /// The report's id.
	    /// </summary>
	    [Required]
	    public int Id { get; set; }

	    /// <summary>
	    /// The report's submitter.
	    /// </summary>
	    [Required]
	    public string Submitter { get; set; }

	    /// <summary>
	    /// The report's creation date.
	    /// </summary>
	    [Required]
	    public string DateCreated { get; set; }

		/// <summary>
	    /// The pothole location.
	    /// </summary>
	    [Required]
	    public float Location { get; set; }

		/// <summary>
		/// The date inspected by city employee.
		/// </summary>
		[Required]
	    public int DateInspected { get; set; }

		/// <summary>
		/// The date repaired by city employee.
		/// </summary>
		[Required]
	    public int DateRepaired { get; set; }

		/// <summary>
		/// The pothole severity.
		/// </summary>
		[Required]
	    public int Severity { get; set; }
		
	    /// <summary>
	    /// The pothole status.
	    /// </summary>
	    [Required]
	    public int Status { get; set; }

	    /// <summary>
	    /// How many times this pothole has been reported.
	    /// </summary>
	    [Required]
	    public int ReportCount { get; set; }

	    /// <summary>
	    /// The pothole description (if any).
	    /// </summary>
	    [Required]
	    [MaxLength(244)]
		public string Description { get; set; }
	}
}

