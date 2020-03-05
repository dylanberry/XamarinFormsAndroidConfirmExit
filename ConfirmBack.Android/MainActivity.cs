using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Widget;

namespace ConfirmBack.Droid
{
    [Activity(Label = "ConfirmBack", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        bool _isBackPressed = false;
        public override void OnBackPressed()
        {
            var app = (ConfirmBack.App)App.Current;
            if (app.PromptToConfirmExit)
            {
                if (app.IsToastExitConfirmation)
                    ConfirmWithToast();
                else
                    ConfirmWithDialog();

                return;
            }
            base.OnBackPressed();
        }

        private void ConfirmWithDialog()
        {
            using (var alert = new AlertDialog.Builder(this))
            {
                alert.SetTitle("Confirm Exit");
                alert.SetMessage("Are you sure you want to exit?");
                alert.SetPositiveButton("Yes", (sender, args) => { FinishAffinity(); });
                alert.SetNegativeButton("No", (sender, args) => { }); // do nothing

                var dialog = alert.Create();
                dialog.Show();
            }
            return;
        }

        private void ConfirmWithToast()
        {
            if (_isBackPressed)
            {
                FinishAffinity(); // inform Android that we are done with the activity
                return;
            }

            _isBackPressed = true;
            Toast.MakeText(this, "Press back again to exit", ToastLength.Short).Show();

            // Disable back to exit after 2 seconds.
            new Handler().PostDelayed(() => { _isBackPressed = false; }, 2000);
            return;
        }
    }
}