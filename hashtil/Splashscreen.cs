
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hashtil
{
    [Activity(Theme = "@style/MyTheme.Splash", NoHistory = true, MainLauncher = true)]
    public class Splashscreen : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(Splashscreen).Name;
        string usr, pwd;
        ISharedPreferences pref;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQ3MTY2QDMxMzgyZTMzMmUzMFFKaE1kT3ZaczFPYUFYTDByT1NTSTBPb01MMmFLTC94a2ZrZG1GOGdha0E9");
            Log.Debug(TAG, "SplashActivity.OnCreate");

            pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string userName = pref.GetString("User", string.Empty);
            string password = pref.GetString("Password", string.Empty);
            usr = userName;
            pwd = password;

            if (userName == string.Empty || password == string.Empty)

            {
                Intent intent = new Intent(this, typeof(Login_screen));
                this.StartActivity(intent);
            }

            else
            {
                try
                {
                    WebClient client = new WebClient();
                    Uri uri = new Uri("http://hashtildb.pe.hu/login.php");
                    NameValueCollection parameters = new NameValueCollection();
                    parameters.Add("username", userName);
                    parameters.Add("password", password);
                    client.UploadValuesCompleted += Client_UploadValuesCompleted;
                    client.UploadValuesAsync(uri, parameters);



                }

                catch (SystemException)
                {
                    Toast.MakeText(Application, "בעיית תקשורת..מנסה שוב", ToastLength.Short).Show();
                    this.Recreate();

                }
            }

        }

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            try
            {


                string id = "";
                id = Encoding.UTF8.GetString(e.Result);
                int newId = 0;

                int.TryParse(id, out newId);

                switch (newId)
                {

                    case 1:


                        Intent intent = new Intent(this, typeof(MainActivity));
                        intent.PutExtra("User", usr);
                        ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                        ISharedPreferencesEditor edit = pref.Edit();
                        edit.PutString("UserLevel", "1");
                        edit.Apply();
                        this.StartActivity(intent);

                        break;


                    case 2:


                        Intent mainmngr = new Intent(this, typeof(MainManager));
                        mainmngr.PutExtra("User", usr);
                        ISharedPreferences go = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                        ISharedPreferencesEditor yo = go.Edit();
                        yo.PutString("UserLevel", "2");
                        yo.Apply();
                        this.StartActivity(mainmngr);

                        break;

                    case 3:


                        Intent driver = new Intent(this, typeof(DriversMain));
                        driver.PutExtra("User", usr);
                        ISharedPreferences dr = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                        ISharedPreferencesEditor drv = dr.Edit();
                        drv.PutString("UserLevel", "3");
                        drv.Apply();
                        this.StartActivity(driver);

                        break;
                    case 4:


                        Intent manager_driver = new Intent(this, typeof(DriversMain));
                        manager_driver.PutExtra("User", usr);
                        ISharedPreferences drv_mng = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                        ISharedPreferencesEditor drv_mng_2 = drv_mng.Edit();
                        drv_mng_2.PutString("UserLevel", "4");
                        drv_mng_2.Apply();
                        this.StartActivity(manager_driver);

                        break;
                    case 5:

                       
                        Intent contract_1 = new Intent(this, typeof(DriversMain));
                        contract_1.PutExtra("User", usr);
                        ISharedPreferences pref_contract_1 = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                        ISharedPreferencesEditor edit_contract_1 = pref_contract_1.Edit();
                        edit_contract_1.PutString("UserLevel", "5");
                        edit_contract_1.Apply();
                        //mainmngr.PutExtra("UserLevel","2");

                        this.StartActivity(contract_1);

                        break;
                    case 6:

                       
                        Intent contract_2 = new Intent(this, typeof(DriversMain));
                        contract_2.PutExtra("User", usr);
                        ISharedPreferences pref_contract_2 = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                        ISharedPreferencesEditor edit_contract_2 = pref_contract_2.Edit();
                        edit_contract_2.PutString("UserLevel", "6");
                        edit_contract_2.Apply();
                        //mainmngr.PutExtra("UserLevel","2");

                        this.StartActivity(contract_2);

                        break;
                    case 7:

                        
                        Intent contract_3 = new Intent(this, typeof(DriversMain));
                        contract_3.PutExtra("User", usr);
                        ISharedPreferences pref_contract_3 = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                        ISharedPreferencesEditor edit_contract_3 = pref_contract_3.Edit();
                        edit_contract_3.PutString("UserLevel", "7");
                        edit_contract_3.Apply();
                        //mainmngr.PutExtra("UserLevel","2");

                        this.StartActivity(contract_3);

                        break;

                    default:


                        Intent login = new Intent(this, typeof(Login_screen));
                        this.StartActivity(login);
                        this.Finish();


                        break;
                }

            }
            catch (System.Reflection.TargetInvocationException)
            {



                Intent login = new Intent(this, typeof(Login_screen));
                this.StartActivity(login);
                this.Finish();

            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            _ = Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(10); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");

        }

    }
}