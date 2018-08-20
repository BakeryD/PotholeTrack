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
        /// <returns>The ID of the report after it has been saved in the database.</returns>
		public int CreateReport(Report report)
		{
            int newestId;
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd =
						new SqlCommand(
                            $"INSERT INTO records(submitter, datecreated, severity, lattitude, longitude, status, reportcount, reportnumber)" +
                            $" VALUES (@submitter, @datecreated, @severity, @lattitude, @longitude, @status, @reportcount, @reportnumber); Select Max(id) from records;",
							conn);
					cmd.Parameters.AddWithValue("@submitter", report.Submitter);
					cmd.Parameters.AddWithValue("@datecreated", report.DateCreated);
					cmd.Parameters.AddWithValue("@longitude", report.Longitude);
					cmd.Parameters.AddWithValue("@lattitude", report.Lattitude);
                    cmd.Parameters.AddWithValue("@status", report.Status);
					cmd.Parameters.AddWithValue("@reportcount", report.ReportCount);
					cmd.Parameters.AddWithValue("@reportnumber", report.ReportNumber);
                    cmd.Parameters.AddWithValue("@severity", report.Severity);

                     newestId= Convert.ToInt32(cmd.ExecuteScalar());

				}
                return newestId;
            }
            catch (SqlException ex)
			{
				throw ex;
			}

		}

        public IList<Report> GetAllReports()
        {
            var reports = new List<Report>();
            string sql = "Select * From records;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand(sql, conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        reports.Add(MapRowToReport(reader));
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            return reports;
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
					SqlCommand cmd = new SqlCommand("SELECT * FROM records WHERE id = @reportid;", conn);
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
		/// Adds a count to an existing report.
		/// </summary>
		/// <param name="report"></param>
		public void AddReport(Report report)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand(
                        $"Begin Try " +
                        $"Insert Into user_records (user_id, record_id) Values (@user, @reportid); " +
                        $"Update records Set reportcount = reportcount + 1 Where id = @reportid;" +
                        $"End Try " +
                        $"Begin Catch " +
                        $"End Catch;", conn);
					cmd.Parameters.AddWithValue("@reportid", report.Id);
					cmd.Parameters.AddWithValue("@user", report.Submitter);

					cmd.ExecuteNonQuery();
					
				}
				return;
			}

			catch (SqlException ex)
			{
				throw ex;
			}
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
							$"UPDATE reports SET longitude = @longitude," +
                            $" lattitude = @lattitude," +
                            $" dateinspected = @dateinspected," +
                            $" severity = @severity," +
                            $" daterepaired = @daterepaired," +
                            $" status = @status," +
                            $" reportcount = @reportcount," +
                            $" description = @description" +
							$" reportnumber = @reportnumber" +
                            $" WHERE id = @id;",
							conn);
					cmd.Parameters.AddWithValue("@longitude", report.Longitude);
					cmd.Parameters.AddWithValue("@lattitude", report.Lattitude);
                    cmd.Parameters.AddWithValue("@dateinspected", report.DateInspected);
					cmd.Parameters.AddWithValue("@severity", report.Severity);
					cmd.Parameters.AddWithValue("@daterepaired", report.DateRepaired);
					cmd.Parameters.AddWithValue("@status", report.Status);
					cmd.Parameters.AddWithValue("@reportcount", report.ReportCount);
					cmd.Parameters.AddWithValue("@description", report.Description);
					cmd.Parameters.AddWithValue("@reportnumber", report.ReportNumber);

					cmd.ExecuteNonQuery();

				}
                return;
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
				Submitter = Convert.ToInt32(reader["submitter"]),
				DateCreated = Convert.ToDateTime(reader["datecreated"]),
				Lattitude = Convert.ToDecimal(reader["lattitude"]),
				Longitude = Convert.ToDecimal(reader["longitude"]),
                DateInspected = Convert.ToDateTime(reader["dateinspected"]),
				Severity = Convert.ToInt32(reader["severity"]),
				DateRepaired = Convert.ToDateTime(reader["repairdate"]),
				Status = Convert.ToInt32(reader["status"]),
				ReportCount = Convert.ToInt32(reader["reportcount"]),
				Description = Convert.ToString(reader["description"]),
				ReportNumber = Convert.ToString(reader["reportnumber"])
			};
		}

		
	}

}
