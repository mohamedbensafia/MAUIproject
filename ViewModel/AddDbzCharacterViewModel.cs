using MyReference.Model;
using Newtonsoft.Json;

namespace MyReference.ViewModel;

public partial class AddDbzCharacterViewModel : ObservableObject
{
	[ObservableProperty]
	Dbzanime dbzanimeNew;
	public AddDbzCharacterViewModel()
	{
		this.dbzanimeNew = new Dbzanime();


	}
	[RelayCommand]
	
	async void Add()
	{

        if ((string.IsNullOrEmpty(DbzanimeNew.id))|| (string.IsNullOrWhiteSpace(DbzanimeNew.id))
               || (string.IsNullOrEmpty(DbzanimeNew.name)) || (string.IsNullOrWhiteSpace(DbzanimeNew.name))
               || (string.IsNullOrEmpty(DbzanimeNew.race)) || (string.IsNullOrWhiteSpace(DbzanimeNew.race))
               || (string.IsNullOrEmpty(DbzanimeNew.bio)) || (string.IsNullOrWhiteSpace(DbzanimeNew.bio))
               || (string.IsNullOrEmpty(DbzanimeNew.attack.ToString())) || (string.IsNullOrWhiteSpace(DbzanimeNew.attack.ToString()))
               || (string.IsNullOrEmpty(DbzanimeNew.defense.ToString())) || (string.IsNullOrWhiteSpace(DbzanimeNew.defense.ToString()))
               || (string.IsNullOrEmpty(DbzanimeNew.abilities)) || (string.IsNullOrWhiteSpace(DbzanimeNew.abilities))
               || (string.IsNullOrEmpty(DbzanimeNew.img)) || (string.IsNullOrWhiteSpace(DbzanimeNew.img))

                 )
        {
            await Shell.Current.DisplayAlert("Error while adding character", "Enter correct values", "OK");
        }
        else
        {
            // Ajoute le nouveau joueur � la liste
            Globals.MyStaticList.Add(DbzanimeNew);

            // Chemin d'acc�s complet au fichier JSON
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "dbzanime.json");

            // R�cup�re le contenu existant du fichier
            string jsonContent = File.ReadAllText(filePath);

            // D�s�rialise le contenu JSON en une liste
            var dbzChar = JsonConvert.DeserializeObject<List<Dbzanime>>(jsonContent);

            // Ajoute le nouveau joueur � la liste d�s�rialis�e
            dbzChar.Add(DbzanimeNew);

            // S�rialise la liste mise � jour en JSON
            string updatedJson = JsonConvert.SerializeObject(dbzChar);

            // �crit le contenu JSON s�rialis� dans le fichier
            File.WriteAllText(filePath, updatedJson);

            await Shell.Current.DisplayAlert("Character added", "You can go back.", "OK");
        }

    }

}