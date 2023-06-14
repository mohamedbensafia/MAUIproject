using MyReference.Model;
using MyReference.Services;
using System.Data;

namespace MyReference.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    UserManagement MyUserTables;

    [ObservableProperty]
    public static int var;
    [ObservableProperty]
    public bool alreadyLogged;
    bool isNotBusy = true;

    public ObservableCollection<User> MyShownList { get; set; } = new();


    public LoginViewModel()
    {
        this.MyUserTables = new();
        MyUserTables.ConfigTools();
    }

    [RelayCommand]
    public async Task GoToSignInPage()
    {
        await Shell.Current.GoToAsync(nameof(SignIn), true);
    }
    [RelayCommand]
    public async Task CheckDB()
    {
        isNotBusy = false;

        bool isValidUser = MyUserTables.CheckCredentials(LoginEntry, Password);
        Globals.UserSets.Tables["Users"].Clear();
        Globals.UserSets.Tables["Access"].Clear();

        await MyUserTables.ReadAccessTable();
        await MyUserTables.FillUsers();

        await MoveIntoList();

        if (isValidUser)
        {
            AlreadyLogged = true;

            await Shell.Current.DisplayAlert("Welcome", "Connexion successful!", "OK");
        }
        if(AlreadyLogged == false)
        {

            await Shell.Current.DisplayAlert("Error", "Wrong datas", "OK");
        }

    }



    private string _password;
    public string Password
    {
        get { return _password; }
        set
        {
            if (_password != value)
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }

    private string _loginEntry;
    public string LoginEntry
    {
        get { return _loginEntry; }
        set
        {
            if (_loginEntry != value)
            {
                _loginEntry = value;
                OnPropertyChanged(nameof(LoginEntry));
            }
        }
    }


    internal async Task MoveIntoList()
    {
        List<User> MyList = new();

        try
        {
            MyList = Globals.UserSets.Tables["Users"].AsEnumerable().Select(m => new User()
            {
                Id = m.Field<Int16>("User_ID"),
                Name = m.Field<string>("UserName"),
                Password = m.Field<string>("UserPassword"),
                AccessType = m.Field<Int16>("UserAccessType"),
            }).ToList();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        MyShownList.Clear();

        foreach (User item in MyList)
        {
            MyShownList.Add(item);
        }
    }

}