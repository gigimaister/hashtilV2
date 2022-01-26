using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using ZXing.Mobile;


namespace hashtil

{




    [Activity(Logo = "@drawable/hashtil_logo3", Theme = "@style/Theme.AppCompat.DayNight.NoActionBar", MainLauncher = false, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.KeyboardHidden)]
    public class MainActivity : AppCompatActivity
    {



        Android.App.ProgressDialog progress;
        Button ScanButton;
        Button Logout;
        public string Username, UserLevel;
        private TextView Fromloginusernm;
        public string msg;
        MobileBarcodeScanner scanner;



        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);


            // Set our view from the "main" layout resour
            MobileBarcodeScanner.Initialize(Application);

            SetContentView(Resource.Layout.activity_main);
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            UserLevel = pref.GetString("UserLevel", string.Empty);



            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("בטעינה...");
            progress.SetCancelable(false);

            Fromloginusernm = FindViewById<TextView>(Resource.Id.mainusernm);
            Username = Intent.GetStringExtra("User") ?? "KOBI";
            Fromloginusernm.Text = Username;



            scanner = new MobileBarcodeScanner();

            Logout = this.FindViewById<Button>(Resource.Id.logout);
            ScanButton = this.FindViewById<Button>(Resource.Id.scan);
            Logout.Click += async delegate
           {
               progress.Show();
               Intent logout = new Intent(this, typeof(Login_screen));
               this.StartActivity(logout);
           };

            ScanButton.Click += async delegate
            {
                progress.Show();

                scanner.UseCustomOverlay = false;

                //We can customize the top and bottom text of the default overlay
                scanner.TopText = "יש להחזיק במרחק 6 סמ מהאובייקט";
                scanner.BottomText = "יש להמתין עד לביצוע סריקה";

                var result = await scanner.Scan();
                HandleScanResult(result);
                progress.Hide();
            };



        }

        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }

        virtual public void HandleScanResult(ZXing.Result result)
        {

            if (result != null && !string.IsNullOrEmpty(result.Text))
            {

                msg = result.ToString();
                Intent intent = new Intent(this, typeof(AfterScan));
                intent.PutExtra("scanresult", msg);
                intent.PutExtra("User", Username);

                this.StartActivity(intent);

            }

            else
                NothingToScan();


        }

        public void NothingToScan()
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("User", Username);

            this.StartActivity(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (ZXing.Net.Mobile.Android.PermissionsHandler.NeedsPermissionRequest(this))
                ZXing.Net.Mobile.Android.PermissionsHandler.RequestPermissionsAsync(this);
        }
        public override void OnBackPressed()
        {
            if (UserLevel == "2")
            {
                Intent mainmngr = new Intent(this, typeof(MainManager));
                mainmngr.PutExtra("User", Username.Trim());
                this.StartActivity(mainmngr);
            }
            else
            {
                { }
            }
        }









    }
}


