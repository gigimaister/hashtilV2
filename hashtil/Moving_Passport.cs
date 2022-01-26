using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class Moving_Passport : AppCompatActivity
    {
        ISharedPreferences pref;
        int greenhouse;
        string scanresult;
        private ProgressBar progressBar;
        private Button Btnsend, Btnplus, Btnmuinus, Back, BTNgamlonminus1, BTNgamlonplus1, BTN5minus, BTNplus5;
        TextView hamama, gamlon, username;
        string UserName, Hamama, Gamlon;
        string btn_id;
        int i = 0;
        int[] hamama_picker;
        int[] gamlon_hahama1 = new int[30] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 };
        int[] gamlon_hahama2 = new int[21] { 1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
        int[] gamlon_hahama3 = new int[10] { 18, 19, 20, 21, 22, 23, 24, 25, 26, 27 };
        int[] gamlon_hahama4 = new int[23] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        int[] gamlon_hahama5 = new int[18] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
        int[] gamlon_hahama6 = new int[27] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };
        int[] gamlon_hahama7 = new int[32] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.moving_passport);

            pref = Application.Context.GetSharedPreferences("Greenhouse_Gamlon_Info", FileCreationMode.Private);
            string pef_hamama = pref.GetString("Hamama", string.Empty);
            string pref_gamlon = pref.GetString("Gamlon", string.Empty);

            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            progressBar.Visibility = ViewStates.Invisible;
            UserName = Intent.GetStringExtra("User") ?? "KOBI";
            scanresult = Intent.GetStringExtra("passport");
            btn_id = Intent.GetStringExtra("BTN_id");
            Hamama = pef_hamama.ToString();
            Gamlon = pref_gamlon.ToString();
            Btnplus = FindViewById<Button>(Resource.Id.btnhamamamplus);
            Btnmuinus = FindViewById<Button>(Resource.Id.btnhamamaminus);
            BTNgamlonminus1 = FindViewById<Button>(Resource.Id.btngamlonminus1);
            BTNgamlonplus1 = FindViewById<Button>(Resource.Id.btngamlonplus1);
            BTN5minus = FindViewById<Button>(Resource.Id.btn5minus);
            BTNplus5 = FindViewById<Button>(Resource.Id.btnplus5);
            Back = FindViewById<Button>(Resource.Id.back);
            Btnsend = FindViewById<Button>(Resource.Id.btnsend);
            username = FindViewById<TextView>(Resource.Id.usrname);
            hamama = FindViewById<TextView>(Resource.Id.hamama);
            gamlon = FindViewById<TextView>(Resource.Id.gamlon);

            username.Text = UserName;

            if (pef_hamama == string.Empty || pref_gamlon == string.Empty)
            {
                Hamama = "1";
                hamama.Text = Hamama;
                Int32.TryParse(Hamama, out greenhouse);
                hamama_picker = gamlon_hahama1;
                gamlon.Text = gamlon_hahama1[i].ToString();

            }
            else
            {
                hamama.Text = pef_hamama.ToString();
                Int32.TryParse(hamama.Text, out greenhouse);
                Int32.TryParse(pref_gamlon, out i);
                switch (greenhouse)
                {
                    case 1:
                        hamama_picker = gamlon_hahama1;
                        break;
                    case 2:
                        hamama_picker = gamlon_hahama2;
                        break;
                    case 3:
                        hamama_picker = gamlon_hahama3;
                        break;
                    case 4:
                        hamama_picker = gamlon_hahama4;
                        break;
                    case 5:
                        hamama_picker = gamlon_hahama5;
                        break;
                    case 6:
                        hamama_picker = gamlon_hahama6;
                        break;
                    case 7:
                        hamama_picker = gamlon_hahama7;
                        break;

                    default:

                        break;

                }


                gamlon.Text = hamama_picker[i].ToString();

            };




            Btnplus.Click += delegate
                {
                    try
                    {



                        greenhouse++;

                        if (greenhouse > 7)
                        {
                            greenhouse = 1;

                        }
                        hamama.Text = greenhouse.ToString();
                        switch (greenhouse)
                        {
                            case 1:
                                hamama_picker = gamlon_hahama1;
                                break;
                            case 2:
                                hamama_picker = gamlon_hahama2;
                                break;
                            case 3:
                                hamama_picker = gamlon_hahama3;
                                break;
                            case 4:
                                hamama_picker = gamlon_hahama4;
                                break;
                            case 5:
                                hamama_picker = gamlon_hahama5;
                                break;
                            case 6:
                                hamama_picker = gamlon_hahama6;
                                break;
                            case 7:
                                hamama_picker = gamlon_hahama7;
                                break;

                            default:

                                break;

                        }
                        gamlon.Text = hamama_picker[i].ToString();

                    }
                    catch (SystemException)
                    {
                        i = 0;
                        gamlon.Text = hamama_picker[i].ToString();
                    }
                };

            Btnmuinus.Click += delegate
            {
                try
                {
                    greenhouse--;

                    if (greenhouse < 1)
                    {
                        greenhouse = 7;
                    }
                    hamama.Text = greenhouse.ToString();
                    switch (greenhouse)
                    {
                        case 1:
                            hamama_picker = gamlon_hahama1;
                            break;
                        case 2:
                            hamama_picker = gamlon_hahama2;
                            break;
                        case 3:
                            hamama_picker = gamlon_hahama3;
                            break;
                        case 4:
                            hamama_picker = gamlon_hahama4;
                            break;
                        case 5:
                            hamama_picker = gamlon_hahama5;
                            break;
                        case 6:
                            hamama_picker = gamlon_hahama6;
                            break;
                        case 7:
                            hamama_picker = gamlon_hahama7;
                            break;

                        default:
                            hamama_picker = gamlon_hahama1;
                            break;


                    }
                    gamlon.Text = hamama_picker[i].ToString();
                }
                catch (SystemException)
                {
                    i = 0;
                    gamlon.Text = hamama_picker[i].ToString();
                }
            };

            Back.Click += delegate
            {
                Intent main = new Intent(this, typeof(MainActivity));
                main.PutExtra("User", UserName);
                this.StartActivity(main);
            };


            BTNgamlonminus1.Click += delegate
            {

                i--;
                if (i < 0)
                {
                    i = hamama_picker.Length - 1;
                }

                gamlon.Text = hamama_picker[i].ToString();
            };

            BTNgamlonplus1.Click += delegate
                {

                    i++;
                    if (i == hamama_picker.Length)
                    {
                        i = 0;
                    }

                    gamlon.Text = hamama_picker[i].ToString();


                };
            BTNplus5.Click += delegate
            {
                try
                {


                    i += 5;
                    if (i > hamama_picker.Length - 1)
                    {
                        i = 0;
                        gamlon.Text = hamama_picker[i].ToString();
                    };
                    gamlon.Text = hamama_picker[i].ToString();
                }
                catch (SystemException)
                {
                    i = 0;
                    gamlon.Text = hamama_picker[i].ToString();
                }
            };
            BTN5minus.Click += delegate
             {
                 i -= 5;
                 if (i < 0)
                 {
                     i = hamama_picker.Length - 1;
                 }
                 gamlon.Text = hamama_picker[i].ToString();
             };
            Btnsend.Click += delegate
            {

                progressBar.Visibility = ViewStates.Visible;
                if (btn_id == "0")
                {
                    ISharedPreferences pref = Application.Context.GetSharedPreferences("Greenhouse_Gamlon_Info", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("Hamama", hamama.Text);
                    edit.PutString("Gamlon", i.ToString());
                    edit.Apply();
                    WebClient client = new WebClient();
                    Uri uri = new Uri("http://hashtildb.pe.hu/movingpassports.php");
                    NameValueCollection parameters = new NameValueCollection();
                    parameters.Add("username", UserName);
                    parameters.Add("passport", scanresult);
                    parameters.Add("hamama", hamama.Text);
                    parameters.Add("gamlon", gamlon.Text);
                    client.UploadValuesCompleted += Client_UploadValuesCompleted;
                    client.UploadValuesAsync(uri, parameters);
                }
                if (btn_id == "1")
                {
                    ISharedPreferences pref = Application.Context.GetSharedPreferences("Greenhouse_Gamlon_Info", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("Hamama", hamama.Text);
                    edit.PutString("Gamlon", i.ToString());
                    edit.Apply();
                    WebClient client = new WebClient();
                    Uri uri = new Uri("http://hashtildb.pe.hu/newpassports.php");
                    NameValueCollection parameters = new NameValueCollection();
                    parameters.Add("username", UserName);
                    parameters.Add("passport", scanresult);
                    parameters.Add("hamama", hamama.Text);
                    parameters.Add("gamlon", gamlon.Text);
                    client.UploadValuesCompleted += Client_UploadValuesCompleted;
                    client.UploadValuesAsync(uri, parameters);
                }
                if (btn_id == "2")
                {
                    ISharedPreferences pref = Application.Context.GetSharedPreferences("Greenhouse_Gamlon_Info", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("Hamama", hamama.Text);
                    edit.PutString("Gamlon", i.ToString());
                    edit.Apply();
                    Intent intent = new Intent(this, typeof(Avarage));
                    intent.PutExtra("User", UserName);
                    intent.PutExtra("passport", scanresult);
                    intent.PutExtra("hamama", hamama.Text);
                    intent.PutExtra("gamlon", gamlon.Text);
                    intent.PutExtra("Hamama", hamama.Text);
                    intent.PutExtra("Gamlon", gamlon.Text);

                    this.StartActivity(intent);
                    progressBar.Visibility = ViewStates.Invisible;

                }
            };


        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                try
                {


                    string id = "";
                    id = Encoding.UTF8.GetString(e.Result);

                    switch (id)
                    {

                        case "0":
                            Intent intent = new Intent(this, typeof(MainActivity));
                            intent.PutExtra("User", UserName);
                            this.StartActivity(intent);

                            break;


                        case "1":
                            Intent error = new Intent(this, typeof(AfterScanError));
                            error.PutExtra("User", UserName);
                            this.StartActivity(error);

                            break;
                    }

                }

                catch (SystemException)
                {
                    Intent error = new Intent(this, typeof(AfterScanError));
                    error.PutExtra("User", UserName);
                    this.StartActivity(error);
                    progressBar.Visibility = ViewStates.Invisible;

                }
                catch (System.Reflection.TargetInvocationException)
                {
                    Intent error = new Intent(this, typeof(AfterScanError));
                    error.PutExtra("User", UserName);
                    this.StartActivity(error);
                    progressBar.Visibility = ViewStates.Invisible;

                }
            });
        }

        protected override void OnResume()
        {
            base.OnResume();

        }

        public override void OnBackPressed()
        {
            Intent main = new Intent(this, typeof(MainActivity));
            main.PutExtra("User", UserName);
            this.StartActivity(main);
        }
    }
}