using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace ParisLife.Entities
{

    public class RssArticleModel 
    {
       
        public String ArticleName { get; set; }

        public Uri Image { get; set; }
        public string DatePub { get; set; }
        public string Description {get; set;}
        
      
        public string Author { get; set; }



        public Uri Link { get; set; }








        public RssArticleModel(string name, Uri image,string datePub, string desc, string author, Uri url)
        {
            this.ArticleName = name;
            this.Description = desc;
            this.Image = image;
            this.DatePub = datePub;
            this.Author = author;
            this.Link = url;
        }

    
        public override bool Equals(object obj)
        {
            if (!(obj is RssArticleModel))
                return false;
            RssArticleModel ram = obj as RssArticleModel;
            return this.GetHashCode() == ram.GetHashCode();
        }

        public override int GetHashCode()
        {
            return (this.ArticleName + this.Description + this.Author + this.Link).GetHashCode();
        }
    }
}
