using MauiTemplateEcreo.ViewModel;

namespace MauiTemplateEcreo.Views;

public partial class AbsenceReportPage : ContentPage
{
	public AbsenceReportPage(AbsenceReportViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
    //protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    //{
    //    base.OnNavigatedFrom(args);
    //}
}