namespace MyReference.View;

public partial class EditDbzCharactersPage : ContentPage
{
	public EditDbzCharactersPage(EditDbzCharactersViewModel editDbzCharactersViewModel)
	{
		InitializeComponent();
		BindingContext = editDbzCharactersViewModel;
	}
}