using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParisLife.Entities
{
    class Place
    {
        public String id { get; set; }
        public String name  { get; set; }
        public String reference { get; set; }
        public String vicinity { get; set; }
        public double? rating { get; set; }
        public String etat { get; set; }

        public double lat { get; set; }
        public double lng { get; set; }


        public Place()
        {

        }

public Place(String id,String name, String reference, String vicinity, double? rating,String etat, double lat, double lng)
        {
            this.id = id;
            this.name = name;
            this.reference = reference;
            this.vicinity = vicinity;
            this.rating = rating;
            this.etat = etat;
            this.lat = lat;
            this.lng = lng;

        }
    }
}
