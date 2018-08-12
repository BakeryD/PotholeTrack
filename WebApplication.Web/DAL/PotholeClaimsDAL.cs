using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Account;

namespace WebApplication.Web.DAL
{
    public class PotholeClaimsDAL : IClaimDAL
    {
	    private readonly string connectionString;

		public PotholeClaimsDAL(string connectionString)
	    {
		    this.connectionString = connectionString;
	    }

		/// <summary>
		/// Saves the new claim to the database.
		/// </summary>
		/// <param name="claim"></param>
		public void CreateClaim(Claim claim)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd =
						new SqlCommand(
							"INSERT INTO CLAIMS VALUES (@submitter, @potholeid, @datesubmitted, @status);",
							conn);
					cmd.Parameters.AddWithValue("@submitter", claim.Submitter);
					cmd.Parameters.AddWithValue("@potholeid", claim.PotholeRecord);
					cmd.Parameters.AddWithValue("@datesubmitted", claim.DateSubmitted);
					cmd.Parameters.AddWithValue("@status", claim.Status);
					cmd.Parameters.AddWithValue("@description", claim.Description);

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
		/// Returns the claim.
		/// </summary>
		/// <param name="claimId"></param>
		public Claim GetClaim(int claimId)
		{
			Claim claim = null;
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM CLAIMS WHERE id = @claimid;", conn);
					cmd.Parameters.AddWithValue("@claimid", claimId);

					SqlDataReader reader = cmd.ExecuteReader();

					if (reader.Read())
					{
						claim = MapRowToClaim(reader);
					}
				}
			}

			catch (SqlException ex)
			{
				throw ex;
			}

			return claim;
		}

		/// <summary>
		/// Updates the claim.
		/// </summary>
		/// <param name="claim"></param>
		public void UpdateClaim(Claim claim)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd =
						new SqlCommand(
							"UPDATE CLAIMS SET status = @status, description = @description;",
							conn);
					cmd.Parameters.AddWithValue("@status", claim.Status);
					cmd.Parameters.AddWithValue("@description", claim.Description);

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
		/// Maps the entire row a single claim object.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns>A claim object<returns>
		private Claim MapRowToClaim(SqlDataReader reader)
		{
			return new Claim()
			{
				Id = Convert.ToInt32(reader["id"]),
				Submitter = Convert.ToInt32(reader["submitter"]),
				PotholeRecord = Convert.ToInt32(reader["potholeid"]),
				DateSubmitted = Convert.ToDateTime(reader["datesubmitted"]),
				Status = Convert.ToBoolean(reader["status"]),
				Description = Convert.ToString(reader["description"])
				
			};
		}

	    
	}
}
