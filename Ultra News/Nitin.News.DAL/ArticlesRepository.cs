using Nitin.News.DAL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitin.News.DAL
{
    public class ArticlesRepository : FeedzillaClient<ArticlesProxyObject>
    {
        /// <summary>
        /// /v1/categories/{category_id}/articles.format
        /// </summary>
        /// <param name="category_id"></param>
        /// <param name="culture_code"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<Article> Articles(string category_id, int count, string since, string client_source, string order, string title_only)
        {
            MethodURL = string.Format("categories/{0}/articles", category_id);
            AddParameter("count", count.ToString());
            AddParameter("since", since);
            AddParameter("client_source", client_source);
            AddParameter("order", order);
            AddParameter("title_only", title_only);

            ArticlesProxyObject o = GetArray();
            return o.articles.AsEnumerable();
        }

        /// <summary>
        /// /v1/categories/{category_id}/subcategories/{subcategory_id}/articles.format
        /// </summary>
        /// <param name="category_id"></param>
        /// <param name="culture_code"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<Article> Articles(string category_id, string subcategory_id, int count, string since, string client_source, string order, string title_only)
        {
            MethodURL = string.Format("categories/{0}/subcategories/{1}/articles", category_id, subcategory_id);
            AddParameter("count", count.ToString());
            AddParameter("since", since);
            AddParameter("client_source", client_source);
            AddParameter("order", order);
            AddParameter("title_only", title_only);

            ArticlesProxyObject o = GetArray();
            return o.articles.AsEnumerable();
        }

        /// <summary>
        /// /v1/articles/search.format
        /// </summary>
        /// <param name="q"></param>
        /// <param name="category_id"></param>
        /// <param name="count"></param>
        /// <param name="since"></param>
        /// <param name="client_source"></param>
        /// <param name="order"></param>
        /// <param name="title_only"></param>
        /// <returns></returns>
        public IEnumerable<Article> Search(string q, int count, string since, string client_source, string order, string title_only)
        {
            MethodURL = string.Format("articles/search");
            AddParameter("q", q);
            AddParameter("count", count.ToString());
            AddParameter("since", since);
            AddParameter("client_source", client_source);
            AddParameter("order", order);
            AddParameter("title_only", title_only);

            ArticlesProxyObject o = GetArray();
            return o.articles.AsEnumerable();
        }

        /// <summary>
        /// /v1/categories/{category_id}/articles/search.format
        /// </summary>
        /// <param name="q"></param>
        /// <param name="category_id"></param>
        /// <param name="count"></param>
        /// <param name="since"></param>
        /// <param name="client_source"></param>
        /// <param name="order"></param>
        /// <param name="title_only"></param>
        /// <returns></returns>
        public IEnumerable<Article> Search(string q, string category_id, int count, string since, string client_source, string order, string title_only)
        {
            MethodURL = string.Format("categories/{0}/articles/search", category_id);
            AddParameter("q", q);
            AddParameter("count", count.ToString());
            AddParameter("since", since);
            AddParameter("client_source", client_source);
            AddParameter("order", order);
            AddParameter("title_only", title_only);

            ArticlesProxyObject o = GetArray();
            return o.articles.AsEnumerable();
        }

        /// <summary>
        /// /v1/categories/{category_id}/subcategories/{subcategory_id}/articles/search.format
        /// </summary>
        /// <param name="q"></param>
        /// <param name="category_id"></param>
        /// <param name="count"></param>
        /// <param name="since"></param>
        /// <param name="client_source"></param>
        /// <param name="order"></param>
        /// <param name="title_only"></param>
        /// <returns></returns>
        public IEnumerable<Article> Search(string q, string category_id, string subcategory_id, int count, string since, string client_source, string order, string title_only)
        {
            MethodURL = string.Format("categories/{0}/subcategories/{1}/articles/search", category_id, subcategory_id);
            AddParameter("q", q);
            AddParameter("count", count.ToString());
            AddParameter("since", since);
            AddParameter("client_source", client_source);
            AddParameter("order", order);
            AddParameter("title_only", title_only);

            ArticlesProxyObject o = GetArray();
            return o.articles.AsEnumerable();
        }
    }
}
