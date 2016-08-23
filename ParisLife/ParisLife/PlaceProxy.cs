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
    public class PlaceProxy
    {

        public async static Task<RootObject1> GetPlace(String place)
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=48.856614,2.352222&radius=80000&types="+place+"&key=AIzaSyCUvSvwVFFla2aihYyupm3qTaq5g_AvXQY");
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject1));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject1)serializer.ReadObject(ms);

            return data;
        }

    }

    [DataContract]

    public class Location
        {

        [DataMember]

        public double lat { get; set; }


        [DataMember]

        public double lng { get; set; }
        }

    [DataContract]

    public class Northeast
        {
        [DataMember]

        public double lat { get; set; }
        [DataMember]

        public double lng { get; set; }
        }

    [DataContract]

    public class Southwest
        {
        [DataMember]

        public double lat { get; set; }
        [DataMember]

        public double lng { get; set; }
        }

    [DataContract]

    public class Viewport
        {
        [DataMember]

        public Northeast northeast { get; set; }
        [DataMember]

        public Southwest southwest { get; set; }
        }

    [DataContract]


    public class Geometry
        {
        [DataMember]

        public Location location { get; set; }
        [DataMember]

        public Viewport viewport { get; set; }
        }

    [DataContract]


    public class OpeningHours
        {
        [DataMember]

        public bool open_now { get; set; }
        [DataMember]

        public List<object> weekday_text { get; set; }
        }

    [DataContract]

    public class Photo
        {
        [DataMember]

        public int height { get; set; }
        [DataMember]

        public List<string> html_attributions { get; set; }
        [DataMember]

        public string photo_reference { get; set; }
        [DataMember]

        public int width { get; set; }
        }

    [DataContract]

    public class Result
        {
        [DataMember]


        public Geometry geometry { get; set; }
        [DataMember]

        public string icon { get; set; }
        [DataMember]

        public string id { get; set; }
        [DataMember]

        public string name { get; set; }
        [DataMember]

        public string place_id { get; set; }
        [DataMember]

        public string reference { get; set; }
        [DataMember]

        public string scope { get; set; }
        [DataMember]

        public List<string> types { get; set; }
        [DataMember]

        public string vicinity { get; set; }
        [DataMember]

        public OpeningHours opening_hours { get; set; }
        [DataMember]

        public List<Photo> photos { get; set; }
        [DataMember]

        public double? rating { get; set; }
        }


    [DataContract]

    public class RootObject1
        {
        [DataMember]

        public List<object> html_attributions { get; set; }
        [DataMember]

        public string next_page_token { get; set; }
        [DataMember]

        public List<Result> results { get; set; }
        [DataMember]

        public string status { get; set; }
        }

    
}
