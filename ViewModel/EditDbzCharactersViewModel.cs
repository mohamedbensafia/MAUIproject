using System.IO;
using System.Text.Json;
namespace MyReference.ViewModel;

[QueryProperty(nameof(DbzanimeUpdate), "Dbzanime")]
public partial class EditDbzCharactersViewModel : BaseViewModel
{
    [ObservableProperty]
    Dbzanime dbzanimeUpdate;
    public EditDbzCharactersViewModel()
	{
		
	}


    [RelayCommand]
    async void EditDbzCharacters()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "dbzanime.json");

        // R�cup�re le contenu JSON existant du fichier
        string jsonContent = File.ReadAllText(filePath);

        // D�s�rialise le contenu JSON en une liste
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var dbzcharacters = JsonSerializer.Deserialize<List<Dbzanime>>(jsonContent, options);

        // Recherche le joueur � modifier
        Dbzanime DbzCharacterToEdit = dbzcharacters.FirstOrDefault(dbzanime => dbzanime.id == DbzanimeUpdate.id);

        if (DbzCharacterToEdit != null)
        {
            // V�rifie que les nouvelles valeurs ne sont pas vides ou ne contiennent que des espaces
            if (!string.IsNullOrEmpty(DbzanimeUpdate.name.Trim()) &&
                !string.IsNullOrEmpty(DbzanimeUpdate.race.Trim()) &&
                !string.IsNullOrEmpty(DbzanimeUpdate.bio.Trim()) &&
                !string.IsNullOrEmpty(DbzanimeUpdate.defense.ToString().Trim()) &&
                !string.IsNullOrEmpty(DbzanimeUpdate.attack.ToString().Trim()) &&
                !string.IsNullOrEmpty(DbzanimeUpdate.abilities.Trim()) &&
                !string.IsNullOrEmpty(DbzanimeUpdate.img.Trim())
                )
            {
                // Met � jour les propri�t�s du joueur avec les nouvelles valeurs
                DbzCharacterToEdit.name = DbzanimeUpdate.name;
                DbzCharacterToEdit.race = DbzanimeUpdate.race;
                DbzCharacterToEdit.bio = DbzanimeUpdate.bio;
                DbzCharacterToEdit.defense = DbzanimeUpdate.defense;
                DbzCharacterToEdit.attack = DbzanimeUpdate.attack; 
                DbzCharacterToEdit.abilities = DbzanimeUpdate.abilities;
                DbzCharacterToEdit.img = DbzanimeUpdate.img;

                // S�rialise la liste mise � jour en JSON
                string updatedJsonContent = JsonSerializer.Serialize(dbzcharacters, options);

                // �crit le contenu JSON s�rialis� dans le fichier
                File.WriteAllText(filePath, updatedJsonContent);

                await Shell.Current.DisplayAlert("Edit successful", "You can go back", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Edit error", "Please enter correct values", "OK");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Edit error", "Character not found", "OK");
        }
    }

}

