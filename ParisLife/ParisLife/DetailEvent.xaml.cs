using ParisLife.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace ParisLife
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class DetailEvent : Page
    {
       Event ev;
        public DetailEvent()
        {
            this.InitializeComponent();
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ev = (Event)e.Parameter;
            Title.Text = ev.Nom;
            DateEvent.Text = ev.Lieu;
            Description.Text = ev.Description;
            addresse.Text = ev.Adresse;
           // Categorie.Text=ev.
            BitmapImage im = new BitmapImage(new Uri(ev.Image));
            Img.Source = im;
            BasicGeoposition snPosition = new BasicGeoposition() { Latitude = ev.lat, Longitude = ev.lon };
            Geopoint snPoint = new Geopoint(snPosition);
            MapControl.Center = snPoint;
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Location = snPoint;
            mapIcon1.Title = "Place Needle";
            mapIcon1.ZIndex = 0;
            mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/marker.png"));

            MapControl.MapElements.Add(mapIcon1);

            MapControl.ZoomLevel = 14;
        }


       

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));

        }

        private void fav_Click(object sender, RoutedEventArgs e)
        {
            // Event e = IsolatedStorageHelper.GetObject<Event>("Type");

            bool exist = false;
            List<Event> eve = IsolatedStorageHelper.GetObject<List<Event>>("favoris");
            if (eve != null)
            {
                foreach (Event fevt in eve)
                {
                    if (fevt.IdEvent == ev.IdEvent)
                        exist = true;

                }


                if (!exist)
                {
                    eve.Add(ev);
                    IsolatedStorageHelper.SaveObject<List<Event>>("favoris", eve);
                }
            }else
            {
                eve = new List<Event>();
                eve.Add(ev);
                IsolatedStorageHelper.SaveObject<List<Event>>("favoris", eve);

            }

        }

        private void part_Click(object sender, RoutedEventArgs e)
        {
           

        
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(favoris));
        }
    }
}
