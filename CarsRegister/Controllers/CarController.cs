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

        [HttpPost("addCar")]
        public async Task<String> addCar()
        {
            return  "";
        }
        [HttpPut("updateCar")]
        public async Task<String> updateCar()
        {
            return "";
        }
        [HttpGet("getYourCars")]
        public async Task<String> getCars(string yourId)
        {
            return "";

        }
        [HttpGet("getCar")]
        public async Task<String> getCar(string id)
        {
            return "";
        }
        [HttpPost("addFeature")]
        public async Task<String> addFeature(string id)
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
