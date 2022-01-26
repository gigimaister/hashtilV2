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
    public class Avarage : AppCompatActivity
    {
        string scanresult, UserName, Hamama, Gamlon;
        private Button btn_Send, btn_Back;
        private EditText AVG, Remarks;
        private ProgressBar progressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.avarage);
            btn_Send = FindViewById<Button>(Resource.Id.ok);
            btn_Back = FindViewById<Button>(Resource.Id.back);
            AVG = FindViewById<EditText>(Resource.Id.entry);
            Remarks = FindViewById<EditText>(Resource.Id.remarks);
            UserName = Intent.GetStringExtra("User");
            scanresult = Intent.GetStringExtra("passport");
            Hamama = Intent.GetStringExtra("hamama");
            Gamlon = Intent.GetStringExtra("gamlon");
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            progressBar.Visibility = ViewStates.Invisible;

            btn_Send.Click += Send_Avarage;
            btn_Back.Click += Back;
            // Create your application here
        }

        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(Moving_Passport));
            intent.PutExtra("User", UserName);
            intent.PutExtra("passport", scanresult);
            this.StartActivity(intent);
        }

        private void Back(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Moving_Passport));
            intent.PutExtra("User", UserName);
            this.StartActivity(intent);
        }

        private void Send_Avarage(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            WebClient client = new WebClient();
            Uri uri = new Uri("http://hashtildb.pe.hu/limoravaragetable.php");
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("username", UserName);
            parameters.Add("passport", scanresult);
            parameters.Add("hamama", Hamama.ToString());
            parameters.Add("gamlon", Gamlon.ToString());
            parameters.Add("avarage", AVG.Text);
            parameters.Add("remarks", Remarks.Text);
            client.UploadValuesCompleted += Client_UploadValuesCompleted;
            client.UploadValuesAsync(uri, parameters);
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
            });
        }

    }
}