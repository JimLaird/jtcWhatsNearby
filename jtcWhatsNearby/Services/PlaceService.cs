using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace jtcWhatsNearby.Services
{
    public class PlaceService
    {
        HttpClient client;

        public PlaceService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-rapidapi-key", Constants.RapidApiKey);
            client.DefaultRequestHeaders.Add("x-rapidapi-host", Constants.RapidApiHost);
        }

        Place placeData = null;
        
       public async Task<Place> GetPlaces(string query)
        {
            
            var response = await client.GetAsync(query);

            

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                placeData = JsonConvert.DeserializeObject<Place>(content);
            }

            return placeData;
            
        }
    }
}
