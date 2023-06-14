using System.IO;
using System.Text.Json;
namespace MyReference.ViewModel;

[QueryProperty("Dbzanime", "Dbzanime")]
public partial class ShowDbzCharactersPageViewModel : BaseViewModel
{

    [ObservableProperty]
    Dbzanime dbzanime;


    public ShowDbzCharactersPageViewModel()
	{
		
	}

    [RelayCommand]
    public async Task GoToDbzCharactersPage(Dbzanime dbzanime)
    {
        await Shell.Current.GoToAsync(nameof(EditDbzCharactersPage), true, new Dictionary<string, object>
        {

            {"Dbzanime", dbzanime }

        });
    }

    [RelayCommand]
    async void DeleteDbzCharacter(Dbzanime dbzanime)
    {
        if (Globals.MyStaticList.Contains(dbzanime))
        {
            Globals.MyStaticList.Remove(dbzanime);

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "dbzanime.json");

            string jsonContent = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dbzcharacters = JsonSerializer.Deserialize<List<Dbzanime>>(jsonContent, options);

            dbzcharacters.RemoveAll(j => j.id == dbzanime.id);

            string updatedJson = JsonSerializer.Serialize(dbzcharacters, options);

            File.WriteAllText(filePath, updatedJson);
        }

        await Shell.Current.DisplayAlert("Successful delete", "You can go back", "OK");
    }



}

