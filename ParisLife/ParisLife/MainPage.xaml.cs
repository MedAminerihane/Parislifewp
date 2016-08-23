using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParisLife.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Syndication;
using winsdkfb;
using winsdkfb.Graph;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ParisLife
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        bool share = false;
       
        List<Event> Events = new List<Event>();
        List<Event> Evento = new List<Event>();
        public ObservableCollection<RssFeedModel> FeedsList = new ObservableCollection<RssFeedModel>();
        public MainPage()
        {
            this.InitializeComponent();

         //   string SID = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();
          //  System.Diagnostics.Debug.WriteLine(SID);

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
          
               Frame.Navigate(typeof(About));
         
        }



        private  void ListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
           
            ListView lv = (ListView)sender;
            Event ev = (Event)lv.SelectedItem;
         
             Frame.Navigate(typeof(DetailEvent),ev);



        }

        async  Task<String> AccessTheWebAsync()
        {
            string urlContents = "";
            // You need to add a reference to System.Net.Http to declare client.
            HttpClient client = new HttpClient();

            // GetStringAsync returns a Task<string>. That means that when you await the
            // task you'll get a string (urlContents).
            try { 
            Task<string> getStringTask = client.GetStringAsync("https://api.paris.fr/api/data/1.4/QueFaire/get_activities/?token=854a6414181ab47dcf9281f3ed0dbb755508b16a800953d7c95318526428afb4&cid=0&tag=0&created=0&start=0&end=0&offset=0&limit=10");

            
             urlContents = await getStringTask;
            }
            catch {

            }


            return urlContents;
        }



        public List<Event> JsonToEvent(String result)
        {

           

            JObject deserializedEvent = JsonConvert.DeserializeObject<JObject>(result);
            JArray data = JsonConvert.DeserializeObject<JArray>((String)(deserializedEvent["data"]).ToString());
            // System.Diagnostics.Debug.WriteLine(data);


            List<Event> EventsList = new List<Event>();
           


            foreach (JObject conteur in data)
            {

                Event e = new Event();
                JObject objEvent = JsonConvert.DeserializeObject<JObject>(conteur.ToString());
       
                e.Nom = (String)objEvent["nom"];
                e.Description = (String)objEvent["small_description"];
                e.IdEvent = (String)objEvent["idactivites"];
                e.Adresse = (String)objEvent["adresse"];
                e.lat = (Double)objEvent["lat"];
                e.lon = (Double)objEvent["lon"];
                e.Lieu = (String)objEvent["lieu"];
                e.Access = (String)objEvent["accessType"];

                JArray files = JsonConvert.DeserializeObject<JArray>((String)(objEvent["files"]).ToString());
              

             foreach (JObject img in files)
                  {

                    JObject obb = JsonConvert.DeserializeObject<JObject>(img.ToString());
                    String imge= (String)obb["file"];
                    e.Image = "http://filer.paris.fr/" + imge;

                  }
                  

                //e.Access = (String)objEvent["accessType"];
                EventsList.Add(e);


            }
            return EventsList;

        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
    await AddRssFeed("http://www.france24.com/fr/france/rss");

      
               

     Events =   JsonToEvent(await AccessTheWebAsync());

     EventSection.DataContext = Events;
      // EventList.DataContext = Events;
    }


        private async Task AddRssFeed(string url)
        {
            try
            {
                SyndicationClient client = new SyndicationClient();

                Uri myUri;
                if (Uri.TryCreate(url, UriKind.Absolute, out myUri))
                {
                    if ((myUri.Scheme == "http" || myUri.Scheme == "https"))
                    {
                        SyndicationFeed feed = await client.RetrieveFeedAsync(new Uri(url));


                        RssFeedModel feedModel = new RssFeedModel(feed.Title.Text, feed.Subtitle != null ? feed.Subtitle.Text : "", new Uri(url), null);

                        feedModel.ArticleList = new ObservableCollection<RssArticleModel>(feed.Items.Select(f =>
                                 new RssArticleModel(f.Title.Text,

                                f.ItemUri != null ? f.ItemUri : f.Links.Select(l => l.Uri).LastOrDefault(),
                               f.PublishedDate.ToString(),
                        f.Summary != null ? Regex.Replace(Regex.Replace(f.Summary.Text, "\\&.{0,4}\\;", string.Empty), "<.*?>", string.Empty) : "",
                                     f.Authors.Select(a => a.NodeValue).FirstOrDefault(),
                                     f.ItemUri != null ? f.ItemUri : f.Links.Select(l => l.Uri).FirstOrDefault()
                                     )));
                     
                        EventList.DataContext = feedModel.ArticleList;

                        if (FeedsList.Contains(feedModel))
                        {
                            FeedsList.Remove(feedModel);


                        }
                     
                    //   FeedsList.Add(feedModel);
                    }
                
                }
            }
            catch (Exception)
            {
               
            }
        }

      

        private async void ResultImage_Loaded(object sender, RoutedEventArgs e)
        {
           RootObject myweather = await OpenWeatherMapProxy.GetWeather();
            Image im = (Image)sender;
            TextBlock txt = new TextBlock() ;
            txt.FontFamily = new FontFamily("Arial Black");
           txt.FontWeight = Windows.UI.Text.FontWeights.Bold;
         
            txt.Margin = new Thickness(2,20,2,2);
            txt.FontSize = 32;
            txt.Foreground = new SolidColorBrush(Colors.White);

            txt.Text = (Math.Round(myweather.main.temp - 273.15)).ToString() + "°C";
            StackPanel st = (StackPanel)im.Parent;
            st.Children.Add(txt);
            BitmapImage om = new BitmapImage(new Uri("ms-appx:///Assets/Weather/" + myweather.weather.
              ElementAt(0).icon+".png"));

           im.Source = om;
        }

        private  void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {

            //StackPanel st = (StackPanel)sender;
            //String a = st.Tag.ToString();
            switch (((StackPanel)sender).Tag.ToString())
            {
                case "cafe":
                    IsolatedStorageHelper.SaveObject("Type", "cafe");
                    Frame.Navigate(typeof(Guide));

                    break;
                case "bar":
                    IsolatedStorageHelper.SaveObject("Type", "bar");

                    Frame.Navigate(typeof(Guide));
                    //do something else
                    break;
                case "food":
                    IsolatedStorageHelper.SaveObject("Type", "food");

                    Frame.Navigate(typeof(Guide));
                    //do something else
                    break;
                case "bank":
                    IsolatedStorageHelper.SaveObject("Type", "bank");

                    Frame.Navigate(typeof(Guide));
                    //do something else
                    break;
                case "museum":
                    IsolatedStorageHelper.SaveObject("Type", "museum");

                    Frame.Navigate(typeof(Guide));
                    //do something else
                    break;
                case "hopital":
                    IsolatedStorageHelper.SaveObject("Type", "hospital");

                    Frame.Navigate(typeof(Guide));
                    //do something else
                    break;
                case "library":
                    IsolatedStorageHelper.SaveObject("Type", "library");

                    Frame.Navigate(typeof(Guide));
                    //do something else
                    break;
                case "lodging":
                    IsolatedStorageHelper.SaveObject("Type", "lodging");

                    Frame.Navigate(typeof(Guide));
                    //do something else
                    break;
                default:
                    //something else
                    break;
            }

        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            

        }
        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {

            Events = JsonToEvent(await AccessTheWebAsync());

            EventSection.DataContext = Events;

        }

        private async void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            FBSession sess = FBSession.ActiveSession;
            sess.FBAppId = "557554417758083";
            sess.WinAppId = "s-1-15-2-3773560014-4269826133-810696220-1385929008-3221146478-1514580757-3294081984";
            List<String> permissionList = new List<String>();
            permissionList.Add("public_profile");
            permissionList.Add("user_friends");
            permissionList.Add("user_likes");
            permissionList.Add("user_location");
            permissionList.Add("user_photos");
            permissionList.Add("publish_actions");
            FBPermissions permissions = new FBPermissions(permissionList);

            // Login to Facebook
            if (sess.LoggedIn)
            {
              await  sess.LogoutAsync();
                fbcon.Label = "connexion";
            }
            else { 
            FBResult result = await sess.LoginAsync(permissions);
            if (result.Succeeded)
            {
                FBUser user = sess.User;
                System.Diagnostics.Debug.WriteLine(user.Name);
                    // ProfilePic.UserId = sess.User.Id;

                    //     Debug.WriteLine(sess.User.Id);
                    //   Debug.WriteLine(sess.User.Name);
                    fbcon.Label = "Déconnexion";
            }
            else
            {
                //Login failed
            }
            }
        }

        private void AppBarButton_Click_4(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(favoris));
        }

        private  void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {

       //     System.Diagnostics.Debug.WriteLine("image tapped");
             share = true;





        }

        private async void ListView_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("list taped tapped");
            if (share == true)
            {
                ListView lr = (ListView)sender;
                RssArticleModel ram = (RssArticleModel)lr.SelectedItem;

         //  System.Diagnostics.Debug.WriteLine(ram.Link);
                FBSession sess = FBSession.ActiveSession;
                if (sess.LoggedIn)
                {
                    
                    PropertySet parameters = new PropertySet();
                    parameters.Add("title", ram.ArticleName);
                    parameters.Add("link", ram.Link.AbsoluteUri);
                    parameters.Add("description", ram.Description);
                    // Display feed dialog
                    FBResult fbresult = await sess.ShowFeedDialogAsync(parameters);

                    share = false;

                }
                else
                {


                    var dialog = new MessageDialog("You Have to login First ");
                  await dialog.ShowAsync();



                }
            }

        }
    }
}
