using MyReference.View;
using MyReference.ViewModel;

namespace MyReference;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<DetailViewModel>();
        builder.Services.AddTransient<DetailPage>();

        builder.Services.AddTransient<AddDbzCharacterViewModel>();
        builder.Services.AddTransient<AddDbzCharacter>();

        builder.Services.AddTransient<SignInViewModel>();
        builder.Services.AddTransient<SignIn>();

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<Login>();

        builder.Services.AddTransient<UsersListViewModel>();
        builder.Services.AddTransient<UsersList>();

        builder.Services.AddTransient<ShowDbzCharactersPageViewModel>();
        builder.Services.AddTransient<ShowDbzCharactersPage>();

        builder.Services.AddTransient<EditDbzCharactersViewModel>();
        builder.Services.AddTransient<EditDbzCharactersPage>();

        builder.Services.AddTransient<ShowUsersMainViewModel>();
        builder.Services.AddTransient<ShowUsers>();





        builder.Services.AddTransient<UserManagement>();


    

        return builder.Build();
	}
}
