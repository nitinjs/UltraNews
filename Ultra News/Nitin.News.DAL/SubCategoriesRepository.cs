using Nitin.News.DAL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitin.News.DAL
{
    public class SubCategoriesRepository : FeedzillaClient<Subcategory>
    {
        /// <summary>
        /// /v1/subcategories.format
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Subcategory> SubCategories(string culture_code, string order)
        {
            MethodURL = "subcategories";
            AddParameter("culture_code", culture_code);
            AddParameter("order", order);

            return Get();
        }

        /// <summary>
        /// v1/categories/{category_id}/subcategories.format
        /// </summary>
        /// <param name="category_id"></param>
        /// <param name="culture_code"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<Subcategory> SubCategories(string category_id, string culture_code, string order)
        {
            MethodURL = string.Format("categories/{0}/subcategories", category_id);
            AddParameter("culture_code", culture_code);
            AddParameter("order", order);

            return Get();
        }
    }
}
