using Nitin.News.DAL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitin.News.DAL
{
    public class CategoriesRepository : FeedzillaClient<Category>
    {
        /// <summary>
        /// /v1/categories.format
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> Categories(string culture_code, string order)
        {
            MethodURL = "categories";
            AddParameter("culture_code", culture_code);
            AddParameter("order", order);

            return Get();
        }
    }
}
