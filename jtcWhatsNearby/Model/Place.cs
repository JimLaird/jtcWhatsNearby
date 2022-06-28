using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace jtcWhatsNearby.Model
{

    public partial class Place
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phone_number", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty("website", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Website { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }

        public string TypeList => string.Format(GenerateTypeString());

        string GenerateTypeString()
        {
            string typeList = "";
            for (int i = 0; i < Types.Count(); i++)
            {
                typeList += $"{Types[i]}; ";
            }

            return typeList;

        }

        [JsonProperty("distance")]
        public long Distance { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }
}


    //// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    //public class Location
    //{
    //    [JsonProperty("lat")]
    //    public double Lat { get; set; }

    //    [JsonProperty("lng")]
    //    public double Lng { get; set; }
    //}

    //public class Result
    //{
    //    [JsonProperty("id")]
    //    public string Id { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("address")]
    //    public string Address { get; set; }

    //    [JsonProperty("phone_number")]
    //    public string PhoneNumber { get; set; }

    //    [JsonProperty("website")]
    //    public string Website { get; set; }

    //    [JsonProperty("location")]
    //    public Location Location { get; set; }

    //    [JsonProperty("types")]
    //    public List<string> Types { get; set; }

    //    [JsonProperty("distance")]
    //    public int Distance { get; set; }
    //}

    //public class Root
    //{
    //    [JsonProperty("results")]
    //    public List<Result> Results { get; set; }
    //}

//}
