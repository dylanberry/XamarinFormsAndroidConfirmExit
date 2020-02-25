using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

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

        public override void OnBackPressed()
        {
            if (((ConfirmBack.App)App.Current).PromptToConfirmExit)
            {
                using (var alert = new AlertDialog.Builder(this))
                {
                    alert.SetTitle("Confirm Exit?");
                    alert.SetMessage("Are you sure you want to exit?");
                    alert.SetPositiveButton("Yes", (sender, args) => { FinishAffinity(); }); // inform Android that we are done with the activity
                    alert.SetNegativeButton("No", (sender, args) => {}); // do nothing

                    var dialog = alert.Create();
                    dialog.Show();
                }
                return;
            }
            base.OnBackPressed();
        }
    }
}