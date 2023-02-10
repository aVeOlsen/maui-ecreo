using MauiEcreoLib;
using MauiTemplateEcreo.Helper;
using MauiTemplateEcreo.Model;
using MauiTemplateEcreo.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTemplateEcreo.ViewModel
{
    public partial class SettingsViewModel: BaseViewModel
    {

        //public AsyncCommand<FileResult> LoadPhotoCmd { get; set; }
        //public AsyncCommand UploadPhotoCmd { get; set; }


        private ImageSource photoPath;
        public ImageSource PhotoPath
        {
            get { return photoPath; }
            set
            {
                photoPath = value;
                OnPropertyChanged("PhotoPath");
            }
        }

        HttpClient _client;
        UserModel user;
        IUserDbService _userDbService;
        IImageDbService _imageDbService;
        IAdminstratorService _adminstratorService;
        public SettingsViewModel()
        {

            //_adminstratorService = DependencyService.Get<IAdminstratorService>();
            //_imageDbService = DependencyService.Get<IImageDbService>();
            _adminstratorService = ServiceHelper.GetService<AdminstratorService>();
            _imageDbService = ServiceHelper.GetService<ImageDbService>();

            user = new UserModel();
            _client = new HttpClient();
            _userDbService = DependencyService.Get<IUserDbService>();
            UserGet = new UserGetModel();
            //UploadPhotoCmd = new AsyncCommand(UploadPhotoAsync);
            //LoadPhotoCmd = new AsyncCommand<FileResult>(LoadPhotoAsync);
        }
        public async Task<ImageSource> LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                UserGet.Image = null;
                return null;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            UserGet.Image = photo.FileName;

            user = await _userDbService.GetUserAsync(UserGet.BaseID);
            user.Image = UserGet.Image;

            user.PersonalInformation = await _adminstratorService.GetUserInfoAsync(UserGet.BaseID);

            await _userDbService.UpdateUserModelAsync(user);

            PhotoPath = newFile;
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
            {
                await stream.CopyToAsync(newStream);
            }

            return PhotoPath;


        }

        public async Task UploadPhotoAsync(/*object sender, EventArgs e*/)
        {
            var file = await MediaPicker.PickPhotoAsync();

            if (file == null)
                return;

            await _imageDbService.UploadImageAsync(await file.OpenReadAsync(), file.FileName);
            await LoadPhotoAsync(file);


        }
        //
    }
}
