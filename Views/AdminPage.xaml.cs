using MauiTemplateEcreo.ViewModel;

namespace MauiTemplateEcreo.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		var viewModel = (AdminViewModel)BindingContext;
		await viewModel.Refresh();
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        //var viewModel = (AdminViewModel)BindingContext;
        //args.LoadFromXaml(viewModel.UserModel);
        //var userId = viewModel.UserModel.BaseID;
        base.OnNavigatedFrom(args);
    }
}