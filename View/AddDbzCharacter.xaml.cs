namespace MyReference.View;

public partial class AddDbzCharacter : ContentPage
{
	public AddDbzCharacter(AddDbzCharacterViewModel addDbzCharacterViewModel)
	{
		InitializeComponent();
		BindingContext = addDbzCharacterViewModel;

	}
}