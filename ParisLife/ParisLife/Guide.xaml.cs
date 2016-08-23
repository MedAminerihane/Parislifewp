using ParisLife.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace ParisLife
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Guide : Page
    {
        String paramters;
     List<Place> placesl = new List<Place>();
        public Guide()
        {
            this.InitializeComponent();
          
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            TypeP.Text = paramters;

            RootObject1 place = await PlaceProxy.GetPlace(paramters);
            

            for (int i = 0; i < place.results.Count; i++)
            {
                String etat = "ouvert";
                /* if ( place.results.ElementAt(i).opening_hours.open_now!=null && !place.results.ElementAt(i).opening_hours.open_now)
                  {


                      etat = "fermé";

                  }*/
                Place p = new Place();
                p.id = place.results.ElementAt(i).id;
                p.name = place.results.ElementAt(i).name;
                // if(place.results.ElementAt(i).)

                try
                {
                    p.reference = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + place.results.ElementAt(i).photos.ElementAtOrDefault(0).photo_reference + "&key=AIzaSyCUvSvwVFFla2aihYyupm3qTaq5g_AvXQY";
                }
                catch
                {
                    p.reference = "Assets/noimage.png";

                }





                p.rating = place.results.ElementAt(i).rating;
                p.vicinity = place.results.ElementAt(i).vicinity;
                p.lng = place.results.ElementAt(i).geometry.location.lng;
                p.lat = place.results.ElementAt(i).geometry.location.lat;
                p.etat = etat;
                placesl.Add(p);
            }


                /*   Place pl = new Place(place.results.ElementAt(i).id,
                       place.results.ElementAt(i).name,
                       "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + place.results.ElementAt(i).photos.ElementAt(0).photo_reference + "&key=AIzaSyCUvSvwVFFla2aihYyupm3qTaq5g_AvXQY",
                        place.results.ElementAt(i).vicinity,
                         place.results.ElementAt(i).rating,
                         etat,
                          place.results.ElementAt(i).geometry.location.lat,
                           place.results.ElementAt(i).geometry.location.lng);


                   placesl.Add(p);
               }*/

                places.ItemsSource = placesl;
        }






        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // paramters = (String)e.Parameter;
            paramters = IsolatedStorageHelper.GetObject<String>( "Type");


        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void places_Tapped(object sender, TappedRoutedEventArgs e)
        {

            ListView lv = (ListView)sender;
            Place pl = (Place)lv.SelectedItem;

            Frame.Navigate(typeof(DetailPlace), pl);
        }
    }
}
