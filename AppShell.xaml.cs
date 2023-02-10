using MauiTemplateEcreo.Views;

namespace MauiTemplateEcreo;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(AbsenceReportPage), typeof(AbsenceReportPage));

        Routing.RegisterRoute(nameof(OfficeStatusPage), typeof(OfficeStatusPage));

        Routing.RegisterRoute(nameof(HomeStatusPage), typeof(HomeStatusPage));

        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
        Routing.RegisterRoute($"{nameof(AdminPage)}/{nameof(UpdateUserPage)}", typeof(UpdateUserPage));
        BindingContext = this;
    }

    private void SignOut_Clicked(object sender, EventArgs e)
    {
        Preferences.Clear();
        Shell.Current.GoToAsync("//LoginPage");
    }
}
