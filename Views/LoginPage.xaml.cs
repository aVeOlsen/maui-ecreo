using MauiTemplateEcreo.Helper;
using MauiTemplateEcreo.ViewModel;

namespace MauiTemplateEcreo.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}