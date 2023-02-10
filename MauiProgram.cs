using CommunityToolkit.Maui;
using MauiTemplateEcreo.Services;
using MauiTemplateEcreo.ViewModel;
using MauiTemplateEcreo.Views;

namespace MauiTemplateEcreo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>().UseMauiCommunityToolkit()
            
            //.ConfigureEssentials(e =>
            //{
            //e.UseVersionTracking();
            //})
//            .ConfigureMauiHandlers(e =>
//            {
//#if WINDOWS //e.AddWindows(w=> {....});  Compiler directives.
//            })
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddTransient<UserDbService>();
		builder.Services.AddTransient<AdminstratorService>();
		builder.Services.AddTransient<ImageDbService>();
		builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<OfficeStatusPage>();
        builder.Services.AddTransient<OfficeStatusViewModel>();
        builder.Services.AddTransient<HomeStatusPage>();
        builder.Services.AddTransient<HomeStatusViewModel>();
        builder.Services.AddTransient<AbsenceStatusPage>();
        builder.Services.AddTransient<AbsenceStatusViewModel>();
        builder.Services.AddTransient<AbsenceReportPage>();
		builder.Services.AddTransient<AbsenceReportViewModel>();
        builder.Services.AddTransient<AdminPage>();
        builder.Services.AddTransient<AdminViewModel>();
        builder.Services.AddTransient<UpdateUserPage>();
        builder.Services.AddTransient<UpdateUserViewModel>();
        builder.Services.AddTransient<SettingsPage>();
        //Med disse tilføjet, kan jeg så bruge constructor injections. 
        return builder.Build();
	}
}
