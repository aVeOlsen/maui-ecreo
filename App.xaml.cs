using MauiTemplateEcreo.Helper;
using MonkeyCache.FileStore;

namespace MauiTemplateEcreo;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        Settings.SetTheme();
        //Giver applikationen et id, som giver en folder til at putte data i.
        //Alt ryger i barrel som er en slags grundlag for at putte data i barrel med forskellige keys
        Barrel.ApplicationId = AppInfo.PackageName;
		MainPage = new AppShell();
	}

    

    protected override void OnStart()
    {
        base.OnStart();
        OnResume();
    }

    protected override void OnResume()
    {
        base.OnResume();
        Settings.SetTheme();
        RequestedThemeChanged += App_RequestedThemeChanged;
    }


    protected override void OnSleep()
    {
        base.OnSleep();
        Settings.SetTheme();
        RequestedThemeChanged -= App_RequestedThemeChanged;
    }
    private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Settings.SetTheme();
        });
    }

}
