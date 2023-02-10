using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEcreoLib;
using MauiTemplateEcreo.Helper;
using MauiTemplateEcreo.Model;
using MauiTemplateEcreo.Services;
using MauiTemplateEcreo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MauiTemplateEcreo.ViewModel
{
    [QueryProperty(nameof(UserModel), "UserModel")]
    public partial class UpdateUserViewModel : BaseViewModel, IQueryAttributable
    {

        IUserDbService _userDbService;
        IImageDbService _imageDbService;
        IAdminstratorService _adminstratorService;
        public string[] AllRoles { get; } = Enum.GetNames(typeof(Role));

        private Role selectedRole;
        public Role SelectedRole
        {
            get => selectedRole;
            set
            {
                SetProperty(ref selectedRole, value);
            }
        }


        [ObservableProperty]
        public UserModel userModel;

        public UserModel User
        {
            get
            {
                return userModel;
            }
            set
            {
                SetProperty(ref userModel, value);
            }
        }
        private ImageSource imageUrl;
        public ImageSource ImageURL
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                OnPropertyChanged("ImageURL");
            }
        }

        public UpdateUserViewModel()
        {
            //UsersGet = new ObservableRangeCollection<UserGetModel>();
            _userDbService = ServiceHelper.GetService<UserDbService>();
            _imageDbService = ServiceHelper.GetService<ImageDbService>();
            _adminstratorService = ServiceHelper.GetService<AdminstratorService>();
            //User = _userDbService.GetUserAsync(UserModel.BaseID);
        }
        //[ICommand]
        public async Task LoadPhotoAsync()//Previous image from main
        {
            //User = await _userDbService.GetUserAsync(user.BaseID);
            //UserGet = await _userDbService.GetUserModelAsync(tmp.BaseID);
            IsBusy = true;
            if (User.Image == null)
            {
                User.Image = null;
                IsBusy = false;
                return;
            }
            var stream = await _imageDbService.GetImage(User.Image);
            User.ImageURL = ImageSource.FromStream(() =>
            {
                IsBusy = false;
                return new MemoryStream(stream);
            });
            IsBusy = false;
        }
        public async Task LoadImageAsync(FileResult photo)//Taken image
        {
            if (photo==null)
            {
                User.Image = null;
                IsBusy = false;
                return;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            var stream = await _imageDbService.GetImage(photo.FileName);
            IsBusy = false;
            User.ImageURL = ImageSource.FromStream(() =>
            {
                return new MemoryStream(stream);
            });
            if (DeviceInfo.Idiom == DeviceIdiom.Phone || DeviceInfo.Idiom ==DeviceIdiom.Tablet)
            {
                var result = await App.Current.MainPage.DisplayActionSheet("Gem billede?", "No", "Yes");
                if (result == "Yes")
                {
                    User.Image = photo.FileName;
                    await _userDbService.UpdateUserModelAsync(User);
                    IsBusy=false;
                }
            }
            else if (DeviceInfo.Idiom==DeviceIdiom.Desktop)
            {
                User.Image = photo.FileName;
                await _userDbService.UpdateUserModelAsync(User);
                IsBusy=false;
            }
            else
                IsBusy=false;
                return;



        }

        [ICommand]
        async Task Update()
        {
            User.ImageURL=null;
            User = await _userDbService.UpdateUserModelAsync(User);
            await Shell.Current.GoToAsync($"///{nameof(AdminPage)}");
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Values.Count != 0)
            {
                User = query["UserModel"] as UserModel;
                //OnPropertyChanged("User");
                SelectedRole = User.Role;
                await LoadPhotoAsync();

            }
        }
        [ICommand]
        public async Task TakePhotoAsync()
        {
            var file = await MediaPicker.CapturePhotoAsync();

            if (file == null)
                return;

            await _imageDbService.UploadImageAsync(await file.OpenReadAsync(), file.FileName);
            await LoadImageAsync(file);

        }

        [ICommand]
        public async Task UploadPhotoAsync()
        {
            var file = await MediaPicker.PickPhotoAsync();

            if (file == null)
                return;

            await _imageDbService.UploadImageAsync(await file.OpenReadAsync(), file.FileName);
            await LoadImageAsync(file);

        }





        //public void ApplyQueryAttributes(IDictionary<string, object> query)
        //{
        //    if(query.Values.Count != 0)
        //    {
        //        var name = HttpUtility.UrlDecode(query[user]);
        //    }
        //}
    }
}
