using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WitnessReport.Models;
using WitnessReport.Services.Interfaces;

namespace WitnessReport.Services
{
    public class BaseHttpClient : IBaseHttpClient
    {
        public async Task<T> Get<T>(string url,
            IDictionary<string, string> headers = null,
            IDictionary<string, string> parameters = null)
        {
            var result = await SendRequest(HttpMethod.Get, url, null, headers, parameters).ConfigureAwait(false);
            return await Deserialize<T>(result).ConfigureAwait(false);
        }

        private async Task<HttpResponseMessage> SendRequest(
            HttpMethod method,
            string url,
            object data = null,
            IDictionary<string, string> headers = null,
            IDictionary<string, string> parameters = null)
        {
            HttpResponseMessage result;

            using (var client = new HttpClient())
            {
                var requestMessage = GetHttpRequestMessage(method, url, headers, parameters);
                requestMessage.Content = GetMessageContent(data);

                result = await client.SendAsync(requestMessage);
                await ErrorCheck(result);
            }

            return result;
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage result)
        {
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var obj = !string.IsNullOrEmpty(content) ? JsonConvert.DeserializeObject<T>(content) : default(T);
            return obj;
        }

        private static HttpRequestMessage GetHttpRequestMessage(HttpMethod method,
            string resource,
            IDictionary<string, string> headers = null,
            IDictionary<string, string> parameters = null)
        {
            var message = new HttpRequestMessage(method, resource);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (headers != null)
            {
                foreach (var (key, value) in headers)
                {
                    message.Headers.Add(key, value);
                }
            }

            if (parameters != null)
            {
                foreach (var (key, value) in parameters)
                {
                    message.Properties.Add(key, value);
                }
            }

            return message;
        }

        private HttpContent GetMessageContent(object data)
        {
            return data == null ? null : new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        private async Task ErrorCheck(HttpResponseMessage result)
        {
            if (!result.IsSuccessStatusCode)
            {
                var errorMessage = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new ApiException($"Error when calling {result.RequestMessage.Method.ToString().ToUpperInvariant()}.",
                    result.StatusCode,
                    errorMessage,
                    null);
            }
        }
    }
}
