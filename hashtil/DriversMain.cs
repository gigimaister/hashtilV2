
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Xamarin.Essentials;
using ZXing.Mobile;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class DriversMain : MainActivity
    {
        Android.App.ProgressDialog progress;
        Button SendScan, SendByHand, LogOut;
        public string Username, UserLevel;
        private TextView Fromloginusernm;
        public string msg;
        MobileBarcodeScanner scanner;


        protected override  void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            MobileBarcodeScanner.Initialize(Application);

            SetContentView(Resource.Layout.drivers_main);

           

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            UserLevel = pref.GetString("UserLevel", string.Empty);

            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("בטעינה...");
            progress.SetCancelable(false);
            progress.Hide();

            Fromloginusernm = FindViewById<TextView>(Resource.Id.mainusernm);
            Username = Intent.GetStringExtra("User") ?? "KOBI";
            Fromloginusernm.Text = Username;

            scanner = new MobileBarcodeScanner();

            LogOut = this.FindViewById<Button>(Resource.Id.logout);
            SendScan = this.FindViewById<Button>(Resource.Id.scan);
            SendByHand = this.FindViewById<Button>(Resource.Id.u_insertbyhand);

            LogOut.Click += async delegate
            {
                progress.Show();
                Intent logout = new Intent(this, typeof(Login_screen));
                this.StartActivity(logout);
            };


            SendByHand.Click += async delegate
            {
                progress.Show();
                Intent intent = new Intent(this, typeof(DriversComboBox));
                intent.PutExtra("User", Username);

                this.StartActivity(intent);

            };

            SendScan.Click += async delegate
            {
                progress.Show();
                Intent intent = new Intent(this, typeof(DriverLoadingReportSearch));
                intent.PutExtra("User", Username);
                this.StartActivity(intent);
                //Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                //Android.App.AlertDialog alert = dialog.Create();
                //alert.SetTitle("תוכן לא זמין");
                //alert.SetMessage("לא זמין בגרסה זו");
                //alert.SetButton("אוקיי", (c, ev) =>
                //{
                //    // Ok button click task  
                //});
                //alert.Show();
            };

        }

     

        public override void HandleScanResult(ZXing.Result result)
        {

            if (result != null && !string.IsNullOrEmpty(result.Text))
            {

                msg = result.ToString();
                Intent intent = new Intent(this, typeof(AfterScanDrivers));
                intent.PutExtra("scanresult", msg);
                intent.PutExtra("User", Username);

                this.StartActivity(intent);

            }

            else
            {
                Intent intent = new Intent(this, typeof(DriversMain));
                intent.PutExtra("User", Username);

                this.StartActivity(intent);

            }
        }

        public override void OnBackPressed()
        {
                     
                
            if (UserLevel == "4")
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