using CarsRegister.Tools;
using RestSharp;

namespace CarsRegister.Third_Part
{
    public class RentalBooking
    {
        public async Task isCarAvailableAsync(string id)
        {
             
            var client = new RestClient(Connection.RENTALBOOKIN_URL + "/BookCar/getBookedById?id=" +id );
            var request = new RestRequest( Method.GET);
            request.AddHeader("", "B");
            request.AddHeader("Authorization", Connection.TOKEN );
            request.AddHeader("Cookie", "ARRAffinity=f3a5076d1c6180b59a6791153a87a33a75eda2136b729d79d2ac45bc01c7fc28; ARRAffinitySameSite=f3a5076d1c6180b59a6791153a87a33a75eda2136b729d79d2ac45bc01c7fc28");
            RestResponse response = (RestResponse)await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);

            
        }
    }
}
