using MauiTemplateEcreo.ViewModel;

namespace MauiTemplateEcreo.Views;

public partial class AbsenceStatusPage : ContentPage
{
	public AbsenceStatusPage(AbsenceStatusViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		var viewModel = (AbsenceStatusViewModel)BindingContext;
		await viewModel.Refresh();
    }
}