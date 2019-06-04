using BangazonWorkforce.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Repositories
{
    public class TrainingProgramRepository

        {

            private static IConfiguration _config;

            public static void SetConfig(IConfiguration configuration)
            {
                _config = configuration;
            }

            public static SqlConnection Connection
            {
                get
                {
                    return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                }
            }

            public static List<TrainingProgram> GetTrainingPrograms()
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                        SELECT
                           Id, Name, StartDate, EndDate, MaxAttendees
                        FROM TrainingProgram";
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<TrainingProgram> trainingPrograms = new List<TrainingProgram>();
                        while (reader.Read())
                        {
                            TrainingProgram trainingProgram = new TrainingProgram
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                                MaxAttendees = reader.GetInt32(reader.GetOrdinal("MaxAttendees")),
                            };
                        DateTime today = DateTime.Now;
                        if(today < trainingProgram.StartDate)
                        {
                            trainingPrograms.Add(trainingProgram);

                        }

                    }

                        reader.Close();

                        return trainingPrograms;
                    }
                }

            }

        public static TrainingProgram GetOneTrainingProgram(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT
                            t.Id, t.Name, t.StartDate, t.EndDate, t.MaxAttendees, e.FirstName, e.LastName
                        FROM TrainingProgram t
                        LEFT JOIN EmployeeTraining et ON t.Id = et.TrainingProgramId
                        lEFT JOIN Employee e ON et.EmployeeId = e.Id
                        WHERE t.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    TrainingProgram TrainingProgram = null;

                    if (reader.Read())
                    {
                        TrainingProgram = new TrainingProgram
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                            MaxAttendees = reader.GetInt32(reader.GetOrdinal("MaxAttendees")),

                        };
                    }
                    reader.Close();

                     return TrainingProgram;
                }
            }

        }

        public static TrainingProgram GetOneTrainingProgramWithAttendingEmployees(int id)
        {
            TrainingProgram trainingProgram = GetOneTrainingProgram(id);
            trainingProgram.Employees = TrainingProgramRepository.GetEmployeesAttendingTraining(id);
            return trainingProgram;
        }


        public static List<Employee> GetEmployeesAttendingTraining(int id)
        {

            List<Employee> Employees = new List<Employee>();

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT e.Id, e.FirstName, e.LastName FROM Employee e JOIN EmployeeTraining et ON e.Id = et.EmployeeId WHERE et.TrainingProgramId = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employees.Add(new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        });
                    }
                    reader.Close();
                }
            }
            return Employees;
        }

        public static void CreateTrainingProgram(TrainingProgram trainingProgram)
        {
            using (SqlConnection conn = Connection)
            {

                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO TrainingProgram
            ( Name, StartDate, EndDate, MaxAttendees )
            VALUES
            ( @Name, @StartDate, @EndDate, @MaxAttendees )";
                    cmd.Parameters.Add(new SqlParameter("@Name", trainingProgram.Name));
                    cmd.Parameters.Add(new SqlParameter("@StartDate", trainingProgram.StartDate));
                    cmd.Parameters.Add(new SqlParameter("@EndDate", trainingProgram.EndDate));
                    cmd.Parameters.Add(new SqlParameter("@MaxAttendees", trainingProgram.MaxAttendees));
                    cmd.ExecuteNonQuery();


                }
            }

        }

        public static void UpdateTrainingProgram(int id, TrainingProgram trainingProgram)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    if (trainingProgram.StartDate > DateTime.Now)
                    {
                        string command = @"UPDATE TrainingProgram
                                        SET Name=@Name,
                                        StartDate=@StartDate,
                                        EndDate=@EndDate,
                                        MaxAttendees=@Maxattendees
                                        WHERE Id = @id";
                        cmd.CommandText = command;
                        cmd.Parameters.Add(new SqlParameter("@Name", trainingProgram.Name));
                        cmd.Parameters.Add(new SqlParameter("@StartDate", trainingProgram.StartDate));
                        cmd.Parameters.Add(new SqlParameter("@EndDate", trainingProgram.EndDate));
                        cmd.Parameters.Add(new SqlParameter("@MaxAttendees", trainingProgram.MaxAttendees));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int rowsAffected = cmd.ExecuteNonQuery();
                    }

            }

        }

    }

   

}
}

