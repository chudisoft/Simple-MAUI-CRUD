using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace SimpleMAUICRUD
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        // Define your request codes as constants
        const int RequestStorageCode = 100; // This is an arbitrary number you choose

        public void RequestStoragePermission()
        {
            // Here's where you request the permissions and use your request code
            RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, RequestStorageCode);
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == RequestStorageCode) // Replace with your actual request code
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {
                    // Permission was granted, continue with file operations
                }
                else
                {
                    // Permission was denied
                    Toast.MakeText(this, "Storage permission is required.", ToastLength.Long).Show();

                    // Delay closing the app to let the user see the toast
                    Task.Delay(2000).ContinueWith(t =>
                    {
                        Finish(); // Close the app
                    }, TaskScheduler.FromCurrentSynchronizationContext()); // Ensure this runs on the UI thread
                }
            }
        }

        public async Task TryToGetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                await GetPermissionsAsync();
                return;
            }
        }

        const int RequestLocationId = 0;
        readonly string[] PermissionsGroupLocation =
            {
                //TODO add more permissions
                Manifest.Permission.ReadExternalStorage,
                Manifest.Permission.WriteExternalStorage,
            };

        async Task GetPermissionsAsync()
        {
            const string permission = Manifest.Permission.WriteExternalStorage;

            if (CheckSelfPermission(permission) == (int)Permission.Granted)
            {
                // The permission is already available
                return;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                // Explain to the user why you need the permission
            }

            RequestPermissions(PermissionsGroupLocation, RequestLocationId);
        }

    }
}
