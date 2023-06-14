using MyQualityApp.Services;

namespace MyReference.ViewModel;

[QueryProperty(nameof(MonTxt), "Databc")]
public partial class DetailViewModel : BaseViewModel
{
    DeviceOrientationServices MyDeviceOrientationService;

    [ObservableProperty]
	string monTxt;
    [ObservableProperty]
    Dbzanime dbzAnimeSearch;
	public DetailViewModel()
	{
        this.MyDeviceOrientationService = new DeviceOrientationServices();
        MyDeviceOrientationService.ConfigureScanner();
        MyDeviceOrientationService.MyQueueBuffer.Changed += SerialBuffer_Changed;

    }

    private async void SerialBuffer_Changed(object sender, EventArgs e)
    {
        DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;

        MonTxt = myQueue.Dequeue().ToString();

        foreach (var item in Globals.MyStaticList)
        {
            if (MonTxt == item.id)
            {
                DbzAnimeSearch = item;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "id not found", "OK");

            }
        }

    }

    
}