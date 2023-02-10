using MauiEcreoLib;
using MauiTemplateEcreo.Model;
using MonkeyCache.FileStore;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTemplateEcreo.Services
{
    public class UserDbService : IUserDbService
    {
        static HttpClient _client;
        //static JsonSerializerOptions serializerOptions;
        private ObservableRangeCollection<User> users { get; set; }
        private ObservableRangeCollection<UserGetModel> usersGet { get; set; }
        private ObservableRangeCollection<ContactInformation> contactInfo { get; set; }
        public UserDbService()
        {
            //HttpClientHandler insecureHandler = GetInsecureHandler();
            //_client = new HttpClient(insecureHandler);
            //serializerOptions = new JsonSerializerOptions
            //{
            //    //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    //WriteIndented = true,
            //};
            _client = new HttpClient();
        }
        public static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public async Task AddUser(string firstname, string lastname, bool admin, Role role, string email, string phone, string address, string password)
        {
            Uri uri = new Uri(String.Format(Constants.UserRestUrl + "Create/", string.Empty));

            //await Init();
            string image = null; //"https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png";
            var contact = new ContactInformation
            {
                Email = email,
                Phone = phone,
                Address = address
            };
            var user = new User
            {
                Firstname = firstname,
                Lastname = lastname,
                AdminStatus = admin,
                Image = image,
                Role = role,
                PersonalInformation = contact,
                Password = password
            };
            //var json = JsonSerializer.Serialize(user, serializerOptions);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            //await db.InsertAsync(user);
        }


        //public async Task<ObservableRangeCollection<UserGetModel>> GetUsersAsync()
        //{
        //    Uri uri = new Uri(String.Format(Constants.UserRestUrl, string.Empty));
        //    HttpResponseMessage response = await _client.GetAsync(uri);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        //usersGet = JsonSerializer.Deserialize<ObservableRangeCollection<UserGetModel>>(content, serializerOptions);
        //        usersGet = JsonConvert.DeserializeObject<ObservableRangeCollection<UserGetModel>>(content);

        //        return usersGet;
        //    }
        //    else
        //        Debug.WriteLine(response);
        //    return null;

        //}

        Uri uri = new Uri(String.Format(Constants.UserRestUrl, string.Empty));
        public Task<IEnumerable<UserGetModel>> GetUsersAsync() =>
            GetAsync<IEnumerable<UserGetModel>>(uri, "getUsers");

        async Task<T> GetAsync<T>(Uri url, string key, int mins = 1, bool forceRefresh = true)
        {
            var content = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                content = Barrel.Current.Get<string>(key);
            else if (!forceRefresh && !Barrel.Current.IsExpired(key))
                content = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    HttpResponseMessage response = await _client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        Barrel.Current.Add(key, content, TimeSpan.FromMinutes(mins));
                        //var user = JsonConvert.DeserializeObject<T>(content);
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                    //content = await _client.GetStringAsync(url);
                }
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }

        public async Task<User> GetItemAsync(string id)
        {
            Uri uri = new Uri(String.Format(Constants.UserRestUrl, id));
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);
            //var user = JsonSerializer.Deserialize<User>(content, serializerOptions);
            return user;
            //return db.Table<User>().Where(c => c.BaseID == id).FirstOrDefaultAsync();
        }
        public async Task<UserGetModel> GetUserModelAsync(string id)
        {
            Uri uri = new Uri(String.Format(Constants.UserRestUrl, id));
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserGetModel>(content);
            //var user = JsonSerializer.Deserialize<User>(content, serializerOptions);
            return user;
            //return db.Table<User>().Where(c => c.BaseID == id).FirstOrDefaultAsync();
        }
        public async Task<UserModel> GetUserAsync(string id)
        {
            Uri uri = new Uri(String.Format(Constants.UserRestUrl, id));
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserModel>(content);
            //var user = JsonSerializer.Deserialize<User>(content, serializerOptions);
            return user;


        }

        public async Task<User> UpdateUserAsync(User user)
        {
            //await Init();
            Uri uri = new Uri(String.Format(Constants.UserRestUrl, user.BaseID));
            //var json = JsonSerializer.Serialize<User>(user, serializerOptions);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            return user;
            //await db.UpdateAsync(user);
            //var User = await db.Table<User>().Where(c => c.BaseID == user.BaseID).FirstOrDefaultAsync();
            //return User;
        }
        public async Task<UserGetModel> UpdateUserModelAsync(UserGetModel user)
        {
            //await Init();
            Uri uri = new Uri(String.Format(Constants.UserRestUrl, user.BaseID));
            //var json = JsonSerializer.Serialize<User>(user, serializerOptions);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            return user;
            //await db.UpdateAsync(user);
            //var User = await db.Table<User>().Where(c => c.BaseID == user.BaseID).FirstOrDefaultAsync();
            //return User;
        }
        public async Task<UserGetModel> UpdateUserAbsenceAsync(UserGetModel user)
        {
            //await Init();
            //int enumID = ((int)user.CurrentAbsenceStatus);
            Uri uri = new Uri(String.Format(Constants.UserRestUrl, user.BaseID + "/" + ((int)user.CurrentAbsenceStatus)));
            //var json = JsonSerializer.Serialize<User>(user, serializerOptions);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            return user;
            //await db.UpdateAsync(user);
            //var User = await db.Table<User>().Where(c => c.BaseID == user.BaseID).FirstOrDefaultAsync();
            //return User;
        }

        public async Task<UserModel> UpdateUserModelAsync(UserModel user)
        {
            //await Init();

            //int enumID = ((int)user.CurrentAbsenceStatus);
            Uri uri = new Uri(String.Format(Constants.UserRestUrl, user.BaseID));
            //var json = JsonSerializer.Serialize<User>(user, serializerOptions);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            return user;
            //await db.UpdateAsync(user);
            //var User = await db.Table<User>().Where(c => c.BaseID == user.BaseID).FirstOrDefaultAsync();
            //return User;
        }


        public async Task RemoveUser(string id)
        {
            Uri uri = new Uri(string.Format(Constants.UserRestUrl, id));
            var response = await _client.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }

        }

    }
}
