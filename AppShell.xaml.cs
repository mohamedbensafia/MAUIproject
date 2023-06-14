namespace MyReference;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
        Routing.RegisterRoute(nameof(AddDbzCharacter), typeof(AddDbzCharacter));
        Routing.RegisterRoute(nameof(SignIn), typeof(SignIn));
        Routing.RegisterRoute(nameof(Login), typeof(Login));
        Routing.RegisterRoute(nameof(UsersList), typeof(UsersList));
        Routing.RegisterRoute(nameof(ShowDbzCharactersPage), typeof(ShowDbzCharactersPage));
        Routing.RegisterRoute(nameof(EditDbzCharactersPage), typeof(EditDbzCharactersPage));
        Routing.RegisterRoute(nameof(ShowUsers), typeof(ShowUsers));

    }
}
