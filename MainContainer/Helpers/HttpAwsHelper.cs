using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace MainContainer.Helpers
{
    public class HttpAwsHelper
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpAwsHelper(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetResponse(string httpClient, string action,
            params string[] param)
        {
            if (param.Length > 0)
                foreach (var par in param)
                    action += "/" + par;

            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.GetStringAsync(action);
            return JsonConvert.DeserializeObject<string>(response);
        }

        public async Task<string> PostResponse(string httpClient, string action, object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.PostAsync(action, data);
            return response.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> PutResponse(string httpClient, string action, object id, object model)
        {
            Dictionary<string, object> set = new Dictionary<string, object>() { { "id", id }, { "model", model } };
            var json = JsonConvert.SerializeObject(set);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.PutAsync(action, data);
            return response.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> DeleteResponse(string httpClient, string action)
        {
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.DeleteAsync(action);
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
