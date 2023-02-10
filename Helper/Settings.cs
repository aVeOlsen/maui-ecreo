using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTemplateEcreo.Helper
{
    public static class Settings
    {
        const int theme = 0;

        public static int Theme 
        { 
            get=> Preferences.Get(nameof(Theme), theme); 
            set=> Preferences.Set(nameof(Theme), value); 
        }
        public static void SetTheme()
        {
            switch (Theme)
            {
                case 0:
                    App.Current.UserAppTheme = AppTheme.Unspecified;
                    break;
                case 1:
                    App.Current.UserAppTheme = AppTheme.Light;
                    break;
                case 2:
                    App.Current.UserAppTheme = AppTheme.Dark;
                    break;
            }
        }
    }
}
