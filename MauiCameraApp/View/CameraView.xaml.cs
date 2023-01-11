using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiCameraApp.Platforms.Android;
using MauiCameraApp.Utility;

namespace MauiCameraApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraView : ContentPage
    {
        public CameraView()
        {
            InitializeComponent();
            ICameraProcess camPro = new CameraProcess(Platform.AppContext, 0);
            camPro.createNativeView();
            camPro.connect();

        }
    }
}