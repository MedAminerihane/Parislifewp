using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParisLife.Entities
{
   public class Event
    {


        public String Image { get; set; }
        public String  IdEvent { get; set; }
        public String Lieu { get; set; }
        public String Adresse { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public Double lon { get; set; }
        public Double lat { get; set; }
        public String Access { get; set; }
        public Event( String IdEvent , String Lieu, String Adresse, String Nom , String Description)
        {
            this.IdEvent = IdEvent;
            this.Lieu = Lieu;
            this.Adresse = Adresse;
            this.Nom = Nom;
            this.Description = Description;


           

        }
        public Event()
        {



        }






    }
}
