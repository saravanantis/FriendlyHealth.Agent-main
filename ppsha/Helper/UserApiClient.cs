using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ppsha.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ppsha.Helper
{
    public class UserApiClient
    {
        private string BaseURL;
        private MySettings MySettings { get; set; }

        public UserApiClient(string baseURL, MySettings myConstants)
        {
            BaseURL = baseURL;
            MySettings = myConstants;
        }
        public async Task<(string, bool, int)> UserLogin()
        {
            try
            {
                ResultModel result = await UserSignIn(MySettings.UserEmail, MySettings.UserPassword);
                if (result.Status && result.ResetPasswordStatus == false)
                {
                    var environment = Environment.GetEnvironmentVariable(MySettings.EnvironmentName).ToLower();
                    var userid = await CreateCognitoUserApiClientAsync(MySettings.UserEmail, false);                    

                    string token = string.IsNullOrEmpty(result.TokenString) ? GenerateToken(MySettings.UserEmail) : result.TokenString;
                    var TokenString = !string.IsNullOrEmpty(token) ? token : string.Empty;
                    //SessionObj.TokenString = !string.IsNullOrEmpty(token) ? token : string.Empty;
                    //SessionObj.UserId = userid;

                    return (TokenString, result.Status, userid);
                }
                else
                {
                    return ("", false, 0);
                }
            }
            catch (Exception ex)
            {
                return ("", false, 0);
            }
        }
        public string GenerateToken(string username)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuvwxyz-123456789012345678901234567890");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            // remove password before returning

            return tokenString;
        }
        public async Task<ResultModel> UserSignIn(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var _userEntity = new
                {
                    Email = email,
                    Password = password
                };
                var path = "/api/User/formprocessing/UserSignIn";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                return await GetUserResponse(client, _userEntity, uri);
            }
        }
        public async Task<ResultModel> GetUserResponse(HttpClient client, object _userEntity, Uri uri)
        {
            var results = new ResultModel();
            client.DefaultRequestHeaders.Accept.Clear();
            var userDetailsJson = JsonConvert.SerializeObject(_userEntity);
            HttpContent content = new StringContent(userDetailsJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var jsonAsString = response.Content.ReadAsStringAsync().Result;
                results = JsonConvert.DeserializeObject<ResultModel>(jsonAsString);
            }
            return results;
        }
        public async Task<int> CreateCognitoUserApiClientAsync(string email, bool isBatchProcessUser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/createcognitouser/{0}/{1}", email, isBatchProcessUser);
                var results = await ApiGetClient.ExecuteAsync<int>(client, path);
                return results;
            }
        }
    }
}
