using BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class EmployerApiClient
    {
        private string BaseURL;        

        public EmployerApiClient(string baseURL)
        {
            BaseURL = baseURL;
        }

        public async Task<EmployerEntity> CreateEmployerApiClient(EmployerEntity employerdata)
        {
            using (var client = new HttpClient())
            {
                var path = "/api/Employer/formprocessing/create/employerdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                var employerdataJson = JsonConvert.SerializeObject(employerdata);

                HttpContent content = new StringContent(employerdataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<EmployerEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        public async Task<List<EmployerEntity>> GetEmployerListApiClient()
        {
            var path = string.Format("/api/Employer/formprocessing/getemployerlist");
            return await ClientAPIGet<List<EmployerEntity>>(path);
        }

        public async Task<List<EmployerEntity>> GetEmployerClassNameApiClient()
        {
            var path = string.Format("/api/Employer/formprocessing/getemployerclass");
            return await ClientAPIGet<List<EmployerEntity>>(path);
        }
        public async Task<List<EmployerEntity>> GetContractListApiClient()
        {
            var path = string.Format("/api/Employer/formprocessing/getcontractlist");
            return await ClientAPIGet<List<EmployerEntity>>(path);
        }
        private async Task<T> ClientAPIGet<T>(string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var results = await ApiGetClient.ExecuteAsync<T>(client, path);
                return results;
            }
        }
    }
}
