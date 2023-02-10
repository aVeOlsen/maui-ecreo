using MauiEcreoLib;
using MauiTemplateEcreo.Helper;
using MauiTemplateEcreo.Model;
using MauiTemplateEcreo.Services;
using MauiTemplateEcreo.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm;

namespace MauiTemplateEcreo.ViewModel
{
    public partial class OfficeStatusViewModel : BaseViewModel
    {
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand OpenAbsenceCmd { get; set; }
        public ObservableRangeCollection<User> Users { get; set; }
        public ObservableRangeCollection<UserGetModel> UsersGet { get; set; }
        public User user { get; set; }
        private ImageSource imageDataUrl;
        public ImageSource ImageDataUrl
        {
            get { return imageDataUrl; }
            set
            {
                imageDataUrl = value;
                OnPropertyChanged("ImageDataUrl");
            }
        }

        IUserDbService _userDbService;
        IImageDbService _imageDbService;
        public OfficeStatusViewModel()
        {
            _imageDbService = ServiceHelper.GetService<ImageDbService>();
            _userDbService = ServiceHelper.GetService<UserDbService>();



            Users = new ObservableRangeCollection<User>();
            UsersGet = new ObservableRangeCollection<UserGetModel>();
            OpenAbsenceCmd = new AsyncCommand(OnAbsenceClicked);
            RefreshCommand = new AsyncCommand(Refresh);
            user = new User();
            //svm = new SettingsViewModel();
        }
        //[ICommand]
        public async Task Refresh()
        {
            if (IsBusy)
                return;
            try
            {

                IsBusy = true;
                await Task.Delay(300);
                UsersGet.Clear();
                var users = await _userDbService.GetUsersAsync();
                foreach (var item in users)
                {
                    if (item.CurrentAbsenceStatus == AbsenceStatusRole.OnSite)
                    {

                        if (item.Image != null && item.Image.Contains("jpg")||item.Image.Contains("JPG")||item.Image.Contains("png"))
                        {

                            var stream = await _imageDbService.GetImage(item.Image);
                            item.ImageURL = ImageSource.FromStream(() =>
                            {
                                return new MemoryStream(stream);
                            });


                        }
                        UsersGet.Add(item);
                    }
                //IsNotBusy = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", "Unable to get users, pls check your network connection", "Ok");
            }
            finally
            {
                IsBusy = false;
                //IsNotBusy = true;

            }
        }

        public async Task OnAbsenceClicked()
        {
            if (this.user == null)
            {
                await Shell.Current.GoToAsync($"..");
            }
            else
            {
                await Shell.Current.GoToAsync($"//OfficeStatus/{nameof(AbsenceReportPage)}");
                //await Shell.Current.GoToAsync($"//{nameof(AbsenceReportPage)}");
            }

        }

    }
}
