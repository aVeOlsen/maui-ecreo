using MauiTemplateEcreo.ViewModel;


namespace MauiTemplateEcreo.Views;

public partial class OfficeStatusPage : ContentPage
{
    //Virker i videoen, med at saette BindingContext = viewModel     i constructor, men dette fungere ikke her
    private OfficeStatusViewModel viewModel => BindingContext as OfficeStatusViewModel;
    public OfficeStatusPage(OfficeStatusViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vModel = (OfficeStatusViewModel)BindingContext;
        await vModel.RefreshCommand.ExecuteAsync();// vm.Refresh();
    }

    //private void OnCounterClicked(object sender, EventArgs e)
    //{
    //	count++;

    //	if (count == 1)
    //		CounterBtn.Text = $"Clicked {count} time";
    //	else
    //		CounterBtn.Text = $"Clicked {count} times";

    //	SemanticScreenReader.Announce(CounterBtn.Text);
    //}
}

