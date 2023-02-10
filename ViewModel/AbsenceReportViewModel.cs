using MauiEcreoLib;
using MauiTemplateEcreo.Helper;
using MauiTemplateEcreo.Model;
using MauiTemplateEcreo.Services;
using MauiTemplateEcreo.Views;
//using MvvmHelpers.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiTemplateEcreo.ViewModel
{
    public partial class AbsenceReportViewModel: BaseViewModel
    {
        string reason;
        public string Reason { get => reason; set => SetProperty(ref reason, value); }
        string firstname;
        public string Firstname { get => UserGet.Firstname; }
        readonly IUserDbService _userDbService;
        public AbsenceStatusRole absenceReport { get; set; }
        //public ICommand AbsenceReportCommand { get; set; }
        public string[] AllRoles { get; } = Enum.GetNames(typeof(AbsenceStatusRole));

        private AbsenceStatusRole selectedRole = AbsenceStatusRole.OnSite;
        public AbsenceStatusRole SelectedRole
        {
            get => selectedRole;
            set
            {
                SetProperty(ref selectedRole, value);
            }
        }

        public AbsenceReportViewModel()
        {
            //AbsenceReportCmd = new AsyncCommand(UpdateCommand);
            _userDbService = ServiceHelper.GetService<UserDbService>();// new UserDbService();
            UserGet = new UserGetModel();
        }
        //[ICommand]
        //Task Back() =>
        //    Shell.Current.GoToAsync($"//{nameof(OfficeStatusPage)}");


        [ICommand]
        async Task AbsenceReport()
        {
            UserGet.CurrentAbsenceStatus = SelectedRole;
            await _userDbService.UpdateUserAbsenceAsync(UserGet);
            //await Shell.Current.GoToAsync($"..?BaseID={User.BaseID}");
            if (UserGet.CurrentAbsenceStatus == AbsenceStatusRole.OnSite || UserGet.CurrentAbsenceStatus == AbsenceStatusRole.Late)
            {
                await Shell.Current.GoToAsync($"//{nameof(OfficeStatusPage)}");

            }
            else if (UserGet.CurrentAbsenceStatus == AbsenceStatusRole.Home)
            {
                //await Shell.Current.GoToAsync($"//{nameof(HomeStatusPage)}?BaseID={UserGet.BaseID}");
                await Shell.Current.GoToAsync($"//{nameof(HomeStatusPage)}");
            }


        }



    }
}
