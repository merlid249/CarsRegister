using CarsRegister.Model;
using CarsRegister.Tools;
using System.Data;
using System.Data.SqlClient;

namespace CarsRegister.Service
{
    public class CarService
    {
        #region Customers
        internal static async Task<List<Car>> GetCarsAsync()
        {
            List<Car> bookings = new List<Car>();

            using (SqlConnection conn = new SqlConnection(Connection.CarListConnection))
            {
                await conn.OpenAsync();
                string response = "SELECT * FROM CarList.dbo.Cars";

                using (SqlCommand cmd = new SqlCommand(response, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Map the data from the reader to a Booking object
                            Car cars = new Car
                            {
                            };
                            try
                            {
                                cars.CarId = reader["CarId"].ToString();
                                cars.OwnerId = reader["OwnerId"].ToString();
                                cars.Model = reader["Model"].ToString();
                                cars.Make = reader["Make"].ToString();
                                cars.Year = (int)reader["Year"];
                                cars.Color = reader["Color"].ToString();
                                cars.Mileage = (decimal)reader["Mileage"];
                                cars.Transmission = reader["Transmission"].ToString();
                                cars.Status = reader["Status"].ToString();
                                cars.PricePerDay = (decimal) reader["PricePerDay"];
                                cars.Location = reader["Location"].ToString();
                                cars.ImageUrl = reader["ImageUrl"].ToString();
                                cars.Description = reader["Description"].ToString();
                                cars.CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString());
                                cars.UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString());
                                cars.Location = reader["Location"].ToString();
                                cars.ImageUrl = reader["ImageUrl"].ToString();
                                cars.LastMaintenanceDate = DateTime.Parse(reader["LastMaintenanceDate"].ToString());
                                if (reader["LastMaintenanceDate"] != DBNull.Value)
                                {
                                    cars.LastMaintenanceDate = DateTime.Parse(reader["LastMaintenanceDate"].ToString());
                                }
                                else
                                {
                                    cars.LastMaintenanceDate = null; // This line is optional since nullable types default to null.
                                }
                            }
                            catch (FormatException fe)
                            {
                                // Log or handle the format exception as needed
                                Console.WriteLine(fe.Message);
                                continue; // Skip this record and continue with the next one
                            }
                            bookings.Add(cars);
                        }
                    }
                }
            }
            return bookings;
        }
        internal static async Task<List<Car>> ByIdGetCarsAsync(string id)
        {
            List<Car> bookings = new List<Car>();

            using (SqlConnection conn = new SqlConnection(Connection.CarListConnection))
            {
                await conn.OpenAsync();
                string response = "SELECT * FROM CarList.dbo.Cars WHERE CarId=" +id+";";

                using (SqlCommand cmd = new SqlCommand(response, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Map the data from the reader to a Booking object
                            Car cars = new Car
                            {
                            };
                            try
                            {
                                cars.CarId = reader["CarId"].ToString();
                                cars.OwnerId = reader["OwnerId"].ToString();
                                cars.Model = reader["Model"].ToString();
                                cars.Make = reader["Make"].ToString();
                                cars.Year = (int)reader["Year"];
                                cars.Color = reader["Color"].ToString();
                                cars.Mileage = (decimal)reader["Mileage"];
                                cars.Transmission = reader["Transmission"].ToString();
                                cars.Status = reader["Status"].ToString();
                                cars.PricePerDay = (decimal)reader["PricePerDay"];
                                cars.Location = reader["Location"].ToString();
                                cars.ImageUrl = reader["ImageUrl"].ToString();
                                cars.Description = reader["Description"].ToString();
                                cars.CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString());
                                cars.UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString());
                                cars.Location = reader["Location"].ToString();
                                cars.ImageUrl = reader["ImageUrl"].ToString();
                                cars.LastMaintenanceDate = DateTime.Parse(reader["LastMaintenanceDate"].ToString());
                                if (reader["LastMaintenanceDate"] != DBNull.Value)
                                {
                                    cars.LastMaintenanceDate = DateTime.Parse(reader["LastMaintenanceDate"].ToString());
                                }
                                else
                                {
                                    cars.LastMaintenanceDate = null; // This line is optional since nullable types default to null.
                                }
                            }
                            catch (FormatException fe)
                            {
                                // Log or handle the format exception as needed
                                Console.WriteLine(fe.Message);
                                continue; // Skip this record and continue with the next one
                            }
                            bookings.Add(cars);
                        }
                    }
                }
            }
            return bookings;
        }
        internal static async Task<List<Car>> GetCars(string? model, decimal? minPrice, decimal? maxPrice, int? year)
        {
            List<Car> carsList = new List<Car>();

            using (SqlConnection conn = new SqlConnection(Connection.CarListConnection))
            {
                await conn.OpenAsync();

                var query = "SELECT * FROM CarList.dbo.Cars WHERE 1=1";

                if (!string.IsNullOrEmpty(model))
                {
                    query += " AND Model = '" + model + "'";
                }
                if (minPrice.HasValue & minPrice != 0)
                {
                    query += " AND PricePerDay >= '" + minPrice + "'";
                }
                if (maxPrice.HasValue & maxPrice !=0 )
                {
                    query += " AND PricePerDay <=  '" + maxPrice + "'";
                }
                if (year.HasValue & year != 0)
                {
                    query += " AND Year = '"+year + "'";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    if (!string.IsNullOrEmpty(model))
                    {
                        cmd.Parameters.AddWithValue(model, model);
                    }
                    if (minPrice.HasValue & minPrice != 0)
                    {
                        cmd.Parameters.AddWithValue(minPrice.ToString(), minPrice.Value);
                    }
                    if (maxPrice.HasValue & maxPrice != 0)
                    {
                        cmd.Parameters.AddWithValue(maxPrice.ToString(), maxPrice.Value);
                    }
                    if (year.HasValue & year !=0)
                    {
                        cmd.Parameters.AddWithValue(year.ToString(), year.Value);
                    }

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Car car = new Car
                            {
                                CarId = reader["CarId"].ToString(),
                                OwnerId = reader["OwnerId"].ToString(),
                                Model = reader["Model"].ToString(),
                                Make = reader["Make"].ToString(),
                                Year = (int)reader["Year"],
                                Color = reader["Color"].ToString(),
                                Mileage = (decimal)reader["Mileage"],
                                Transmission = reader["Transmission"].ToString(),
                                Status = reader["Status"].ToString(),
                                PricePerDay = (decimal)reader["PricePerDay"],
                                Location = reader["Location"].ToString(),
                                ImageUrl = reader["ImageUrl"].ToString(),
                                Description = reader["Description"].ToString(),
                                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                                UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString())
                            };

                            if (reader["LastMaintenanceDate"] != DBNull.Value)
                            {
                                car.LastMaintenanceDate = DateTime.Parse(reader["LastMaintenanceDate"].ToString());
                            }
                            else
                            {
                                car.LastMaintenanceDate = null;
                            }

                            carsList.Add(car);
                        }
                    }
                }
            }
            return carsList;
        }
        internal static async Task<Boolean> addReview(CarReview carReview)
        {
            Boolean resp = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Connection.CarListConnection))
                {
                    await conn.OpenAsync();
                    string insertQuery = "INSERT INTO CarList.dbo.CarReviews " +
                                      "(CarId, ReviewerName, Rating, Comment, CreatedAt) " +
                                      "VALUES (" +
                                        "'" + carReview.CarId + "'," +
                                          "'" + carReview.ReviewerName + "'," +
                                          "'" + carReview.Rating + "'," +
                                          "'" + carReview.Comment + "'," +
                                          "'" + carReview.CreatedAt + "'" +
                                          ")";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.RecordsAffected == 1)
                            {
                                resp = true;

                            }
                        }
                    }
                }

            }
            catch(Exception ex) 
            { 
                
            }
            return resp;


        }
        #endregion
    }
}
