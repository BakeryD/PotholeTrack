using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Account;

namespace WebApplication.Web.DAL
{
	public class PotholeReportDAL : IPotholeDAL
	{

		private readonly string connectionString;

		public PotholeReportDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}

		/// <summary>
		/// Saves the new report to the database.
		/// </summary>
		/// <param name="report"></param>
		public void CreateReport(Report report)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd =
						new SqlCommand(
							"INSERT INTO users VALUES (@submitter, @datecreated, @location, @dateinspected, @severity, @daterepaired, @status, @reportcount, @description);",
							conn);
					cmd.Parameters.AddWithValue("@submitter", report.Submitter);
					cmd.Parameters.AddWithValue("@datecreated", report.DateCreated);
					cmd.Parameters.AddWithValue("@location", report.Location);
					cmd.Parameters.AddWithValue("@dateinspected", report.DateInspected);
					cmd.Parameters.AddWithValue("@severity", report.Severity);
					cmd.Parameters.AddWithValue("@daterepaired", report.DateRepaired);
					cmd.Parameters.AddWithValue("@status", report.Status);
					cmd.Parameters.AddWithValue("@reportcount", report.ReportCount);
					cmd.Parameters.AddWithValue("@description", report.Description);

					cmd.ExecuteNonQuery();

					return;
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}

		}


		/// <summary>
		/// Returns the report.
		/// </summary>
		/// <param name="reportId"></param>
		public Report GetReport(int reportId)
		{
			Report report = null;
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM USERS WHERE reportId = @reportid;", conn);
					cmd.Parameters.AddWithValue("@reportid", reportId);

					SqlDataReader reader = cmd.ExecuteReader();

					if (reader.Read())
					{
						report = MapRowToReport(reader);
					}
				}
			}

			catch (SqlException ex)
			{
				throw ex;
			}

			return report;
		}

		/// <summary>
		/// Updates the report.
		/// </summary>
		/// <param name="report"></param>
		public void UpdateReport(Report report)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd =
						new SqlCommand(
							"UPDATE users SET password = @password, salt = @salt, role = @role, phonenumber = @phonenumber WHERE id = @id;",
							conn);
					cmd.Parameters.AddWithValue("@location", report.Location);
					cmd.Parameters.AddWithValue("@dateinspected", report.DateInspected);
					cmd.Parameters.AddWithValue("@severity", report.Severity);
					cmd.Parameters.AddWithValue("@daterepaired", report.DateRepaired);
					cmd.Parameters.AddWithValue("@status", report.Status);
					cmd.Parameters.AddWithValue("@reportcount", report.ReportCount);
					cmd.Parameters.AddWithValue("@description", report.Description);

					cmd.ExecuteNonQuery();

					return;
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}


		}


		/// <summary>
		/// Maps the entire row a single report object.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns>A report object<returns>
		private Report MapRowToReport(SqlDataReader reader)
		{
			return new Report()
			{
				Id = Convert.ToInt32(reader["id"]),
				Submitter = Convert.ToString(reader["submitter"]),
				DateCreated = Convert.ToString(reader["datecreated"]),
				Location = Convert.ToDecimal(reader["location"]),
				DateInspected = Convert.ToInt32(reader["dateinspected"]),
				Severity = Convert.ToInt32(reader["severity"]),
				DateRepaired = Convert.ToInt32(reader["daterepaired"]),
				Status = Convert.ToInt32(reader["status"]),
				ReportCount = Convert.ToInt32(reader["reportcount"]),
				Description = Convert.ToString(reader["description"])
			};
		}

		
	}

}
