namespace MyReference.ViewModel;

[QueryProperty("User", "User")]
public partial class ShowUsersMainViewModel : BaseViewModel
{
    UserManagement MyUserTablesShow;
    bool isNotBusyList = true;
    public ObservableCollection<User> MyShownList { get; set; } = new();
    public ObservableCollection<User> MyUsers { get; set; } = new();
    UserManagement MyDBService = new();
    [ObservableProperty]
    User user;

    public ShowUsersMainViewModel()
	{
        this.MyUserTablesShow = new();
        MyUserTablesShow.ConfigTools();
    }


    [RelayCommand]
    async Task EditUser()
    {
        // await ReadAccess();
        try
        {
            await MyUserTablesShow.UpdateUser(User.Id, User.Name, User.Password, User.AccessType);
            await Shell.Current.DisplayAlert("User Updated", "Successful", "OK");

        }
        catch (Exception e)
        {

            await Shell.Current.DisplayAlert("User not edited", e.Message, "OK");
        }
        await ReadAccess();
        MoveIntoList();
 

    }
    

    [RelayCommand]
    public async Task DeleteUser()
    {

        isNotBusyList = false;
        await MyUserTablesShow.DeleteUsers(User.Name);
        await MoveIntoList();
        isNotBusyList = true;
    }


    async Task ReadAccess()
    {

        Globals.UserSets.Tables["Users"].Clear();
        Globals.UserSets.Tables["Access"].Clear();
        try
        {
            await MyDBService.ReadAccessTable();
            await MyDBService.ReadUserTable();

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Databse", ex.Message, "OK");
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

