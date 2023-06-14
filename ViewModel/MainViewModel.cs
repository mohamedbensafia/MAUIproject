using MyQualityApp.Services;
using MyReference.Model;
using MyReference.View;

namespace MyReference.ViewModel;

public partial class MainViewModel : BaseViewModel
{

    [ObservableProperty] 
    string monTexte = "Add Dbz Characters";
    [ObservableProperty]
    string menuText = "Scanner Dbz Characters";
    [ObservableProperty]
    string submenuText = "Scan";


    public ObservableCollection<Dbzanime> MyShownList { get; } = new();
    public MainViewModel()
    {
    }

    [RelayCommand]
    async Task GoToShowPage(Dbzanime dbzanime)
    {
        if (dbzanime is null)
            return;

        await Shell.Current.GoToAsync(nameof(ShowDbzCharactersPage), true, new Dictionary<string, object>
        {

            {"Dbzanime", dbzanime }

        });
    }

    [RelayCommand]
    public async Task GoToDetailPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(DetailPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }

    [RelayCommand]
    public async Task GoToSignInPage()
    {
        await Shell.Current.GoToAsync(nameof(SignIn), true);
    }

    [RelayCommand]
    public async Task GoToUsersPage()
    {
        await Shell.Current.GoToAsync(nameof(UsersList), true);
    }

    [RelayCommand]
    public async Task GoToLoginPage()
    {
        await Shell.Current.GoToAsync(nameof(Login), true);
    }

    [RelayCommand]
    public async Task GoToAddCharacterPage()
    {
        await Shell.Current.GoToAsync(nameof(AddDbzCharacter), true);
    }
    [RelayCommand]
    async Task DbzFromJSON()
    {
        if (IsBusy) return;

        DbzService MyService = new();

        try
        {
            IsBusy = true;
            Globals.MyStaticList = await MyService.GetDbzAnime();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error : {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally { IsBusy = false; }

        Refresh();
       
    }
    public void Refresh()
    {
        MyShownList.Clear();

        foreach (Dbzanime dbz in Globals.MyStaticList)
        {
            MyShownList.Add(dbz);
        }
    }
}