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
    public sealed partial class favoris : Page
    {
        public favoris()
        {
            this.InitializeComponent();

        }


        private  void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            List<Event> eve = IsolatedStorageHelper.GetObject<List<Event>>("favoris");
            favorisbind.ItemsSource = eve;
        }


        private void ListView_Tapped(object sender, TappedRoutedEventArgs e)
        {


            ListView lv = (ListView)sender;
            Event ev = (Event)lv.SelectedItem;

            Frame.Navigate(typeof(DetailEvent), ev);



        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));

        }
    }
}
