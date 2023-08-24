using CarsRegister.Model;
using CarsRegister.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace CarsRegister.Controllers
{
    [ApiController]
    [Route("[controller]")]
  //  [Authorize(AuthenticationSchemes = "CustomBearer")]
    public class CarController : ControllerBase
    {
        #region Bussines
        //Ok
        [HttpGet("getYourCars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetYourCars([FromQuery] string yourId)
        {
            try
            {
                var cars = await CarService.GetYourCars(yourId);
                if (cars != null && cars.Any())
                {
                    return Ok(cars);
                }
                else
                {
                    return NotFound("No cars found for Owner ID " + yourId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Something went wrong");
            }
        }
        //Ok
        [HttpPut("updateCar/{id}")]
        public async Task<ActionResult<string>> UpdateCar(int id, [FromBody] Car updatedCar)
        {
            try
            {
                bool response = await CarService.UpdateCar(id, updatedCar);
                if (response)
                {
                    return "Car with ID " + id + " has been updated successfully!";
                }
                else
                {
                    return "Update failed for Car ID " + id + ". Something went wrong!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Something went wrong");
            }
        }
        //Ok
        [HttpPost("addFeature")]
        public async Task<ActionResult<string>> AddFeature([FromBody] CarFeature carFeature)
        {
            try
            {
                bool response = await CarService.AddFeature(carFeature);
                if (response)
                {
                    return "Feature " + carFeature.FeatureName + " is added to Car ID " + carFeature.CarId + "!";
                }
                else
                {
                    return "Submit failed for Feature " + carFeature.FeatureName + ". Something went wrong!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Something went wrong");
            }
        }
        //Ok
        [HttpPost("addCar")]
        public async Task<ActionResult<string>> AddCar([FromBody] Car car)
        {
            try
            {
                bool response = await CarService.AddCar(car);
                if (response)
                {
                    return "Car with Model " + car.Model + " is added!";
                }
                else
                {
                    return "Submit failed for Model " + car.Model + ". Something went wrong!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Something went wrong");
            }
        }



        [HttpGet("getCar")]
        public async Task<String> getCar(string id)
        {
            return "";
        }
        
        [HttpGet("getReview")]
        public async Task<String> getReview(string id)
        {
            return "";
        }
        #endregion

        #region Customers
        // OK
        [HttpGet("cGetCars")]
        public async Task<IEnumerable<Car>> cGetCars([FromBody] CarSearchReq request)
        {
             List<Car> car=new List<Car>();
            string ? model = request.model;
            decimal? minPrice = request.minPrice;
            decimal? maxPrice = request.maxPrice;
            int? year = request.year;
            try
            {
              car =  await CarService.GetCars(model, minPrice, maxPrice, year);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return car;
        }
        //It s ok but the same job is done by the method before 
        [HttpGet("cGetAllCars")]
        public async Task<ActionResult<List<Car>>> getListCarsAsync()
        {
            try
            {
                // Simulate multiple concurrent database operations
                var task1 = CarService.GetCarsAsync();

                // Wait for all tasks to complete
                var results = await Task.WhenAll(task1);

                // Combine the results from all tasks
                var bookedCars = new List<Car>();
                foreach (var result in results)
                {
                    bookedCars.AddRange(result);
                }

                if (bookedCars.Count > 0)
                {
                    return Ok(bookedCars);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
        [HttpGet("cGetCar")]
        public async Task<ActionResult<List<Car>>> ByIdGetCar(string id)
        {
            try
            {
                // Simulate multiple concurrent database operations
                var task1 = CarService.ByIdGetCarsAsync(id);

                // Wait for all tasks to complete
                var results = await Task.WhenAll(task1);

                // Combine the results from all tasks
                var bookedCars = new List<Car>();
                foreach (var result in results)
                {
                    bookedCars.AddRange(result);
                }

                if (bookedCars.Count > 0)
                {
                    return Ok(bookedCars);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }



        [HttpPost("addReview")]
        public async Task<ActionResult<string>> addReview([FromBody] CarReview carReviews)
        {
 
            try
            {
                // Start tasks to book each car
                
                    Boolean response = await CarService.addReview(carReviews);
                    if (response == true)
                    {
                        return "Review for car ID " + carReviews.CarId + " is submitted!";
                    }
                    else
                    {
                        return "Submit failed for car ID " + carReviews.CarId + ". Something went wrong!";
                    }

                // Wait for all tasks to complete
 
             }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Something went wrong");
            }
        }

        #endregion
    }
}
