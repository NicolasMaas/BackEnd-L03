using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Lab02.Models;
using Microsoft.Extensions.Configuration;

namespace Lab02.Repositories
{
    public class StudentRepo_SQL : IStudentRepo
    {
        private readonly string connectionString;
        private readonly IEducationRepo educationRepo;

        public StudentRepo_SQL(IConfiguration config , IEducationRepo educationRepo )
        {
            var connectionConfig = config.GetSection("Configurations")["OtherConnection"];
            connectionString = ConfigurationExtensions.GetConnectionString(config, "DefaultConnection");
            this.educationRepo = educationRepo;
        }

        public async Task<Student> AddAsync(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "Insert into Students(Name, Gender, Email, DateOfBirth, PassWord , EducationId)";
                SQL += " Values(@Name, @Gender, @Email, @DateOfBirth, @PassWord , @EducationId)";

                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Email", student.Email ?? "");

                //cmd.Parameters.AddWithValue("@DateOfBirth", Student.DateOfBirth != null ? Student.DateOfBirth : (System.DateTime) SqlDateTime.Null);
                if (student.Birthday != null)
                {
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.Birthday);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DateOfBirth", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@Password", student.Password ?? "");

                //Maak educationId  Nullable via int? ( gezien het een integer is) 
                if (student.EducationId != null)
                {
                    cmd.Parameters.AddWithValue("@EducationId", student.EducationId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@EducationId", DBNull.Value);
                }

                con.Open();
                //SqlDataReader reader = cmd.ExecuteReader();
                await cmd.ExecuteNonQueryAsync();
                //cmd.ExecuteNonQuery();
                con.Close();
                return student;
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            List<Student> lst = new List<Student>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //1. SQL query
                string sql = "SELECT * FROM Students ";
                SqlCommand cmd = new SqlCommand(sql, con);
                //2. Data ophalen
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                lst = await GetData(reader);
                con.Close();
            }
            return lst;

        }

        public async Task<Student> GetStudentAsync(int id)
        {
            Student student = new Student();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT * FROM Students WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(SQL, con)
                {
                    CommandType = System.Data.CommandType.Text,
                };
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                student = (await GetData(reader))[0];
                con.Close();

            }
            return student;

        }

        public Task<Student> Update(Student student)
        {
            throw new NotImplementedException();
        }

        //SQL helpers --------------------------------------------------
        private async Task<List<Student>> GetData(SqlDataReader reader)
        {
            List<Student> lst = new List<Student>();
            //1. try catch verhindert applicatie crash
            try
            {
                while (await reader.ReadAsync())
                {
                    Student s = new Student();
                    s.Id = Convert.ToInt32(reader["Id"]);
                    s.Name = !Convert.IsDBNull(reader["Name"]) ? (string)reader["Name"] : "";
                    //TO DO: verder uitbouwen van overige properties
                    s.Email = !Convert.IsDBNull(reader["Email"]) ? (string)reader["Email"] : "";
                    s.Password = !Convert.IsDBNull(reader["Password"]) ? (string)reader["Password"] : "";
                    s.Gender = Convert.ToInt32(reader["Gender"]) == 0 ? Person.GenderType.Male : Person.GenderType.Female;
                    if (!Convert.IsDBNull(reader["DateOfBirth"]) ){
                        s.Birthday = Convert.ToDateTime(reader["DateOfBirth"]);
                    }
                    //EducationId kan NULL zijn.
                    if (!Convert.IsDBNull(reader["EducationId"])) {
                        s.EducationId = (int)reader["EducationId"];
                      s.Education = await educationRepo.GetEducationAsync(s.EducationId.Value);
                    }

                    //Let op mogelijke NULL waarden (=> anders crash) 
                    lst.Add(s);
                }
            }
            catch (Exception exc)
            {
                Console.Write(exc.Message); //later loggen
            }
            finally
            {
                reader.Close();  //Niet vergeten. Beperkt aantal verbindingen (of kosten)
            }
            return lst;
        }


    }
}
