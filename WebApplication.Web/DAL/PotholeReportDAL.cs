using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
	public class PotholeReportDAL
	{

		private readonly string connectionString;

		public PotholeReportDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}

		/// <summary>
		/// Saves the user to the database.
		/// </summary>
		/// <param name="user"></param>
		public void CreateUser(User user)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd =
						new SqlCommand("INSERT INTO users VALUES (@username, @password, @salt, @role, @phonenumber);",
							conn);
					cmd.Parameters.AddWithValue("@username", user.Username);
					cmd.Parameters.AddWithValue("@firstname", user.FirstName);
					cmd.Parameters.AddWithValue("@lastname", user.LastName);
					cmd.Parameters.AddWithValue("@password", user.Password);
					cmd.Parameters.AddWithValue("@salt", user.Salt);
					cmd.Parameters.AddWithValue("@role", user.Role);
					cmd.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);

					cmd.ExecuteNonQuery();

					return;
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}

		}
	}

}
