namespace MyReference.View;

public partial class ShowDbzCharactersPage : ContentPage
{
	public ShowDbzCharactersPage(ShowDbzCharactersPageViewModel showDbzCharactersPageViewModel)
	{
		InitializeComponent();
		BindingContext = showDbzCharactersPageViewModel;
	}
}