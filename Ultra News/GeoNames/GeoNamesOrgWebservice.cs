using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using System.Xml;

namespace BenjaminSchroeter.GeoNames
{
    public enum GeoNamesDataStyle
    {
        Short,
        Medium,
        Long,
        Full
    }

    public enum ElevationModel
    {
        GTOPO30,
        SRTM3
    }

    /// <summary>
    /// An interface to Webservice from http://www.geonames.org.
    /// </summary>
    /// <remarks>
    /// For more information see http://www.geonames.org/export/ws-overview.html.
    /// 
    /// Currently only the following webservice calls are supported:
    ///  - findNearby
    ///  - findNearbyPlaceName
    ///  - findNearByWeather
    ///  - findNearbyWikipedia
    ///
    ///  - get
    ///  - hierarchy
    ///  - children
    /// 
    ///  - gtopo30
    ///  - srtm3
    /// </remarks>
    public static class GeoNamesOrgWebservice
    {
        #region FindNearby
        /// <summary>
        /// Finds the nearby toponym optional only by a given feature class or feature code.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>Returns the closest toponym for the lat/lng query, optional only toponyms by a given feature class or feature code.</returns>
        /// <remarks>
        /// From the geonames.org forum
        /// "I should have added that this service is not yet documented since it is impossible to performance optimize 
        /// it for all combinations of our 650 feature codes. If the above combination of codes is requested pretty often 
        /// we will add a database index to speed it up."
        /// 
        /// http://forum.geonames.org/gforum/posts/list/581.page#2667
        /// </remarks>
        /// <example>http://ws.geonames.org/findNearby?lat=48.865618158309374&lng=2.344207763671875&featureClass=P&featureCode=PPLA&featureCode=PPL&featureCode=PPLC</example>
        public static Geoname FindNearby(decimal latitude, decimal longitude)
        {
            return FindNearby(latitude, longitude, GeoNamesDataStyle.Medium);
        }
        /// <summary>
        /// Finds the nearby toponym optional only by a given feature class or feature code.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="style">A style describing the data to receive.</param>
        /// <returns>Returns the closest toponym for the lat/lng query, optional only toponyms by a given feature class or feature code.</returns>
        /// <remarks>
        /// From the geonames.org forum
        /// "I should have added that this service is not yet documented since it is impossible to performance optimize 
        /// it for all combinations of our 650 feature codes. If the above combination of codes is requested pretty often 
        /// we will add a database index to speed it up."
        /// 
        /// http://forum.geonames.org/gforum/posts/list/581.page#2667
        /// </remarks>
        /// <example>http://ws.geonames.org/findNearby?lat=48.865618158309374&lng=2.344207763671875&featureClass=P&featureCode=PPLA&featureCode=PPL&featureCode=PPLC</example>
        public static Geoname FindNearby(decimal latitude, decimal longitude, GeoNamesDataStyle style)
        {
            return FindNearby(latitude, longitude, new string[] { }, new string[] { }, style);
        }
        /// <summary>
        /// Finds the nearby toponym optional only by a given feature class or feature code.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="featureclasses">The featureclasses or emtpy for all; see http://www.geonames.org/export/codes.html </param>
        /// <returns>Returns the closest toponym for the lat/lng query, optional only toponyms by a given feature class or feature code.</returns>
        /// <remarks>
        /// From the geonames.org forum
        /// "I should have added that this service is not yet documented since it is impossible to performance optimize 
        /// it for all combinations of our 650 feature codes. If the above combination of codes is requested pretty often 
        /// we will add a database index to speed it up."
        /// 
        /// http://forum.geonames.org/gforum/posts/list/581.page#2667
        /// </remarks>
        /// <example>http://ws.geonames.org/findNearby?lat=48.865618158309374&lng=2.344207763671875&featureClass=P&featureCode=PPLA&featureCode=PPL&featureCode=PPLC</example>
        public static Geoname FindNearby(decimal latitude, decimal longitude, params string[] featureclasses)
        {
            return FindNearby(latitude, longitude, GeoNamesDataStyle.Medium, featureclasses);
        }
        /// <summary>
        /// Finds the nearby toponym optional only by a given feature class or feature code.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="featureclasses">The featureclasses or emtpy for all; see http://www.geonames.org/export/codes.html </param>
        /// <param name="style">A style describing the data to receive.</param>
        /// <returns>Returns the closest toponym for the lat/lng query, optional only toponyms by a given feature class or feature code.</returns>
        /// <remarks>
        /// From the geonames.org forum
        /// "I should have added that this service is not yet documented since it is impossible to performance optimize 
        /// it for all combinations of our 650 feature codes. If the above combination of codes is requested pretty often 
        /// we will add a database index to speed it up."
        /// 
        /// http://forum.geonames.org/gforum/posts/list/581.page#2667
        /// </remarks>
        /// <example>http://ws.geonames.org/findNearby?lat=48.865618158309374&lng=2.344207763671875&featureClass=P&featureCode=PPLA&featureCode=PPL&featureCode=PPLC</example>
        public static Geoname FindNearby(decimal latitude, decimal longitude, GeoNamesDataStyle style, params string[] featureclasses)
        {
            return FindNearby(latitude, longitude, featureclasses, new string[] {}, style) ;
        }
        /// <summary>
        /// Finds the nearby toponym optional only by a given feature class or feature code.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="featureclasses">The featureclasses or emtpy for all; see http://www.geonames.org/export/codes.html </param>
        /// <param name="featurecodes">The featurecodes or emtpy for all; see http://www.geonames.org/export/codes.html </param>
        /// <returns>Returns the closest toponym for the lat/lng query, optional only toponyms by a given feature class or feature code.</returns>
        /// <remarks>
        /// From the geonames.org forum
        /// "I should have added that this service is not yet documented since it is impossible to performance optimize 
        /// it for all combinations of our 650 feature codes. If the above combination of codes is requested pretty often 
        /// we will add a database index to speed it up."
        /// 
        /// http://forum.geonames.org/gforum/posts/list/581.page#2667
        /// </remarks>
        /// <example>http://ws.geonames.org/findNearby?lat=48.865618158309374&lng=2.344207763671875&featureClass=P&featureCode=PPLA&featureCode=PPL&featureCode=PPLC</example>
        public static Geoname FindNearby(decimal latitude, decimal longitude, string[] featureclasses, string[] featurecodes)
        {
            return FindNearby(latitude, longitude, featureclasses, featurecodes, GeoNamesDataStyle.Medium);
        }
        /// <summary>
        /// Finds the nearby toponym optional only by a given feature class or feature code.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="featureclasses">The featureclasses or emtpy for all; see http://www.geonames.org/export/codes.html </param>
        /// <param name="featurecodes">The featurecodes or emtpy for all; see http://www.geonames.org/export/codes.html </param>
        /// <param name="style">A style describing the data to receive.</param>
        /// <returns>Returns the closest toponym for the lat/lng query, optional only toponyms by a given feature class or feature code.</returns>
        /// <remarks>
        /// From the geonames.org forum
        /// "I should have added that this service is not yet documented since it is impossible to performance optimize 
        /// it for all combinations of our 650 feature codes. If the above combination of codes is requested pretty often 
        /// we will add a database index to speed it up."
        /// 
        /// http://forum.geonames.org/gforum/posts/list/581.page#2667
        /// </remarks>
        /// <example>http://ws.geonames.org/findNearby?lat=48.865618158309374&lng=2.344207763671875&featureClass=P&featureCode=PPLA&featureCode=PPL&featureCode=PPLC</example>
        public static Geoname FindNearby(decimal latitude, decimal longitude, string[] featureclasses, string[] featurecodes, GeoNamesDataStyle style)
        {
            StringBuilder appendix = new StringBuilder();
            foreach (string s in featureclasses)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    appendix.Append("&featureClass=");
                    appendix.Append(s);
                }
            }
            foreach (string s in featurecodes)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    appendix.Append("&featureCode=");
                    appendix.Append(s);
                }
            }

            NumberFormatInfo format = new NumberFormatInfo {NumberDecimalSeparator = "."};
            string url = String.Format(format, "http://ws.geonames.org/findNearby?lat={0}&lng={1}&style={3}{2}", latitude, longitude, appendix, style);
            Debug.WriteLine("Requesting " + url);

            string xml = CreateWebClient().DownloadString(url);
            GeonameException.ThrowOnError(xml);

            return new Geoname(xml);
        }
        #endregion

        #region FindNearbyPlaceName
        /// <summary>
        /// Find nearby populated place.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>Returns the closest populated place for the lat/lng query.</returns>
        /// <remarks>
        /// http://www.geonames.org/export/web-services.html#findNearbyPlaceName
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/findNearbyPlaceName?lat=47.3&lng=9
        /// </example>
        public static Geoname FindNearbyPlaceName(decimal latitude, decimal longitude)
        {
            return FindNearbyPlaceName(latitude, longitude, GeoNamesDataStyle.Medium);
        }
        /// <summary>
        /// Find nearby populated place.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="style">A style describing the data to receive.</param>
        /// <returns>Returns the closest populated place for the lat/lng query</returns>
        /// <remarks>
        /// http://www.geonames.org/export/web-services.html#findNearbyPlaceName
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/findNearbyPlaceName?lat=47.3&lng=9
        /// </example>
        public static Geoname FindNearbyPlaceName(decimal latitude, decimal longitude, GeoNamesDataStyle style)
        {
            return FindNearbyPlaceName(latitude, longitude, 0, style);                        
        }
        /// <summary>
        /// Find nearby populated place.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="radius">The radius in km.</param>
        /// <returns>Returns the closest populated place for the lat/lng query</returns>
        /// <remarks>
        /// http://www.geonames.org/export/web-services.html#findNearbyPlaceName
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/findNearbyPlaceName?lat=47.3&lng=9
        /// </example>
        public static Geoname FindNearbyPlaceName(decimal latitude, decimal longitude, decimal radius)
        {
            return FindNearbyPlaceName(latitude, longitude, radius, GeoNamesDataStyle.Medium);
        }
        /// <summary>
        /// Find nearby populated place.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="radius">The radius in km.</param>
        /// <param name="style">A style describing the data to receive.</param>
        /// <returns>Returns the closest populated place for the lat/lng query</returns>
        /// <remarks>
        /// http://www.geonames.org/export/web-services.html#findNearbyPlaceName
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/findNearbyPlaceName?lat=47.3&lng=9
        /// </example>
        public static Geoname FindNearbyPlaceName(decimal latitude, decimal longitude, decimal radius, GeoNamesDataStyle style)
        {
            NumberFormatInfo format = new NumberFormatInfo {NumberDecimalSeparator = "."};

            string appendix = "";
            if (radius > 0)
                appendix += string.Format(format, "&radius={0}", radius);

            string url = String.Format(format, "http://ws.geonames.org/findNearbyPlaceName?lat={0}&lng={1}&style={3}{2}", latitude, longitude, appendix, style);
            Debug.WriteLine("Requesting " + url);       

            string xml = CreateWebClient().DownloadString(url);
            GeonameException.ThrowOnError(xml);

            return new Geoname(xml);
        }
        #endregion

        #region FindNearbyWikipedia
        /// <summary>
        /// Find nearby Wikipedia Entries.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>A list of wikipedia entries</returns>
        /// <remarks>
        /// http://www.geonames.org/export/wikipedia-webservice.html#findNearbyWikipedia
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/findNearbyWikipedia?lat=47&lng=9
        /// </example>
        public static List<WikipediaArticle> FindNearbyWikipedia(decimal latitude, decimal longitude)
        {
            return FindNearbyWikipedia(latitude, longitude, "", 0, 0);
        }
        /// <summary>
        /// Find nearby Wikipedia Entries.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="language">The language of the entries.</param>
        /// <returns>A list of wikipedia entries</returns>
        /// <remarks>
        /// http://www.geonames.org/export/wikipedia-webservice.html#findNearbyWikipedia
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/findNearbyWikipedia?lat=47&lng=9
        /// </example>
        public static List<WikipediaArticle> FindNearbyWikipedia(decimal latitude, decimal longitude, string language)
        {
            return FindNearbyWikipedia(latitude, longitude, language, 0, 0);
        }
        /// <summary>
        /// Find nearby Wikipedia Entries.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="language">The language of the entries.</param>
        /// <param name="radius">The radius in km.</param>
        /// <returns>A list of wikipedia entries</returns>
        /// <remarks>
        /// http://www.geonames.org/export/wikipedia-webservice.html#findNearbyWikipedia
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/findNearbyWikipedia?lat=47&lng=9
        /// </example>
        public static List<WikipediaArticle> FindNearbyWikipedia(decimal latitude, decimal longitude, string language, decimal radius)
        {
            return FindNearbyWikipedia(latitude, longitude, language, radius, 0);
        }
        /// <summary>
        /// Find nearby Wikipedia Entries.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="radius">The radius in km.</param>
        /// <returns>A list of wikipedia entries</returns>
        /// <remarks>
        /// http://www.geonames.org/export/wikipedia-webservice.html#findNearbyWikipedia
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/findNearbyWikipedia?lat=47&lng=9
        /// </example>
        public static List<WikipediaArticle> FindNearbyWikipedia(decimal latitude, decimal longitude, decimal radius)
        {
            return FindNearbyWikipedia(latitude, longitude, "", radius, 0);
        }
        /// <summary>
        /// Find nearby Wikipedia Entries.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="language">The language of the entries.</param>
        /// <param name="radius">The radius in km.</param>
        /// <param name="maxRows">The number of rows to return.</param>
        /// <returns>A list of wikipedia entries</returns>
        /// <remarks>
        /// http://www.geonames.org/export/wikipedia-webservice.html#findNearbyWikipedia
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/findNearbyWikipedia?lat=47&lng=9
        /// </example>
        public static List<WikipediaArticle> FindNearbyWikipedia(decimal latitude, decimal longitude, string language, decimal radius, int maxRows)
        {
            string url = FindNearbyWikipediaUrl(language, radius, maxRows, latitude, longitude);
            Debug.WriteLine("Requesting " + url);

            string xml = CreateWebClient().DownloadString(url);
            GeonameException.ThrowOnError(xml);

            return CreateWikipediaArticleList(xml);
        }

        private static string FindNearbyWikipediaUrl(string language, decimal radius, int maxRows, decimal latitude, decimal longitude)
        {
            NumberFormatInfo format = new NumberFormatInfo { NumberDecimalSeparator = "." };

            string appendix = "";
            if (!string.IsNullOrEmpty(language))
                appendix += string.Format("&lang={0}", language);
            if (radius > 0)
                appendix += string.Format(format, "&radius={0}", radius);
            if (maxRows > 0)
                appendix += string.Format(format, "&maxRows={0}", maxRows);

            return String.Format(format, "http://ws.geonames.org/findNearbyWikipedia?lat={0}&lng={1}{2}", latitude, longitude, appendix);
        }
        private static List<WikipediaArticle> CreateWikipediaArticleList(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (doc.DocumentElement == null)
                return new List<WikipediaArticle>();

            XmlNodeList root = doc.GetElementsByTagName("geonames");
            if (root.Count == 0)
                return new List<WikipediaArticle>();
            XmlElement geonames = (XmlElement)root[0];

            List<WikipediaArticle> articles = new List<WikipediaArticle>();
            foreach (XmlElement element in geonames.GetElementsByTagName("entry"))
            {
                WikipediaArticle n = new WikipediaArticle(element);
                articles.Add(n);
            }

            return articles;
        }
        #endregion

        /// <summary>
        /// Finds the Weather Station with most recent weather observation.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>A weather station closest to this given point with the most recent weather observation.</returns>
        /// <remarks>
        /// http://www.geonames.org/export/JSON-webservices.html#findNearByWeatherJSON
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/findNearByWeather?lat=42&lng=-2&style=full
        /// </example>
        public static WeatherObservation FindNearbyWeather(decimal latitude, decimal longitude)
        {
            NumberFormatInfo format = new NumberFormatInfo {NumberDecimalSeparator = "."};

            string url = String.Format(format, "http://ws.geonames.org/findNearByWeatherXML?lat={0}&lng={1}", latitude, longitude);
            Debug.WriteLine("Requesting " + url);

            string xml = CreateWebClient().DownloadString(url);
            GeonameException.ThrowOnError(xml);

            return new WeatherObservation(xml);
        }


        /// <summary>
        /// Gets the specified Geoname Data of a given geoname Id.
        /// </summary>
        /// <param name="geonameId">The geoname id.</param>
        /// <remarks>
        /// http://www.geonames.org/
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/get?geonameId=6295630&style=full
        /// </example>
        public static Geoname Get(int geonameId)
        {
            string url = String.Format("http://ws.geonames.org/get?geonameId={0}", geonameId);
            Debug.WriteLine("Requesting " + url);

            string xml = CreateWebClient().DownloadString(url);
            GeonameException.ThrowOnError(xml);

            return new Geoname(xml);
        }

        #region Hierarchy and Children
        /// <summary>
        /// Returns all GeoNames higher up in the hierarchy of a place name. 
        /// </summary>
        /// <param name="geoname">The geoname instance</param>
        /// <returns>a list of GeoName records, ordered by hierarchy level. The top hierarchy (continent) is the first element in the list.</returns>
        /// <remarks>
        /// http://www.geonames.org/export/place-hierarchy.html#hierarchy
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/hierarchy?geonameId=2657896&style=full
        /// </example>
        public static List<Geoname> Hierarchy(Geoname geoname)
        {
            return Hierarchy(geoname, GeoNamesDataStyle.Medium);
        }
        /// <summary>
        /// Returns all GeoNames higher up in the hierarchy of a place name. 
        /// </summary>
        /// <param name="geoname">The geoname instance</param>
        /// <param name="style">A style describing the data to receive.</param>
        /// <returns>a list of GeoName records, ordered by hierarchy level. The top hierarchy (continent) is the first element in the list.</returns>
        /// <remarks>
        /// http://www.geonames.org/export/place-hierarchy.html#hierarchy
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/hierarchy?geonameId=2657896&style=full
        /// </example>
        public static List<Geoname> Hierarchy(Geoname geoname, GeoNamesDataStyle style)
        {
            return Hierarchy(geoname.Id, style);
        }
        /// <summary>
        /// Returns all GeoNames higher up in the hierarchy of a place name. 
        /// </summary>
        /// <param name="geonameId">The geoname id.</param>
        /// <returns>a list of GeoName records, ordered by hierarchy level. The top hierarchy (continent) is the first element in the list.</returns>
        /// <remarks>
        /// http://www.geonames.org/export/place-hierarchy.html#hierarchy
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/hierarchy?geonameId=2657896&style=full
        /// </example>
        public static List<Geoname> Hierarchy(int geonameId)
        {
            return Hierarchy(geonameId, GeoNamesDataStyle.Medium);
        }
        /// <summary>
        /// Returns all GeoNames higher up in the hierarchy of a place name. 
        /// </summary>
        /// <param name="geonameId">The geoname id.</param>
        /// <param name="style">A style describing the data to receive.</param>
        /// <returns>a list of GeoName records, ordered by hierarchy level. The top hierarchy (continent) is the first element in the list.</returns>
        /// <remarks>
        /// http://www.geonames.org/export/place-hierarchy.html#hierarchy
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/hierarchy?geonameId=2657896&style=full
        /// </example>
        public static List<Geoname> Hierarchy(int geonameId, GeoNamesDataStyle style)
        {
            string url = String.Format("http://ws.geonames.org/hierarchy?geonameId={0}&style={1}", geonameId, style);
            Debug.WriteLine("Requesting " + url);       

            string xml = CreateWebClient().DownloadString(url);
            GeonameException.ThrowOnError(xml);

            return CreateGeonameList(xml);
        }

        /// <summary>
        /// Returns the children for a given geonameId. 
        /// 
        /// The children are the administrative divisions within an other administrative division.
        /// Like the counties (ADM2) in a state (ADM1) or also the countries in a continent. 
        /// </summary>
        /// <param name="geoname">The geoname instance.</param>
        /// <returns>a list of GeoName records</returns>
        /// <remarks>
        /// http://www.geonames.org/export/place-hierarchy.html#children
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/children?geonameId=3175395&style=full
        /// </example>
        public static List<Geoname> Children(Geoname geoname)
        {
            return Children(geoname, GeoNamesDataStyle.Medium);
        }
        /// <summary>
        /// Returns the children for a given geonameId. 
        /// 
        /// The children are the administrative divisions within an other administrative division.
        /// Like the counties (ADM2) in a state (ADM1) or also the countries in a continent. 
        /// </summary>
        /// <param name="geoname">The geoname instance.</param>
        /// <param name="style">A style describing the data to receive.</param>
        /// <returns>a list of GeoName records</returns>
        /// <remarks>
        /// http://www.geonames.org/export/place-hierarchy.html#children
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/children?geonameId=3175395&style=full
        /// </example>
        public static List<Geoname> Children(Geoname geoname, GeoNamesDataStyle style)
        {
            return Children(geoname.Id, style);
        }
        /// <summary>
        /// Returns the children for a given geonameId. 
        /// 
        /// The children are the administrative divisions within an other administrative division.
        /// Like the counties (ADM2) in a state (ADM1) or also the countries in a continent. 
        /// </summary>
        /// <param name="geonameId">The geoname id.</param>
        /// <returns>a list of GeoName records</returns>
        /// <remarks>
        /// http://www.geonames.org/export/place-hierarchy.html#children
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/children?geonameId=3175395&style=full
        /// </example>
        public static List<Geoname> Children(int geonameId)
        {
            return Children(geonameId, GeoNamesDataStyle.Medium);
        }
        /// <summary>
        /// Returns the children for a given geonameId. 
        /// 
        /// The children are the administrative divisions within an other administrative division.
        /// Like the counties (ADM2) in a state (ADM1) or also the countries in a continent. 
        /// </summary>
        /// <param name="geonameId">The geoname id.</param>
        /// <param name="style">A style describing the data to receive.</param>
        /// <returns>a list of GeoName records</returns>
        /// <remarks>
        /// http://www.geonames.org/export/place-hierarchy.html#children
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/children?geonameId=3175395&style=full
        /// </example>
        public static List<Geoname> Children(int geonameId, GeoNamesDataStyle style)
        {
            string url = String.Format("http://ws.geonames.org/children?geonameId={0}&style={1}", geonameId, style);
            Debug.WriteLine("Requesting " + url);

            string xml = CreateWebClient().DownloadString(url);
            GeonameException.ThrowOnError(xml);

            return CreateGeonameList(xml);
        }
        #endregion


        /// <summary>
        /// Gets the elevation for a given place using the GTOPO30 or SRTM3 database.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="model">The model.</param>
        /// <returns>a number giving the elevation in meters according to srtm3 or gtopo30, ocean areas have been masked as "no data" and have been assigned a value of int.MinValue</returns>
        /// <remarks>
        /// GTOPO30 is a global digital elevation model (DEM) with a horizontal grid spacing of
        /// 30 arc seconds (approximately 1 kilometer). 
        /// GTOPO30 was derived from several raster and vector sources of topographic information.
        /// http://www.geonames.org/export/web-services.html#gtopo30
        /// 
        /// Shuttle Radar Topography Mission (SRTM) elevation data. SRTM consisted of a specially 
        /// modified radar system that flew onboard the Space Shuttle Endeavour during an 11-day 
        /// mission in February of 2000. The dataset covers land areas between 60 degrees north 
        /// and 56 degrees south. This web service is using SRTM3 data with data points located 
        /// every 3-arc-second (approximately 90 meters) on a latitude/longitude grid.
        /// http://www.geonames.org/export/web-services.html#srtm3
        /// </remarks>
        /// <example>
        /// http://ws.geonames.org/gtopo30?lat=47.01&lng=10.2
        /// http://ws.geonames.org/srtm3?lat=50.01&lng=10.2
        /// </example>
        public static int GetElevation(decimal latitude, decimal longitude, ElevationModel model)
        {
            string service;
            if (model == ElevationModel.GTOPO30)
                service = "http://ws.geonames.org/gtopo30?lat={0}&lng={1}";
            else
                service = "http://ws.geonames.org/srtm3?lat={0}&lng={1}";

            NumberFormatInfo format = new NumberFormatInfo {NumberDecimalSeparator = "."};

            string url = String.Format(format, service, latitude, longitude);
            Debug.WriteLine("Requesting " + url);

            string data = CreateWebClient().DownloadString(url);

            int value;
            if (!int.TryParse(data, out value))
                return int.MinValue;

            if (value < -1000)
                return int.MinValue;

            return value;
        }

        private static List<Geoname> CreateGeonameList(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (doc.DocumentElement == null)
                return new List<Geoname>();

            XmlNodeList root = doc.GetElementsByTagName("geonames");
            if (root.Count == 0)
                return new List<Geoname>();

            XmlElement geonames = (XmlElement)root[0];

            List<Geoname> names = new List<Geoname>();
            foreach (XmlElement element in geonames.GetElementsByTagName("geoname"))
            {
                Geoname n = new Geoname(element);
                names.Add(n);
            }

            return names;
        }

        private static WebClient webClient;
        private static WebClient CreateWebClient()
        {
            if ( webClient == null )
                webClient = new WebClient { Encoding = Encoding.UTF8 };

            return webClient;
        }
    }
}