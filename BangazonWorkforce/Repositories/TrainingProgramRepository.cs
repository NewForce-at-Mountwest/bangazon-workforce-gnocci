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

        //public static TrainingProgram GetOneTrainingProgram(int id)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                SELECT
        //                    Id, Name, StartDate, EndDate, MaxAttendees
        //                FROM TrainingProgram
        //                WHERE Id = @id";
        //            cmd.Parameters.Add(new SqlParameter("@id", id));
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            TrainingProgram TrainingProgram = null;

        //            if (reader.Read())
        //            {
        //                TrainingProgram = new TrainingProgram
        //                {
        //                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                    Name = reader.GetString(reader.GetOrdinal("firstName")),
        //                    StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
        //                    EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
        //                    MaxAttendees = reader.GetInt32(reader.GetOrdinal("MaxAttendees")),

        //                };
        //            }
        //            reader.Close();

        //            return TrainingProgram;
        //        }
        //    }

        //}

        //public static Student GetOneStudentWithExercises(int id)
        //{
        //    Student student = GetOneStudent(id);
        //    student.Exercises = ExerciseRepository.GetAssignedExercisesByStudent(id);
        //    return student;
        //}

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

        //public static void UpdateStudent(int id, EditStudentViewModel viewModel)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            string command = @"UPDATE Student
        //                                SET firstName=@firstName, 
        //                                lastName=@lastName, 
        //                                slackHandle=@slackHandle, 
        //                                cohortId=@cohortId
        //                                WHERE Id = @id
        //                                DELETE FROM StudentExercise WHERE studentId =@id";

        //            viewModel.SelectedExercises.ForEach(exerciseId =>
        //            {
        //                command += $" INSERT INTO StudentExercise (studentId, exerciseId) VALUES (@id, {exerciseId})";

        //            });
        //            cmd.CommandText = command;
        //            cmd.Parameters.Add(new SqlParameter("@firstName", viewModel.student.FirstName));
        //            cmd.Parameters.Add(new SqlParameter("@lastName", viewModel.student.LastName));
        //            cmd.Parameters.Add(new SqlParameter("@slackHandle", viewModel.student.SlackHandle));
        //            cmd.Parameters.Add(new SqlParameter("@cohortId", viewModel.student.CohortId));
        //            cmd.Parameters.Add(new SqlParameter("@id", id));

        //            int rowsAffected = cmd.ExecuteNonQuery();

        //        }

        //    }

        //}

        //public static void DeleteStudent(int id)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"DELETE FROM StudentExercise WHERE studentId = @id";
        //            cmd.Parameters.Add(new SqlParameter("@id", id));

        //            int rowsAffected = cmd.ExecuteNonQuery();

        //        }
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"DELETE FROM Student WHERE Id = @id";
        //            cmd.Parameters.Add(new SqlParameter("@id", id));

        //            int rowsAffected = cmd.ExecuteNonQuery();

        //        }

        //    }

        //}

    }
}

