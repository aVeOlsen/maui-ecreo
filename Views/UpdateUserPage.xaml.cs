using MauiTemplateEcreo.Model;
using MauiTemplateEcreo.ViewModel;

namespace MauiTemplateEcreo.Views;


public partial class UpdateUserPage : ContentPage
{

    public UpdateUserPage(UpdateUserViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}