using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ppsha.Helper
{
    public class ApiGetClient
    {
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <typeparam name="T">Any Object</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="path">The path.</param>
        /// <returns>Returns Object</returns>
        /// <exception cref="System.ApplicationException">Error requesting API service with URL:</exception>
        public static async Task<T> ExecuteAsync<T>(HttpClient client, string path)
        {
            T output = default(T);
            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                var jsonAsString = await response.Content.ReadAsStringAsync();
                output = JsonConvert.DeserializeObject<T>(jsonAsString);
            }
            else
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                //throw GetHandledMvcExceptionFor(responseBody, path);
                throw new ApplicationException(responseBody.ToString() + ": " + path);
            }

            return output;
        }

        public static async Task<T> PostAsync<T>(HttpClient client, string path, string Json)
        {
            T output = default(T);

            var response = await client.PostAsync(path, new StringContent(Json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var jsonAsString = await response.Content.ReadAsStringAsync();
                output = JsonConvert.DeserializeObject<T>(jsonAsString);
            }
            else
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                //throw GetHandledMvcExceptionFor(responseBody, path);
                throw new ApplicationException(responseBody.ToString() + ": " + path);
            }

            return output;
        }


        public static async Task<T> PostAsyncApiClient<T>(string path, string contentObj, string tokenString)
        {
            T output = default(T);
            using (var client = new HttpClient())
            {
                var uri = new Uri(path);
                var contentJson = contentObj != null ? JsonConvert.SerializeObject(contentObj) : "";
                HttpContent content = new StringContent(contentJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var response = await client.PostAsync(uri, new StringContent(contentObj, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = await response.Content.ReadAsStringAsync();
                    output = JsonConvert.DeserializeObject<T>(jsonAsString);
                }
                else
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    //throw GetHandledMvcExceptionFor(responseBody, path);
                    throw new ApplicationException(responseBody.ToString() + ": " + path);
                }

                return output;
            }
        }

        /// <summary>
        /// Executes the specified client.
        /// </summary>
        /// <typeparam name="T">Any Object</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="path">The path.</param>
        /// <returns>Returns Object</returns>
        /// <exception cref="System.ApplicationException">Error requesting API service with URL:</exception>
        public static T Execute<T>(HttpClient client, string path)
        {
            T output = default(T);
            var response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonAsString = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<T>(jsonAsString);
            }
            else
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                //throw GetHandledMvcExceptionFor(responseBody, path);
                throw new ApplicationException(responseBody.ToString() + ": " + path);
            }

            return output;
        }

        public async static Task<string> PostApiClient(string path, object contentObj, string tokenString)
        {

            using (var client = new HttpClient())
            {
                var uri = new Uri(path);
                var contentJson = contentObj != null ? JsonConvert.SerializeObject(contentObj) : "";
                HttpContent content = new StringContent(contentJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        /// <summary>
        /// Create new instance of handled MVC exception
        /// </summary>
        /// <param name="apiErrorCodeAndMessage">error code and message from API</param>
        /// <param name="url">API url</param>
        /// <returns>new instance of handled MVC exception</returns>
        //private static HandledMvcException GetHandledMvcExceptionFor(string apiErrorCodeAndMessage, string url)
        //{
        //    if (!string.IsNullOrEmpty(apiErrorCodeAndMessage))
        //    {
        //        var parts = apiErrorCodeAndMessage.Split(CustomExceptionsHelper.ErrorCodeAndMessageSeparator);

        //        if (parts.Count() > 1)
        //        {
        //            return new HandledMvcException(parts[0], parts[1]);
        //        }
        //    }

        //    return new HandledMvcException(
        //                                    "OCR_Form_Processing_API_Unhandled_Exception",
        //                                    string.Format("Error requesting API service with URL: {0}", url));
        //}
    }
}
