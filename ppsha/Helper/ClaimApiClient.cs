using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ppsha.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ppsha.Helper
{
    public class ClaimApiClient
    {
        private string BaseURL;

        public ClaimApiClient(string baseURL)
        {
            BaseURL = baseURL;
        }
        public async Task<ClaimEntity> CreateClaimApiClient(ClaimEntity claimEntity, string tokenString)
        {
            using (var client = new HttpClient())
            {
                var path = "/api/Claim/formprocessing/createclaim/claimdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                HttpContent content = new StringContent(claimdataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }       
        public async Task<ResultData> ClaimOCRAsyncApiClientForMultiplePdf(ClaimRequestModel claimreqModel, string tokenString)
        { 
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var path = "/api/Claim/formprocessing/claimocrasyncformultiplepdf/";
                var jsonString = JsonConvert.SerializeObject(claimreqModel);
                var results = await ApiGetClient.PostAsync<ResultData>(client, path, jsonString);
                return results;
            }
        }
        public async Task<bool> UpdatePostedClaimIds(string[] claimIds, string tokenString)
        {
            var path = string.Format("/api/Claim/formprocessing/updatepostedclaimids/{0}", string.Join("', '", claimIds));
            return await ClientAPIGet<bool>(path, tokenString);
        }
        public async Task<List<ClaimEntity>> GetUnprocessedClaimIds(string tokenString)
        {
            var path = string.Format("/api/Claim/formprocessing/GetUnprocessedClaimIds");
            return await ClientAPIGet<List<ClaimEntity>>(path, tokenString);
        }
        public async Task<List<ClaimEntity>> GetProcessedClaimIds(string tokenString)
        {
            var path = string.Format("/api/Claim/formprocessing/getprocessedclaimids");
            return await ClientAPIGet<List<ClaimEntity>>(path, tokenString);
        }
        public async Task<List<ClaimEntity>> GetStructuredJsonByClaimId(string claimId, string tokenString)
        {
            var path = string.Format("/api/Claim/formprocessing/getclaimjsonbyclaimid/{0}", claimId);
            return await ClientAPIGet<List<ClaimEntity>>(path, tokenString);
        }
        private async Task<T> ClientAPIGet<T>(string path, string tokenString)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var results = await ApiGetClient.ExecuteAsync<T>(client, path);
                return results;
            }
        }
    }
}
