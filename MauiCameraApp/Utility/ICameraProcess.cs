using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiCameraApp.Utility
{
    public interface ICameraProcess : IDisposable
    {
        void updateCameraLocation(CameraLocation cameraLocation);
        NativePlatformCameraPreviewView createNativeView();
        void connect();
        void disconnect();
        void updateCamera();
        void updateTorch(bool toggle);
    }
}
