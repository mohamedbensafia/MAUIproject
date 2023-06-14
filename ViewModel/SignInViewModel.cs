namespace MyReference.ViewModel;

public partial class SignInViewModel : ObservableObject
{
    UserManagement MyUserTablesSignIn;

    bool isNotBusySignIn = true;

    public ObservableCollection<User> MyShownList { get; set; } = new();

    public SignInViewModel()
    {
        this.MyUserTablesSignIn = new();
        MyUserTablesSignIn.ConfigTools();
    }

    [RelayCommand]
    public async Task GoToLoginPage()
    {
        await Shell.Current.GoToAsync(nameof(Login), true);
    }


    [RelayCommand]
    internal async Task InsertDB()
    {

        isNotBusySignIn = false;
        await MyUserTablesSignIn.InsertUsers(LoginEntry, Password, 2);
        await MoveIntoList();
        isNotBusySignIn = true;
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