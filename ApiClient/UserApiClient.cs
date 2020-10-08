using Amazon.CognitoIdentityProvider.Model;
using BusinessObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApiClient
{


    public class UserApiClient
    {
        private string BaseURL;

        public UserApiClient(string baseURL)
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
        public async Task<UserEntity> UserLoginApiClientAsync(string email, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/userlogin/{0}/{1}", email, password);
                var results = await ApiGetClient.ExecuteAsync<UserEntity>(client, path);
                return results;
            }
        }

        public async Task<int> CognitoUserCheckApiClientAsync(string email)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/cognitousercheck/{0}", email);
                var results = await ApiGetClient.ExecuteAsync<int>(client, path);
                return results;
            }
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

        public async Task<AdminInitiateAuthResponse> CognitoUserSignIn(string email, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/CognitoUserSignIn/{0}/{1}", email, password);
                var results = await ApiGetClient.ExecuteAsync<AdminInitiateAuthResponse>(client, path);
                return results;
            }
        }


        public async Task<SignUpResponse> CognitoUserSignUp(string email, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/cognitousersignup/{0}/{1}", email, password);
                var results = await ApiGetClient.ExecuteAsync<SignUpResponse>(client, path);
                return results;
            }


            //using (var client = new HttpClient())
            //{
            //    var path = "/api/User/formprocessing/CognitoUserSignUp";
            //    var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    var claimdataJson = JsonConvert.SerializeObject(signUpRequest);

            //    HttpContent content = new StringContent(claimdataJson);
            //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

            //    var response = await client.PostAsync(uri, content);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var jsonAsString = response.Content.ReadAsStringAsync().Result;
            //        return JsonConvert.DeserializeObject<SignUpResponse>(jsonAsString);
            //    }

            //    throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            //}
        }

        public async Task<ConfirmSignUpResponse> CognitoUserSignUpConfirm(string email, string confirmcode)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/CognitoUserSignUpConfirm/{0}/{1}", email, confirmcode);
                var results = await ApiGetClient.ExecuteAsync<ConfirmSignUpResponse>(client, path);
                return results;
            }

        }

        public async Task<List<ErrorLogEntity>> GetErrorLogList(int PageNo, int Take = 10)
        {
            using (var client = new HttpClient())
            {
                int skip = (PageNo - 1) * Take;
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/Claim/formprocessing/geterrorloglist/{0}/{1}", Take, skip);
                var results = await ApiGetClient.ExecuteAsync<List<ErrorLogEntity>>(client, path);
                return results;
            }
        }

        public async Task<List<TextLogEntity>> TextLogList(int PageNo, int Take = 10)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/Claim/formprocessing/gettextloglist");
                var results = await ApiGetClient.ExecuteAsync<List<TextLogEntity>>(client, path);
                return results;
            }
        }

        public async Task<List<TextLogEntity>> GetTextLogByFileName(string fileName)
        {
            List<TextLogEntity> response = new List<TextLogEntity>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format($"/api/Claim/formprocessing/gettextlogfile/{fileName}");
                HttpResponseMessage results = await client.GetAsync(path);
                if (results.IsSuccessStatusCode)
                {
                    var contents = await results.Content.ReadAsByteArrayAsync();
                    Stream stream = new MemoryStream(contents);
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            TextLogEntity log = new TextLogEntity();
                            var lines = line.Split("[INF]");
                            int c = 0;
                            DateTime dateValue;
                            foreach (var li in lines)
                            {
                                if (c == 0)
                                {
                                    if (DateTime.TryParse(li.Split(" ")[0], out dateValue))
                                        log.LogTime = dateValue.ToString("MM/dd/yyyy HH:MM tt");
                                    else
                                        log.LogTime = li.Split(" ")[0];
                                }
                                else
                                {
                                    log.Message = li;
                                    response.Add(log);
                                }
                                c++;
                            }
                        }
                    }
                }
                return response;
            }
        }

        public async Task SendSRConnectionId(string connectionId)
        {
            var path = "/api/User/usertoken/loadsignalr";
            var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                string keyString = SessionObj.TokenString + "###" + connectionId;
                var sUser = new { TokenString = SessionObj.TokenString, ConnectionId = connectionId };
                HttpContent content = new StringContent(JsonConvert.SerializeObject(sUser));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var httpResponce = await client.PostAsync(uri, content);
                if (httpResponce.IsSuccessStatusCode)
                {

                }
            }
        }

        public async Task<ConfigurationEntity> GetConfigurationData()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/Claim/formprocessing/getconfigurationdata/");
                var results = await ApiGetClient.ExecuteAsync<ConfigurationEntity>(client, path);
                return results;
            }
        }

        public async Task<JObject> GetEnvironmentSetting()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/Claim/formprocessing/getenvironmentsettings/");
                var results = await ApiGetClient.ExecuteAsync<JObject>(client, path);
                return results;
            }
        }

        public async Task<ConfigurationEntity> UpdateEnvironmentSetting(string key, string value, string Userid, string oldValue)
        {
            using (var client = new HttpClient())
            {
                ConfigurationEntity configurationdata = new ConfigurationEntity()
                {
                    Key = key,
                    Value = value,
                };
                var path = "/api/Claim/formprocessing/updateenvironmentsettings/configurationdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                var configJson = JsonConvert.SerializeObject(configurationdata);

                HttpContent content = new StringContent(configJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);


                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var configudpates = UserConfigUpdate(value, Userid, oldValue);
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ConfigurationEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

    public async Task<ConfigurationEntity> UpdateConfiguration(string key, string value, string Userid, string oldValue)
        {
            using (var client = new HttpClient())
            {
                ConfigurationEntity configurationdata = new ConfigurationEntity()
                {
                    Key = key,
                    Value = value,
                };
                var path = "/api/Claim/formprocessing/updateconfigurationdata/configurationdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                var configJson = JsonConvert.SerializeObject(configurationdata);

                HttpContent content = new StringContent(configJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);


                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var configudpates = UserConfigUpdate(value, Userid, oldValue);
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ConfigurationEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }


        public async Task UserConfigUpdate(string value, string Userid, string oldValue)
        {
            using (var client = new HttpClient())
            {
                UserConfigUpdates Userconfig = new UserConfigUpdates()
                {
                    newvalue = value,
                    userid = Userid,
                    oldvalue = oldValue


                };
                var path = "/api/User/formprocessing/UserConfigUpdates";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                var configJson = JsonConvert.SerializeObject(Userconfig);

                HttpContent content = new StringContent(configJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);


                var response = await client.PostAsync(uri, content);

            }
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

        public async Task<ResultModel> ForgotPasswordRequest(string email)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/ForgotPasswordRequest/{0}", email);
                var results = await ApiGetClient.ExecuteAsync<ResultModel>(client, path);
                return results;
            }
        }

        public async Task<ResultModel> ForgotPasswordConfirm(string email, string password, string code)
        {
            using (var client = new HttpClient())
            {
                UserModel userModel = new UserModel()
                {
                    Email = email,
                    Password = HttpUtility.UrlDecode(password),
                    Code = code
                };
                var path = "/api/User/formprocessing/ForgotPasswordConfirm";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                var results = await GetUserResponse(client, userModel, uri);
                return results;
            }
        }

        public async Task<ResultModel> UserSignUp(string email, string password)
        {
            using (var client = new HttpClient())
            {
                UserEntity _userEntity = new UserEntity()
                {
                    Email = email,
                    Password = password
                };
                var path = "/api/User/formprocessing/UserSignUp";
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

        public async Task<List<UserEntityData>> GetUserListData()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/User/formprocessing/GetUsersList");

                var results = await ApiGetClient.ExecuteAsync<List<UserEntityData>>(client, path);
                return results;
            }
        }

        public async Task<bool> DisableUser(UserEntityData userEntityData)
        {
            using (var client = new HttpClient())
            {

                var path = "/api/User/formprocessing/DisableUser/userEntityData";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                UserEntityData getUserData = new UserEntityData();
                getUserData.Id = userEntityData.Id;
                getUserData.Disabled = userEntityData.Disabled;
                var userDataJson = JsonConvert.SerializeObject(getUserData);

                HttpContent content = new StringContent(userDataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<List<UserEntityData>> DisabledUserList(bool Disabled)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/User/formprocessing/GetDisabledUsersList/{0}", Disabled);
                var results = await ApiGetClient.ExecuteAsync<List<UserEntityData>>(client, path);
                return results;
            }
        }

        public async Task<List<UserPermissions>> PermissionsListData(int userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/User/formprocessing/GetPermissionsList/{0}", userId);

                var results = await ApiGetClient.ExecuteAsync<List<UserPermissions>>(client, path);
                return results;
            }
        }

        public async Task<bool> UpdateUserPermissions(UserEntityData userEntityData)
        {
            using (var client = new HttpClient())
            {

                var path = "/api/User/formprocessing/UpdateUserPermissions/userEntityData";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                UserEntityData getUserData = new UserEntityData();
                getUserData.Id = userEntityData.Id;
                getUserData.Permissions = userEntityData.Permissions;
                var userDataJson = JsonConvert.SerializeObject(getUserData);

                HttpContent content = new StringContent(userDataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<UserPermissions> GetCognitoUserById(int userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/User/formprocessing/GetCognitoUserById/{0}", userId);

                var results = await ApiGetClient.ExecuteAsync<UserPermissions>(client, path);
                return results;
            }
        }

        public async Task<ResultModel> ResetPasswordRequest(string email, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/ResetPassword/{0}/{1}", email, password);
                var results = await ApiGetClient.ExecuteAsync<ResultModel>(client, path);
                return results;
            }
        }
    
        public async Task<ResultData> CreateUserRequest(string email, string clientname, string userDefaultPermissions,string[] claimType)
        {
            using (var client = new HttpClient())
            {
                
                UserEntityData getUserData = new UserEntityData();
                getUserData.Email = email;
                getUserData.Permissions = userDefaultPermissions;
                getUserData.ClientName = clientname;
                getUserData.ClaimType = claimType;
                var userDataJson = JsonConvert.SerializeObject(getUserData);

                

                  ResultData resultData = new ResultData();
                  var path = "/api/User/formprocessing/AdminCreateUser/userEntityData/";
                  var url = string.Format("{0}{1}", this.BaseURL, path);
                  var jsonString = JsonConvert.SerializeObject(getUserData);

                 resultData = await ApiGetClient.PostAsyncApiClient<ResultData>(url, jsonString) ;

                return resultData;
            }
        }


        public async Task<UserSettings> GetUserSettingList(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/User/formprocessing/GetUserSettingsData/{0}", id);
                var results = await ApiGetClient.ExecuteAsync<UserSettings>(client, path);
                return results;
            }
        }
        public async Task<bool> PostUserSettings(UserSettings userSettings)
        {
            using (var client = new HttpClient())
            {
                var path = "/api/User/formprocessing/PostUserSettings/userSettings";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                UserSettings getUserData = new UserSettings();
                getUserData.Id = userSettings.Id;
                getUserData.DownloadFormat = userSettings.DownloadFormat;
                getUserData.Shareuploadedfiles = userSettings.Shareuploadedfiles;
                var userDataJson = JsonConvert.SerializeObject(getUserData);

                HttpContent content = new StringContent(userDataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public async Task<JObject> GetEnvironmentSettingsBeforeLogin()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/Claim/formprocessing/getenvironmentsettings/");
                var results = await ApiGetClient.ExecuteAsync<JObject>(client, path);
                return results;
            }
        }

         public async Task<ConfigurationEntity> GetConfigurationDataBeforeLogin()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/Claim/formprocessing/getconfigurationdata/");
                var results = await ApiGetClient.ExecuteAsync<ConfigurationEntity>(client, path);
                return results;
            }
        }

        public async Task<ConfigurationEntity> GetUnificationConfigurationData()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/Claim/formprocessing/GetUnificationConfigurationData");
                var results = await ApiGetClient.ExecuteAsync<ConfigurationEntity>(client, path);
                return results;
            }
        }
        public async Task<List<UserAlowedClaimtype>> GetUserClaimTypeData(int userId)
        {
            var path = string.Format("/api/User/formprocessing/getUserClaimType/{0}", userId);
            var results = await this.ClientAPIGet<List<UserAlowedClaimtype>>(path);
            return results;

        }
        public async Task<List<ClaimTypeEntity>> GetUserClaimTypeUserIdData(int userId)
        {
            var path = string.Format("/api/User/formprocessing/getUserClaimTypeUserIdData/{0}", userId);
            var results = await this.ClientAPIGet<List<ClaimTypeEntity>>(path);
            return results;

        }

        public async Task<bool> EditUserClaimType(string email, string clientname, string userDefaultPermissions, string[] claimType)
        {
            try
            {
                

                UserEntityData getUserData = new UserEntityData();
                getUserData.Email = email;
                getUserData.Permissions = userDefaultPermissions;
                getUserData.ClientName = clientname;
                getUserData.ClaimType = claimType;
                var userDataJson = JsonConvert.SerializeObject(getUserData);
                
                var path = "/api/User/formprocessing/editUserClaimType/";
                var url = string.Format("{0}{1}", this.BaseURL, path);
                var jsonString = JsonConvert.SerializeObject(getUserData);

               bool resultData = await ApiGetClient.PostAsyncApiClient<bool>(url, jsonString);
                
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

     
        
        public async Task<UserEntityData> GetCognitoUserByEmailId(string emailId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/GetCognitoUserByEmailId/{0}", emailId);
                var results = await ApiGetClient.ExecuteAsync<UserEntityData>(client, path);
                return results;
            }
        }
    }
}
