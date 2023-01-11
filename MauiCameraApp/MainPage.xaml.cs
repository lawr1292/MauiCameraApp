namespace MauiCameraApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	public async void cameraClick(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("Camera");
	}
}

