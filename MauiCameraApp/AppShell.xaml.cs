using Microsoft.Maui;

namespace MauiCameraApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("Camera", typeof(View.CameraView));

    }
}
