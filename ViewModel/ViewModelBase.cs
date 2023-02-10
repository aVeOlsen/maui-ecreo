using MauiTemplateEcreo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MvvmHelpers;
using MauiTemplateEcreo.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiTemplateEcreo.ViewModel
{
    public partial class BaseViewModel: ObservableObject
    {
        private UserGetModel _userGet;
        private IUserDbService _userDbService;

        public UserGetModel UserGet
        {
            get { return _userGet; }
            set
            {
                _userGet = value;
                OnPropertyChanged("UserGet");
            }
        }

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotBusy))]
        private bool isBusy;
        public bool IsNotBusy => !IsBusy;

        public BaseViewModel()
        {
            UserGet = new UserGetModel();
        }
    }
}
