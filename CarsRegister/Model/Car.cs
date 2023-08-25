namespace CarsRegister.Model
{
    public class Car
    {
        public string? CarId{ get; set; }
        public string OwnerId { get; set; } // Assuming OwnerId is an integer. Adjust the type if needed.
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public decimal Mileage { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public string Status { get; set; }
        public decimal PricePerDay { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastMaintenanceDate { get; set; } // Nullable DateTime for maintenance

        // Navigation properties
        public ICollection<CarFeature>? CarFeatures { get; set; }
        public ICollection<CarReview>? CarReviews { get; set; }

    }
    public class CarFeature
    {
        public int FeatureId { get; set; }
        public int CarId { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }

        // Navigation property
        public Car? Car { get; set; }
    }
    public class CarReview
    {
        public int ReviewId { get; set; }
        public int CarId { get; set; }
        public string ReviewerName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
         
    }

    public class CarSearchReq
    {
        public string? model { get; set; }
        public int? year { get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
    }
    }
