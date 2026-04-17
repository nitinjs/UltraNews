using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitin.News.DAL.Resources
{
    public class Enclosure
    {
        public int length { get; set; }
        public string media_type { get; set; }
        public string uri { get; set; }
    }
    public class Article
    {
        public string publish_date { get; set; }
        public string source { get; set; }
        public string source_url { get; set; }
        public string summary { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public List<Enclosure> enclosures { get; set; }
    }

    public class ArticlesProxyObject
    {
        public List<Article> articles { get; set; }
        public string description { get; set; }
        public string syndication_url { get; set; }
        public string title { get; set; }
    }
}
