using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections;

namespace Nitin.News.DAL
{
    public class FeedzillaClient<T>
    {
        protected readonly string _endpoint;
        protected readonly string _format;

        private string _MethodURL = "";
        public string MethodURL
        {
            get
            {
                return _MethodURL;
            }
            set
            {
                if (_MethodURL == "")
                {
                    _MethodURL = GetMethodURL(value);
                }
                else
                {

                    _MethodURL = value;
                }
            }
        }

        public enum ReturnFormats
        {
            json, xml, rss, atom
        }

        public FeedzillaClient()
        {
            _format = "json";
            _endpoint = "http://api.feedzilla.com/v1/";
        }

        public FeedzillaClient(ReturnFormats format)
            : this()
        {
            _endpoint = "http://api.feedzilla.com/v1/";
            switch (format)
            {
                case ReturnFormats.atom:
                    _format = "atom";
                    break;
                case ReturnFormats.json:
                    _format = "json";
                    break;
                case ReturnFormats.rss:
                    _format = "rss";
                    break;
                case ReturnFormats.xml:
                    _format = "xml";
                    break;
                default:
                    _format = "json";
                    break;
            }
        }

        public IEnumerable<T> Get()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_endpoint);

            //Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // call the REST method
            HttpResponseMessage response = client.GetAsync(MethodURL).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                return response.Content.ReadAsAsync<IEnumerable<T>>().Result;
            }
            else
            {
                throw new Exception(string.Format("Data access faild,{0} ({1}) method:{2}", (int)response.StatusCode, response.ReasonPhrase, MethodURL));
            }
        }

        public T GetArray()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_endpoint);

            //Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // call the REST method
            HttpResponseMessage response = client.GetAsync(MethodURL).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                return response.Content.ReadAsAsync<T>().Result;
            }
            else
            {
                throw new Exception(string.Format("Data access faild,{0} ({1}) method:{2}", (int)response.StatusCode, response.ReasonPhrase, MethodURL));
            }
        }

        public string GetMethodURL(string methodName)
        {
            return _endpoint + methodName + "." + _format;
        }

        public FeedzillaClient<T> AddParameter(string key, string value)
        {
            if (MethodURL.EndsWith("&"))
            {
                MethodURL += key + "=" + System.Web.HttpUtility.UrlEncode(value) + "&";
            }
            else
            {
                MethodURL += "?" + key + "=" + System.Web.HttpUtility.UrlEncode(value) + "&";
            }
            return this;
        }
    }
}