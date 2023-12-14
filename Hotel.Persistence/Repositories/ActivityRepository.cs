using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Hotel.Domain.Repositories;

namespace Hotel.Persistence.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly string databaseConnectionString;

        public ActivityRepository(string connectionString)
        {
            this.databaseConnectionString = connectionString;
        }


        public void AddActivity(Activity activity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        string insertQuery = "INSERT INTO Activity (Name, Description, Location, StartDate, Duration, AvailablePlaces, CostAdult, CostChild, Discount) " +
                                             "VALUES (@Name, @Description, @Location, @StartDate, @Duration, @AvailablePlaces, @CostAdult, @CostChild, @Discount)";

                        command.CommandText = insertQuery;
                        command.Parameters.AddWithValue("@Name", activity.Name);
                        command.Parameters.AddWithValue("@Description", activity.Description);
                        command.Parameters.AddWithValue("@Location", activity.Location);
                        command.Parameters.AddWithValue("@StartDate", activity.StartDate);
                        command.Parameters.AddWithValue("@Duration", activity.Duration);
                        command.Parameters.AddWithValue("@AvailablePlaces", activity.AvailablePlaces);
                        command.Parameters.AddWithValue("@CostAdult", activity.CostAdult);
                        command.Parameters.AddWithValue("@CostChild", activity.CostChild);

                        if (activity.Discount == null)
                        {
                            command.Parameters.AddWithValue("@Discount", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Discount", activity.Discount);
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException($"Error inserting activity: {ex.Message}", ex);
            }
        }

        public List<Activity> GetActivities(string filter)
        {
            List<Activity> activities = new List<Activity>();

            string query = "SELECT * FROM Activity";
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Activity activity;
                        if (reader["Discount"] == DBNull.Value)
                        {
                            activity = new Activity((int)reader["Id"], (string)reader["Name"], (string)reader["Description"], (DateTime)reader["Date"], (int)reader["Duration"], (int)reader["AvailablePlaces"], (decimal)reader["PriceAdult"], (decimal)reader["PriceChild"], 0, (string)reader["Location"]);

                        }
                        else
                        {
                            activity = new Activity((int)reader["Id"], (string)reader["Name"], (string)reader["Description"], (DateTime)reader["Date"], (int)reader["Duration"], (int)reader["AvailablePlaces"], (decimal)reader["PriceAdult"], (decimal)reader["PriceChild"], (decimal)reader["Discount"], (string)reader["Location"]);
                        }

                        activities.Add(activity);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return activities;
        }

        public Activity GetActivityByActivityId(int activityId)
        {
            throw new NotImplementedException();
        }

        public Activity GetActivityById(int activityId)
        {
            throw new NotImplementedException();
        }

        public List<Activity> GetAllActivities()
        {
            throw new NotImplementedException();
        }

        public void RemoveActivityById(int activityId)
        {
            throw new NotImplementedException();
        }
    }




   
}