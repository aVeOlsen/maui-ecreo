using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEcreoLib;
using MauiTemplateEcreo.Helper;
using MauiTemplateEcreo.Model;
using MauiTemplateEcreo.Services;
using MauiTemplateEcreo.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MauiTemplateEcreo.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _config;
        //private readonly ILogger<AuthenticationService> _logger;
        string username, password;
        public string UserName { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        //public AsyncCommand LoginCommand { get; set; }
        public User user { get; set; }
        public UserGetModel UserGet { get; set; }
        //public IValidationRule<string> UserNameValidation { get; set; } = new RequiredValidationRule();


        IUserDbService _userDbService;
        IAuthenticationService _authenticationService;
        IGeolocation geolocation;

        public LoginViewModel(IGeolocation geolocation)
        {
            _userDbService = ServiceHelper.GetService<UserDbService>(); //new UserDbService();
            _authenticationService = new AuthenticationService(_factory, _config);
            //LoginCommand = new AsyncCommand(Login);
            user = new User();
            user.PersonalInformation = new ContactInformation();
            this.geolocation = geolocation;
        }


        [ICommand]
        public async Task Login()
        {
            IsBusy = true;
            if (UserName != null && Password != null)
            {
                var result = await _authenticationService.SignIn(UserName, Password);

                if (result == null)
                {
                    await Application.Current.MainPage.DisplayAlert("FORKERT!", "Indtastede brugernavn eller adgangskode var forkert", "ok");
                    IsBusy = false;
                    return;
                }

                if (result.Identity.IsAuthenticated == true)
                {
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                    {

                        var getLastLocation = await geolocation.GetLastKnownLocationAsync();
                        var setNewLocation = await geolocation.GetLocationAsync(new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Default,
                            Timeout = TimeSpan.FromSeconds(30)
                        });
                        if (getLastLocation == null)
                        {
                            getLastLocation = setNewLocation;
                            Preferences.Set("DateKey", DateTime.Today, "SecurityDateID");
                        }
                        if (getLastLocation != null && setNewLocation != null)
                        {
                            var gpsSecurity = Location.CalculateDistance(getLastLocation, setNewLocation, DistanceUnits.Kilometers);
                            if (gpsSecurity != 0)
                            {
                                var securityTime = TimeSpan.FromDays(2);
                                var dateData = JsonConvert.DeserializeObject<DateTime>(Preferences.Get("DateKey", "default_value", "SecurityDateID"));
                                var today = DateTime.Today;
                                if (dateData + securityTime >= today && gpsSecurity > 1000)
                                {
                                    await Application.Current.MainPage.DisplayAlert("GPS authorizing failed", "An unexpected threat, against our security accoured. Contact Admin to get access.", "ok");
                                    IsBusy = false;
                                    return;
                                }
                                else
                                {
                                    Preferences.Set("DateKey", JsonConvert.SerializeObject(DateTime.Today), "SecurityDateID");
                                }
                            }

                        }
                        if (getLastLocation != null)
                        {
                            Preferences.Set("DateKey", JsonConvert.SerializeObject(DateTime.Today), "SecurityDateID");
                        }
                        else
                        {

                            await Application.Current.MainPage.DisplayAlert("GPS authorizing failed", "Af sikkerhedsmæssige grunde, beder vi dig aktivere GPS'en, så vi bedre kan sikre os mod angreb", "ok");
                        }
                    }
                    //if(location.)

                    var tmp = result.Identities.ToList();
                    var tmp2 = tmp[0].Claims.ElementAtOrDefault(1);
                    user.BaseID = tmp2.Value;
                    user = await _userDbService.GetItemAsync(user.BaseID);

                    Preferences.Set("UserKey", JsonConvert.SerializeObject(user), "LoginID");

                    //Application.Current.Properties["id"] = user.BaseID;
                    //Microsoft.Maui.Stoage("id")// = user.BaseID;
                    //.Current.Properties["id"] = user.BaseID;
                    //UserGet 
                    //result.Claims.GetEnumerator().MoveNext();
                    /*UserGet.Claim*/
                    IsBusy = false;
                    UserName = null;
                    Password = null;
                    var route = $"///OfficeStatus"; //?BaseID={user.BaseID}";
                    await Shell.Current.GoToAsync(route);
                }
                else
                    await Application.Current.MainPage.DisplayAlert("FORKERT!", "Indtastede brugernavn eller adgangskode var forkert", "ok");
                IsBusy = false;
                return;
            }
            else
                await Application.Current.MainPage.DisplayAlert("FORKERT!", "Indtastede brugernavn eller adgangskode var forkert", "ok");
            IsBusy = false;
            return;
        }


        //[ICommand]
        public async Task Submit()
        {
            var route = $"///OfficeStatus";
            await Shell.Current.GoToAsync(route);

        }

    }
}
