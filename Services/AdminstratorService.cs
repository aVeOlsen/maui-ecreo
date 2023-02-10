using MauiEcreoLib;
using MauiTemplateEcreo.Model;
using MauiTemplateEcreo.ViewModel;
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
    public class AdminstratorService : IAdminstratorService
    {
        static HttpClient _client;
        //static JsonSerializerOptions serializerOptions;
        private ObservableRangeCollection<User> Users { get; set; }
        private ObservableRangeCollection<UserGetModel> UsersGet { get; set; }
        private ObservableRangeCollection<ContactInformation> ContactInfo { get; set; }
        public AdminstratorService()
        {
            _client = new HttpClient();
        }
        public async Task<UserPersonalInfoModel> GetUserInfoAsync(string id)
        {
            Uri uri = new Uri(String.Format(Constants.AdminstratorGetUserInfoRestUrl, id));
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            var content = await response.Content.ReadAsStringAsync();
            var contactInfo = JsonConvert.DeserializeObject<UserPersonalInfoModel>(content);
            return contactInfo;
        }
        public async Task<UserPersonalInfoModel> GetUserPasswordAsync(string id)
        {
            Uri uri = new Uri(String.Format(Constants.AdminstratorGetUserPasswordRestUrl, id));
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            var content = await response.Content.ReadAsStringAsync();
            var passwordInfo = JsonConvert.DeserializeObject<UserPersonalInfoModel>(content);
            return passwordInfo;
        }
        public async Task<UserGetModel> GetUserRoleAsync(string id)
        {
            Uri uri = new Uri(String.Format(Constants.AdminstratorGetUserInfoRestUrl, id));
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            var content = await response.Content.ReadAsStringAsync();
            var roleInfo = JsonConvert.DeserializeObject<UserGetModel>(content);
            return roleInfo;
        }
        public async Task<Role> UpdateRoleAsync(Role role, string id)
        {
            Uri uri = new Uri(String.Format(Constants.AdminstratorUpdateRoleRestUrl, id));
            var json = JsonConvert.SerializeObject((int)role);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong. UPDATE ROLE..");
            }
            return role;
        }



    }
}
