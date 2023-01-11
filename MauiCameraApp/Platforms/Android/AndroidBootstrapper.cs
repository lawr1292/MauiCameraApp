using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MauiCameraApp.MauiProgram;
using MauiCameraApp.Utility;

namespace MauiCameraApp.Platforms.Android
{
    internal class AndroidBootstrapper : IBootstrapper
    {
        public MauiApp CreateMauiApp(MauiAppBuilder builder)
        {
            builder.Services.AddScoped<ICameraProcess, CameraProcess>();
            return builder.Build();
        }
    }
}
