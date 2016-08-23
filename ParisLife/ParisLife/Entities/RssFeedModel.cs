using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParisLife.Entities
{
  public  class RssFeedModel
    {
        public String FeedName { get; set; }
        public String Author { get; set; }
        public String Description { get; set; }
        public Uri Link { get; set; }


     
     
    
        
    
     
        public ObservableCollection<RssArticleModel> ArticleList { get; set; }


       

        public RssFeedModel(string name, string desc, Uri url, ObservableCollection<RssArticleModel> articles)
        {
            this.FeedName = name;
            this.Description = desc;
            this.Link = url;
            this.ArticleList = articles;
        }

        public override bool Equals(object obj)
        {
            try
            {
                RssFeedModel other = (RssFeedModel)obj;

                return this.FeedName == other.FeedName && this.Description == other.Description && this.Link == other.Link;
            }
            catch (InvalidCastException e)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return (this.FeedName + this.Description + this.Link).GetHashCode();
        }
    }
}
