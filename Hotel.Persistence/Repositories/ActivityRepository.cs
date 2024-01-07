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
                        string insertQuery = "INSERT INTO Activity (Name, Description, Location, Date, Duration, AvailablePlaces, PriceAdult, PriceChild, Discount) " +
                                             "VALUES (@Name, @Description, @Location, @Date, @Duration, @AvailablePlaces, @PriceAdult, @PriceChild, @Discount)";

                        command.CommandText = insertQuery;
                        command.Parameters.AddWithValue("@Name", activity.Name);
                        command.Parameters.AddWithValue("@Description", activity.Description);
                        command.Parameters.AddWithValue("@Location", activity.Location);
                        command.Parameters.AddWithValue("@Date", activity.Date);
                        command.Parameters.AddWithValue("@Duration", activity.Duration);
                        command.Parameters.AddWithValue("@AvailablePlaces", activity.AvailablePlaces);
                        command.Parameters.AddWithValue("@PriceAdult", activity.PriceAdult);
                        command.Parameters.AddWithValue("@PriceChild", activity.PriceChild);

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

        //public List<Activity> GetActivities(string filter)
        //{
        //    List<Activity> activities = new List<Activity>();

        //    string query = "SELECT * FROM Activity";
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(databaseConnectionString))
        //        {
        //            SqlCommand command = new SqlCommand(query, connection);
        //            connection.Open();

        //            SqlDataReader reader = command.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                Activity activity;
        //                if (reader["Discount"] == DBNull.Value)
        //                {
        //                    activity = new Activity((int)reader["Id"], (string)reader["Name"], (string)reader["Description"], (DateTime)reader["Date"], (int)reader["Duration"], (int)reader["AvailablePlaces"], (decimal)reader["PriceAdult"], (decimal)reader["PriceChild"], 0, (string)reader["Location"]);

        //                }
        //                else
        //                {
        //                    activity = new Activity((int)reader["Id"], (string)reader["Name"], (string)reader["Description"], (DateTime)reader["Date"], (int)reader["Duration"], (int)reader["AvailablePlaces"], (decimal)reader["PriceAdult"], (decimal)reader["PriceChild"], (decimal)reader["Discount"], (string)reader["Location"]);
        //                }

        //                activities.Add(activity);
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }

        //    return activities;
        //}
        public List<Activity> GetActivities(string filter)
        {
            try
            {
                List<Activity> activities = new List<Activity>();
                string sql = "SELECT a.Id, a.Name, a.Description, a.Date, a.Duration, a.AvailablePlaces, " +
                             "a.PriceAdult, a.PriceChild, ISNULL(a.Discount, 0) AS Discount, a.Location " +
                             "FROM Activity a " +
                             "WHERE 1 = 1";

                // Add the filter conditions
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql += " AND (a.Name LIKE @filter OR a.Description LIKE @filter OR a.Location LIKE @filter)";
                }

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    // Add parameter for the filter condition
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Activity activity = new Activity(
                                id: Convert.ToInt32(reader["Id"]),
                                name: reader["Name"].ToString(),
                                description: reader["Description"].ToString(),
                                date: Convert.ToDateTime(reader["Date"]),
                                duration: Convert.ToInt32(reader["Duration"]),
                                availablePlaces: Convert.ToInt32(reader["AvailablePlaces"]),
                                priceAdult: Convert.ToDecimal(reader["PriceAdult"]),
                                priceChild: Convert.ToDecimal(reader["PriceChild"]),
                                discount: Convert.ToDecimal(reader["Discount"]),
                                location: reader["Location"].ToString()
                            );

                            activities.Add(activity);
                        }
                    }
                }

                return activities;
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("getactivities", ex);
            }
        }


        public Activity GetActivityByActivityId(int activityId)
        {
            Activity activity = null;

            string query = "SELECT * FROM Activity WHERE Id = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", activityId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["Discount"] == DBNull.Value)
                        {
                            activity = new Activity((int)reader["Id"], (string)reader["Name"],
                                (string)reader["Description"], (DateTime)reader["Date"], (int)reader["Duration"],
                                (int)reader["AvailablePlaces"], (decimal)reader["PriceAdult"],
                                (decimal)reader["PriceChild"], 0, (string)reader["Location"]);

                        }
                        else
                        {
                            activity = new Activity((int)reader["Id"], (string)reader["Name"],
                                (string)reader["Description"], (DateTime)reader["Date"], (int)reader["Duration"],
                                (int)reader["AvailablePlaces"], (decimal)reader["PriceAdult"],
                                (decimal)reader["PriceChild"], (decimal)reader["Discount"], (string)reader["Location"]);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return activity;
        }

        public Activity GetActivityById(int activityId)
        {
            try
            {
                Activity activity = null;
                string sql = "SELECT * FROM Activity WHERE Id = @Id";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@Id", activityId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["Discount"] == DBNull.Value)
                            {
                                activity = new Activity(activityId, (string)reader["Name"],
                                    (string)reader["Description"], (DateTime)reader["Date"], (int)reader["Duration"],
                                    (int)reader["AvailablePlaces"], (decimal)reader["PriceAdult"],
                                    (decimal)reader["PriceChild"], 0, (string)reader["Location"]);

                            }
                            else
                            {
                                activity = new Activity(activityId, (string)reader["Name"],
                                    (string)reader["Description"], (DateTime)reader["Date"], (int)reader["Duration"],
                                    (int)reader["AvailablePlaces"], (decimal)reader["PriceAdult"],
                                    (decimal)reader["PriceChild"], (decimal)reader["Discount"],
                                    (string)reader["Location"]);
                            }
                        }
                    }
                }

                return activity;
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("getactivitybyid", ex);
            }                               
        }

        public List<Activity> GetAllActivities()
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
                            activity = new Activity((int)reader["Id"], (string)reader["Name"],
                                (string)reader["Description"], (DateTime)reader["Date"], (int)reader["Duration"],
                                (int)reader["AvailablePlaces"], (decimal)reader["PriceAdult"],
                                (decimal)reader["PriceChild"], 0, (string)reader["Location"]);

                        }
                        else
                        {
                            activity = new Activity((int)reader["Id"], (string)reader["Name"],
                                (string)reader["Description"], (DateTime)reader["Date"], (int)reader["Duration"],
                                (int)reader["AvailablePlaces"], (decimal)reader["PriceAdult"],
                                (decimal)reader["PriceChild"], (decimal)reader["Discount"], (string)reader["Location"]);
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

        public void RemoveActivityById(int activityId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        string deleteQuery = "DELETE FROM Activity WHERE Id = @Id";

                        command.CommandText = deleteQuery;
                        command.Parameters.AddWithValue("@Id", activityId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException($"Error deleting activity: {ex.Message}", ex);
            }
        }
    }




   
}