using Lab02.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02.Repositories
{
    public class EducationRepo_SQL : IEducationRepo
    {
        string connectionString;
        public EducationRepo_SQL(IConfiguration config)
        {
            connectionString = ConfigurationExtensions.GetConnectionString(config, "DefaultConnection");

        }

        public async Task<IEnumerable<Education>> GetAllEducationsAsync()
        {
            List<Education> lst = new List<Education>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //1. SQL query
                string sql = "SELECT * FROM Educations ";
                SqlCommand cmd = new SqlCommand(sql, con);
                //2. Data ophalen
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                lst = await GetData(reader);
                con.Close();
            }
            return lst;
        }

        public async Task<Education> GetEducationAsync(int id)
        {
            Education edu = new Education();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT * FROM Educations WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(SQL, con)
                {
                    CommandType = System.Data.CommandType.Text,
                };
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();  
                edu = (await GetData(reader))[0];
                con.Close();

            }
            return edu;

        }

        //SQL helpers --------------------------------------------------
        private async Task<List<Education>> GetData(SqlDataReader reader)
        {
            List<Education> lst = new List<Education>();
            //1. try catch verhindert applicatie crash
            try
            {
                while (await reader.ReadAsync())
                {
                    Education e = new Education();
                    e.Id = Convert.ToInt32(reader["Id"]);
                    e.Name = !Convert.IsDBNull((string)reader["Name"]) ? (string)reader["Name"] : "";
                    e.Code = reader["Code"].ToString();
                    e.Description = !Convert.IsDBNull((string)reader["Description"]) ? (string)reader["Description"] : "";

                    if (e.Id == 0) { e = null; };
                    lst.Add(e);
                }
            }
            catch (Exception exc)
            {
                //Console.Write(exc.Message); //later loggen
                throw exc;  //in development omgeving
            }
            finally
            {
                reader.Close();
            }
            return lst;
        }

    }
}
