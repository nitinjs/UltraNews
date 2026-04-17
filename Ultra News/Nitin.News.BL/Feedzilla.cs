using Nitin.News.DAL;
using Nitin.News.DAL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitin.News.BL
{
    public class Feedzilla
    {
        #region "Enums & Constants"
        public const string DefaultCulture = "en_us";
        public enum CultureOrder
        {
            Popular,
            None
        }
        public enum ArticleOrder
        {
            Relevance,
            Date
        }
        #endregion

        public static IEnumerable<Culture> Cultures()
        {
            CulturesRepository rep = new CulturesRepository();
            return rep.Cultures();
        }

        public static IEnumerable<Category> Categories(string culture = Feedzilla.DefaultCulture, CultureOrder order = CultureOrder.None)
        {
            CategoriesRepository rep = new CategoriesRepository();
            var cats = from p in rep.Categories(culture, order == CultureOrder.Popular ? "popular" : "none")
                           where string.IsNullOrWhiteSpace(p.display_category_name)==false
                           select p;
            var NewCat = new List<Category>{
                new Category(){display_category_name = "News Around Me",english_category_name="News Around Me",category_id="0"}};
            return NewCat.Union(cats);
        }

        public static IEnumerable<Subcategory> SubCategories(string culture = Feedzilla.DefaultCulture, CultureOrder order = CultureOrder.None, string categoryId = "")
        {
            SubCategoriesRepository rep = new SubCategoriesRepository();
            return string.IsNullOrWhiteSpace(categoryId) ?
                rep.SubCategories(culture, order == CultureOrder.Popular ? "popular" : "none", categoryId)
                : rep.SubCategories(culture, order == CultureOrder.Popular ? "popular" : "none");
        }

        public static IEnumerable<Article> Articles(Category category, DateTime since, string client_source, ArticleOrder order = ArticleOrder.Date, int count = 20, bool title_only = false)
        {
            ArticlesRepository rep = new ArticlesRepository();
            return (from p in rep.Articles(category.category_id, count, since.ToString("yyyy-MM-dd"), client_source, order == ArticleOrder.Date ? "date" : "relevance", title_only ? "1" : "0")
                    where string.IsNullOrWhiteSpace(p.summary)==false
                    select p);
        }

        public static IEnumerable<Article> Articles(Category category, Subcategory subcategory, DateTime since, string client_source, ArticleOrder order = ArticleOrder.Date, int count = 20, bool title_only = false)
        {
            ArticlesRepository rep = new ArticlesRepository();
            return (from p in rep.Articles(category.category_id, subcategory.subcategory_id, count, since.ToString("yyyy-MM-dd"), client_source, order == ArticleOrder.Date ? "date" : "relevance", title_only ? "1" : "0")
                            where string.IsNullOrWhiteSpace(p.summary)==false
                    select p);
        }

        public static IEnumerable<Article> Articles(string query, Category category, DateTime since, string client_source, ArticleOrder order = ArticleOrder.Date, int count = 20, bool title_only = false)
        {
            ArticlesRepository rep = new ArticlesRepository();
            return (from p in rep.Search(query, category.category_id, count, since.ToString("yyyy-MM-dd"), client_source, order == ArticleOrder.Date ? "date" : "relevance", title_only ? "1" : "0")
                            where string.IsNullOrWhiteSpace(p.summary)==false
                    select p);
        }

        public static IEnumerable<Article> Articles(string query, DateTime since, string client_source, ArticleOrder order = ArticleOrder.Date, int count = 20, bool title_only = false)
        {
            ArticlesRepository rep = new ArticlesRepository();
            return (from p in rep.Search(query, count, since.ToString("yyyy-MM-dd"), client_source, order == ArticleOrder.Date ? "date" : "relevance", title_only ? "1" : "0")
                    where string.IsNullOrWhiteSpace(p.summary) == false
                    select p);
        }

        public static IEnumerable<Article> Search(string query, Category category, Subcategory subcategory, DateTime since, string client_source, ArticleOrder order = ArticleOrder.Date, int count = 20, bool title_only = false)
        {
            ArticlesRepository rep = new ArticlesRepository();
            return (from p in rep.Search(query, category.category_id, subcategory.subcategory_id, count, since.ToString("yyyy-MM-dd"), client_source, order == ArticleOrder.Date ? "date" : "relevance", title_only ? "1" : "0")
                            where string.IsNullOrWhiteSpace(p.summary)==false
                    select p);
        }
    }
}
