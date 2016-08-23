using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ParisLife
{
  public  class OpenWeatherMapProxy
    {

        public async static Task<RootObject> GetWeather()
        {
            var http = new HttpClient();
            var response = await http.GetAsync("http://api.openweathermap.org/data/2.5/weather?lat=48.85&lon=2.35&APPID=ca1232cc1bf7cc3ffcb1f07e55eb6504");
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }

    }


    [DataContract]
    public class Coord
    {
        [DataMember]

        public double lon { get; set; }

        [DataMember]
        public double lat { get; set; }
    }



    [DataContract]

  

    public class Main
    {
        [DataMember]
        public double temp { get; set; }
      
        [DataMember]
        public double pressure { get; set; }
      
      
        [DataMember]
        public int humidity { get; set; }

        [DataMember]

        public double temp_min { get; set; }
        [DataMember]
        public double temp_max { get; set; }

    }


    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }





    [DataContract]

    public class RootObject
    {
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public string @base { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public int visibility { get; set; }
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int cod { get; set; }
    }

}
