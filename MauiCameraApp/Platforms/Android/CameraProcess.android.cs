using Android.Content;
using MauiCameraApp.Utility;
using Android.Views;
using AndroidX.Camera.Core;
using AndroidX.Camera.Lifecycle;
using AndroidX.Camera.View;
using AndroidX.Core.Content;
using Java.Util;
using Java.Util.Concurrent;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;

namespace MauiCameraApp.Platforms.Android
{
    public class CameraProcess : ICameraProcess
    {
        protected Context Context;
        public CameraLocation CameraLocation { get; private set; }
        AndroidX.Camera.Core.Preview cameraPreview;
        ImageAnalysis imageAnalyzer;
        PreviewView previewView;
        IExecutorService cameraExecutor;
        CameraSelector cameraSelector = null;
        ProcessCameraProvider cameraProvider;
        ICamera camera;

        public CameraProcess(Context context, CameraLocation cameraLocation)
        {
            Context = context;
            CameraLocation = cameraLocation;
        }

        public NativePlatformCameraPreviewView createNativeView()
        {
            previewView = new PreviewView(Context);
            cameraExecutor = Executors.NewSingleThreadExecutor();
            return previewView;
        }

        public void connect()
        {
            var cameraProviderFuture = ProcessCameraProvider.GetInstance(Context);

            cameraProviderFuture.AddListener(new Java.Lang.Runnable(() =>
            {
                // Used to bind the lifecycle of cameras to the lifecycle owner
                cameraProvider = (ProcessCameraProvider)cameraProviderFuture.Get();

                // Preview
                cameraPreview = new AndroidX.Camera.Core.Preview.Builder().Build();
                cameraPreview.SetSurfaceProvider(previewView.SurfaceProvider);

                updateCamera();

            }), ContextCompat.GetMainExecutor(Context)); //GetMainExecutor: returns an Executor that runs on the main thread.
        }


        public void disconnect()
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            cameraExecutor?.Shutdown();
            cameraExecutor?.Dispose();
        }

        public void updateCamera()
        {
            if (cameraProvider != null)
            {
                // Unbind use cases before rebinding
                //cameraProvider.UnbindAll();

                var cameraLocation = CameraLocation;

                // Select back camera as a default, or front camera otherwise
                if (cameraLocation == CameraLocation.Rear && cameraProvider.HasCamera(CameraSelector.DefaultBackCamera))
                    cameraSelector = CameraSelector.DefaultBackCamera;
                else if (cameraLocation == CameraLocation.Front && cameraProvider.HasCamera(CameraSelector.DefaultFrontCamera))
                    cameraSelector = CameraSelector.DefaultFrontCamera;
                else
                    cameraSelector = CameraSelector.DefaultBackCamera;

                if (cameraSelector == null)
                    throw new System.Exception("Camera not found");


                // The Context here SHOULD be something that's a lifecycle owner
                if (Context is AndroidX.Lifecycle.ILifecycleOwner lifecycleOwner)
                {
                    camera = cameraProvider.BindToLifecycle(lifecycleOwner, cameraSelector, cameraPreview);
                }
                else if (Microsoft.Maui.ApplicationModel.Platform.CurrentActivity is AndroidX.Lifecycle.ILifecycleOwner maLifecycleOwner)
                {
                    // if not, this should be sufficient as a fallback
                    camera = cameraProvider.BindToLifecycle(maLifecycleOwner, cameraSelector, cameraPreview);
                }
            }
        }

        public void updateCameraLocation(CameraLocation cameraLocation)
        {
            CameraLocation = cameraLocation;

            updateCamera();
        }

        public void updateTorch(bool toggle)
        {
            camera?.CameraControl?.EnableTorch(toggle);
        }
    }
}
