using Nitin.News.DAL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitin.News.DAL
{
    public class CulturesRepository:FeedzillaClient<Culture>
    {
        /// <summary>
        /// /v1/cultures.format
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Culture> Cultures()
        {
            MethodURL="cultures";

            return Get();
        }
    }
}
