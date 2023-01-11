using MauiCameraApp.View;

namespace MauiCameraApp;

public static class MauiProgram
{
	public static IBootstrapper Platform { get; set; }
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		Platform?.CreateMauiApp(builder);
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<CameraView>();

		return builder.Build();
	}

	public interface IBootstrapper
	{
		MauiApp CreateMauiApp(MauiAppBuilder builder);
	}
}
