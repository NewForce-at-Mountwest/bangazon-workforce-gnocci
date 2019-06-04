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


    }
}

