using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace BenjaminSchroeter.GeoNames
{
    //<entry>
    //<lang>en</lang>
    //<title>Oberblegisee</title>
    //<summary>
    //Oberblegisee is a lake in the Canton of Glarus, Switzerland. It is located at an elevation of 1422 m, above Luchsingen. Its surface area is 0.17 km².  (...)
    //</summary>
    //<feature>waterbody</feature>
    //<countryCode>CH</countryCode>
    //<population>0</population>
    //<elevation>0</elevation>
    //<lat>46.9808</lat>
    //<lng>9.0133</lng>
    //<wikipediaUrl>http://en.wikipedia.org/wiki/Oberblegisee</wikipediaUrl>
    //<thumbnailImg/>
    //<distance>2.3589</distance>
    //</entry>

    [DebuggerDisplay("{Title} ({Language})")]
    public class WikipediaArticle : GeonameDataBase
    {
        #region properties
        [Category("Data")]
        public string Language { get; private set; }
        [Category("Names")]
        public string Title { get; private set; }
        [Category("Names")]
        public string Summary { get; private set; }
        [Category("Codes")]
        public string Feature { get; private set; }
        [Category("Codes")]
        public string CountryCode { get; private set; }

        [Category("Url")]
        public string WikipediaUrl { get; private set; }
        [Category("Url")]
        public string ThumbnailImg { get; private set; }

        [Category("Position")]
        public decimal Latitude { get; private set; }
        [Category("Position")]
        public decimal Longitude { get; private set; }
        [Category("Position")]
        public decimal Distance { get; private set; }
        #endregion

        public WikipediaArticle(string genonameXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(genonameXml);

            if (doc.DocumentElement == null)
                return;

            XmlNodeList root = doc.GetElementsByTagName("entry");
            if ( root.Count == 0 )
                return;

            xml = (XmlElement) root[0];

            Init();
        }

        public WikipediaArticle(XmlElement geoname)
        {
            xml = geoname;
            Init();
        }

        private void Init()
        {
            this.Language = GetElement(xml, "lang");
            this.Title = GetElement(xml, "title");
            this.Summary = GetElement(xml, "summary");
            this.Feature = GetElement(xml, "feature");
            this.CountryCode = GetElement(xml, "countryCode");

            this.WikipediaUrl = GetElement(xml, "wikipediaUrl");
            this.ThumbnailImg = GetElement(xml, "thumbnailImg");

            this.Latitude = ToDecimal(GetElement(xml, "lat"));
            this.Longitude = ToDecimal(GetElement(xml, "lng"));
            this.Distance = ToDecimal(GetElement(xml, "distance"));
        }
    }
}