using MauiEcreoLib;
using MauiTemplateEcreo.Helper;
using MauiTemplateEcreo.Model;
using MauiTemplateEcreo.Services;
using MauiTemplateEcreo.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTemplateEcreo.ViewModel
{
    public partial class HomeStatusViewModel: BaseViewModel
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

        //SettingsViewModel svm;
        IUserDbService _userDbService;
        //AdminstratorService _adminstratorService;
        IImageDbService _imageDbService;
        public HomeStatusViewModel()
        {

            //_adminstratorService = new AdminstratorService();
            //_userDbService = DependencyService.Get<UserDbService>();//    new UserDbService();
            Users = new ObservableRangeCollection<User>();
            UsersGet = new ObservableRangeCollection<UserGetModel>();
            _userDbService = ServiceHelper.GetService<UserDbService>();
            _imageDbService = ServiceHelper.GetService<ImageDbService>();

            OpenAbsenceCmd = new AsyncCommand(OnAbsenceClicked);
            RefreshCommand = new AsyncCommand(Refresh);
            user = new User();
            //svm = new SettingsViewModel();
        }

        public async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(300);
            UsersGet.Clear();
            var users = await _userDbService.GetUsersAsync();
            //for(int i = 0; i < users.Count; i++)
            foreach (var item in users)
            {
                if (item.CurrentAbsenceStatus == AbsenceStatusRole.Home)
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
            }
            IsBusy = false;
        }

        public async Task OnAbsenceClicked()
        {
            if (this.user == null)
            {
                await Shell.Current.GoToAsync($"..");
            }
            else
            {
                //await Shell.Current.GoToAsync($"//HomeStatusPage/{nameof(AbsenceReportPage)}?BaseID={UserGet.BaseID}");
                await Shell.Current.GoToAsync($"//HomeStatusPage/{nameof(AbsenceReportPage)}");
            }

        }


    }
}
