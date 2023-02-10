using MauiTemplateEcreo.ViewModel;

namespace MauiTemplateEcreo.Views;

public partial class HomeStatusPage : ContentPage
{
	public HomeStatusPage(HomeStatusViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = (HomeStatusViewModel)BindingContext;
        await vm.RefreshCommand.ExecuteAsync();// vm.Refresh();
    }
}