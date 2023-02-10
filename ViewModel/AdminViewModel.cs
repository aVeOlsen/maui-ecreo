using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using MauiEcreoLib;
using MauiTemplateEcreo.Helper;
using MauiTemplateEcreo.Model;
using MauiTemplateEcreo.Services;
using MauiTemplateEcreo.Views;
using MvvmHelpers;

namespace MauiTemplateEcreo.ViewModel
{
    public partial class AdminViewModel : BaseViewModel
    {
        public ObservableRangeCollection<UserGetModel> UsersGet { get; set; }
        //public AsyncCommand RefreshCommand { get; set; }
        //public AsyncCommand AddCommand { get; set; }
        //public AsyncCommand<UserGetModel> RemoveCommand { get; set; }
        //public AsyncCommand<User> UpdateCommand { get; set; }
        IAdminstratorService _adminstratorService;
        IUserDbService _userDbService;
        IImageDbService _imageDbService;
        public string[] AllRoles { get; } = Enum.GetNames(typeof(Role));

        private Role selectedRole = Role.RegularEmployee;
        public Role SelectedRole
        {
            get => selectedRole;
            set
            {
                SetProperty(ref selectedRole, value);
            }
        }
        public User _user { get; set; }
        public UserModel UserModel { get; set; }
        public AdminViewModel()
        {
            UsersGet = new ObservableRangeCollection<UserGetModel>();
            _userDbService = ServiceHelper.GetService<UserDbService>();
            _adminstratorService = ServiceHelper.GetService<AdminstratorService>();
            
            _imageDbService = ServiceHelper.GetService<ImageDbService>();
            //RefreshCommand = new AsyncCommand(Refresh);
            //AddCommand = new AsyncCommand(Add);
            //RemoveCommand = new AsyncCommand<UserGetModel>(Remove);
            //UpdateCommand = new AsyncCommand<User>(Update);
            _user = new User();
        }


        //async Task Add()
        //{
        //    var route = $"{nameof(AddUserPage)}?BaseID={UserGet.BaseID}";
        //    await Shell.Current.GoToAsync(route);
        //}
        [ICommand]
        async void Update(UserGetModel userGet)
        {
            UserModel = await _userDbService.GetUserAsync(userGet.BaseID);
            UserModel.PersonalInformation = await _adminstratorService.GetUserInfoAsync(UserModel.BaseID);
            //user = userConvert;
            await Shell.Current.GoToAsync($"{nameof(UpdateUserPage)}?user={UserModel}",
            new Dictionary<string, object>
            {
                ["UserModel"] = UserModel
            });
            ////await _userDbService.UpdateUserAsync(user);
            //await Refresh();
        }
        [ICommand]
        public async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(600);
            var user = await _userDbService.GetUsersAsync();
            UsersGet.Clear();
            foreach (var item in user)
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
            IsBusy = false;
        }
        [ICommand]
        async Task Remove(UserGetModel user)
        {
            await _userDbService.RemoveUser(user.BaseID);
            await Refresh();
        }
    }
}
