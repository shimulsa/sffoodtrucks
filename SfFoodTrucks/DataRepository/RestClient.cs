using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;

namespace SfFoodTrucks.DataRepository
{
    public static class RestClient
    {
        public static T GetResponse<T>(Uri url)
        {
            WebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            using (HttpWebResponse webResp = webRequest.GetResponse() as HttpWebResponse)
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                object objResponse = jsonSerializer.ReadObject(webResp.GetResponseStream());
                T jsonResponse = (T)objResponse;
                return jsonResponse;
            }
        }
    }
}