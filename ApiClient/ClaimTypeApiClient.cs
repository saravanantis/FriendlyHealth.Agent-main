using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ClaimTypeApiClient
    {
        private string BaseURL;

        public ClaimTypeApiClient(string baseURL)
        {
            BaseURL = baseURL;
        }
        public async Task<T> ClientAPIGet<T>(string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                return await ApiGetClient.ExecuteAsync<T>(client, path);
            }
        }
        public async Task<List<ClaimTypeEntity>> GetClaimTypeListApiClient()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/ClaimType/formprocessing/getclaimtypelist");

                var results = await ApiGetClient.ExecuteAsync<List<ClaimTypeEntity>>(client, path);
                return results;
            }
        }

        public async Task<bool> CreateEditClaimType(string typeId, string claimType)
        {
            var path = string.Format("/api/ClaimType/formprocessing/createEditClaimType/{0}/{1}", typeId,claimType);
            bool results = await this.ClientAPIGet<bool>(path);
            return results;

        }
        public async Task<bool> DisableClaimType(int typeId ,bool isDeleted)
        {
            var path = string.Format("/api/ClaimType/formprocessing/disableClaimType/{0}/{1}", typeId, isDeleted);
            bool results = await this.ClientAPIGet<bool>(path);
            return results;

        }
        public async Task<bool> ValidateClaimType(int typeId, bool isRequired)
        {
            var path = string.Format("/api/ClaimType/formprocessing/ValidateClaimType/{0}/{1}", typeId, isRequired);
            bool results = await this.ClientAPIGet<bool>(path);
            return results;

        }
        public async Task<List<ClaimTypeEntity>> GetClaimTypeAllListApiClient()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/ClaimType/formprocessing/getclaimtypefulllist");

                var results = await ApiGetClient.ExecuteAsync<List<ClaimTypeEntity>>(client, path);
                return results;
            }
        }

    }
}
