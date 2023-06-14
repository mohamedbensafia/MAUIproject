using MyReference.Services;

namespace MyReference.ViewModel;

public partial class UsersListViewModel : BaseViewModel
{

    UserManagement MyUserTablesList;

    bool isNotBusyList = true;
    public ObservableCollection<User> MyShownList { get; set; } = new();

    public UsersListViewModel()
	{
        this.MyUserTablesList = new();
        MyUserTablesList.ConfigTools();
    }

    [RelayCommand]
    public async Task ShowList()
    {
        isNotBusyList = false;

        Globals.UserSets.Tables["Users"].Clear();
        Globals.UserSets.Tables["Access"].Clear();

        await MyUserTablesList.FillUsers();

        await MoveIntoList();

        isNotBusyList = true;
    }

    [RelayCommand]
    async Task GoToShowUserPage(User user)
    {
        if (user is null)
            return;

        await Shell.Current.GoToAsync(nameof(ShowUsers), true, new Dictionary<string, object>
        {

            {"User", user }

        });
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