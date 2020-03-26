using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigitalClaimT
{
   public class clsConexion
    {
        public static bool IsInternet()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                // write your code if there is no Internet available      
                return false;
            }
        }
        public static async Task<dynamic> getServiceData(string queryString)
        {
            dynamic quote = null;
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(queryString);

            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                quote = JsonConvert.DeserializeObject(json);
            }

            return quote;
        }
        public static async Task<string> GetQuote(string u,string p)
        {
            string queryString = "http://localhost:50479/Service1.svc/ValidateLogin/" + u + "," + p;

            dynamic results = await getServiceData(queryString).ConfigureAwait(false);

            if (results != null)
            {
                
                return results;
            }
            else
            {
                return null;
            }
        }
    }
}
