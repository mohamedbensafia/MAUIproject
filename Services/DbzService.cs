using MyReference.Model;

namespace MyReference.Services;

public class DbzService : ContentPage
{
	public DbzService()
	{
		
	}

    public async Task<List<Dbzanime>> GetDbzAnime()
    {
        List<Dbzanime> dbzanimes;

        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "dbzanime.json");
        using var reader = new StreamReader(filePath);
        var contents = await reader.ReadToEndAsync();
        dbzanimes = JsonSerializer.Deserialize<List<Dbzanime>>(contents);

        return dbzanimes;
    }

}