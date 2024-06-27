namespace DanielaMoraAPINASA.Views;
using DanielaMoraAPINASA.ViewModels;


public partial class ApodPageDM : ContentPage
{
	public ApodPageDM()
	{
		InitializeComponent();
        BindingContext = new DMApodViewModel();

    }
}