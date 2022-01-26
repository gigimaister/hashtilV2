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
    public class NextPage : AppCompatActivity
    {

        private Button Btndestroyed;
        private ProgressBar progressBar;
        string usrname, ScanResult;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.next_page);
            Btndestroyed = FindViewById<Button>(Resource.Id.btndestroyed);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            usrname = Intent.GetStringExtra("User");
            ScanResult = Intent.GetStringExtra("passport");

            progressBar.Visibility = ViewStates.Invisible;

            Btndestroyed.Click += Send_Destroyed;

            // Create your application here
        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        private void Send_Destroyed(object sender, EventArgs e)
        {
            try
            {
                progressBar.Visibility = ViewStates.Visible;
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hashtildb.pe.hu/destroyedpassports.php");
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("username", usrname);
                parameters.Add("passport", ScanResult);

                client.UploadValuesCompleted += Client_UploadValuesCompleted;
                client.UploadValuesAsync(uri, parameters);

            }
            catch (SystemException)
            {
                Intent intent = new Intent(this, typeof(AfterScanError));
                intent.PutExtra("User", usrname);

                this.StartActivity(intent);

            }
        }

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                try
                {


                    string id = "";
                    id = Encoding.UTF8.GetString(e.Result);
                    int newId = 0;

                    int.TryParse(id, out newId);

                    switch (newId)
                    {

                        case 0:

                            Intent main = new Intent(this, typeof(MainActivity));
                            main.PutExtra("User", usrname);
                            this.StartActivity(main);
                            progressBar.Visibility = ViewStates.Invisible;
                            break;


                        case 1:
                            Intent intent = new Intent(this, typeof(AfterScanError));
                            intent.PutExtra("User", usrname);
                            this.StartActivity(intent);
                            progressBar.Visibility = ViewStates.Invisible;
                            break;

                        default:

                            Intent error = new Intent(this, typeof(AfterScanError));
                            error.PutExtra("User", usrname);
                            this.StartActivity(error);
                            progressBar.Visibility = ViewStates.Invisible;
                            break;
                    }

                }

                catch (System.Reflection.TargetInvocationException)
                {
                    Intent error = new Intent(this, typeof(AfterScanError));
                    error.PutExtra("User", usrname);
                    this.StartActivity(error);
                    progressBar.Visibility = ViewStates.Invisible;

                }
            });
        }
    }
}