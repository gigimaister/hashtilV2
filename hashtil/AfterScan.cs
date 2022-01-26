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
    public class AfterScan : AppCompatActivity
    {

        static string btn_id;
        private Button btnready, btnproblem, btnmoving_new, btnmoving_inside, btnaudit_pass, Btnpage2;
        string ScanResult, usrname, ScanName;
        private string Dbmessage, PDbmessage;
        private TextView scantxt;
        private ProgressBar progressBar;


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.after_scan);



            btnready = FindViewById<Button>(Resource.Id.btnReady);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            btnaudit_pass = FindViewById<Button>(Resource.Id.btnaudit_pass);
            Btnpage2 = FindViewById<Button>(Resource.Id.btnnextpage);
            btnproblem = FindViewById<Button>(Resource.Id.btnProblem);
            btnmoving_new = FindViewById<Button>(Resource.Id.btnmoving_new);
            btnmoving_inside = FindViewById<Button>(Resource.Id.btnmoving_inside);
            scantxt = FindViewById<TextView>(Resource.Id.scanresult);
            usrname = Intent.GetStringExtra("User");
            progressBar.Visibility = ViewStates.Invisible;
            ScanResult = Intent.GetStringExtra("scanresult");
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            ScanName = usrname.ToString();

            PDbmessage = ScanResult.ToString() + " בעיה!";
            Dbmessage = ScanResult.ToString() + " מוכן";

            scantxt.Text = usrname + ":\n" + ScanResult;

            btnmoving_new.Click += send_new;

            btnmoving_inside.Click += send_inside;

            btnaudit_pass.Click += send_audit;

            btnready.Click += Send_Click1;

            btnproblem.Click += Send_Click3;

            Btnpage2.Click += Next_Page;


        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        private void Next_Page(object sender, EventArgs e)
        {
            if (ScanResult.Length != 5)
            {
                Intent nextpage = new Intent(this, typeof(AfterScanError));
                nextpage.PutExtra("User", usrname);

                this.StartActivity(nextpage);
            }
            else
            {
                progressBar.Visibility = ViewStates.Visible;
                Intent intent = new Intent(this, typeof(NextPage));
                intent.PutExtra("User", usrname);
                intent.PutExtra("passport", ScanResult);
                this.StartActivity(intent);
                progressBar.Visibility = ViewStates.Invisible;
            }
        }

        private void send_audit(object sender, EventArgs e)
        {
            if (ScanResult.Length != 5)
            {
                Intent err2 = new Intent(this, typeof(AfterScanError));
                err2.PutExtra("User", usrname);

                this.StartActivity(err2);
            }
            else
            {
                progressBar.Visibility = ViewStates.Visible;
                Intent intent = new Intent(this, typeof(Audit_Passports));
                intent.PutExtra("User", usrname);
                intent.PutExtra("passport", ScanResult);
                this.StartActivity(intent);
                progressBar.Visibility = ViewStates.Invisible;
            }
        }


        // Create your application here

        protected override void OnResume()
        {
            base.OnResume();

        }

        private void send_new(object sender, EventArgs e)
        {
            if (ScanResult.Length != 5)
            {
                Intent err = new Intent(this, typeof(AfterScanError));
                err.PutExtra("User", usrname);

                this.StartActivity(err);
            }
            else
            {
                progressBar.Visibility = ViewStates.Visible;
                btn_id = "1";
                Intent intent = new Intent(this, typeof(Moving_Passport));
                intent.PutExtra("User", usrname);
                intent.PutExtra("BTN_id", btn_id);
                intent.PutExtra("passport", ScanResult);
                this.StartActivity(intent);
                progressBar.Visibility = ViewStates.Invisible;
            }

        }
        private void send_inside(object sender, EventArgs e)
        {
            if (ScanResult.Length != 5)
            {
                Intent err2 = new Intent(this, typeof(AfterScanError));
                err2.PutExtra("User", usrname);

                this.StartActivity(err2);
            }
            else
            {
                progressBar.Visibility = ViewStates.Visible;
                btn_id = "0";
                Intent intent = new Intent(this, typeof(Moving_Passport));
                intent.PutExtra("User", usrname);
                intent.PutExtra("BTN_id", btn_id);
                intent.PutExtra("passport", ScanResult);

                this.StartActivity(intent);
                progressBar.Visibility = ViewStates.Invisible;
            }
        }


        private void Send_Click1(object sender, EventArgs e)
        {
            if (ScanResult.Length < 15)
            {
                Intent intent = new Intent(this, typeof(AfterScanError));
                intent.PutExtra("User", usrname);

                this.StartActivity(intent);

            }
            else
            {
                try
                {
                    progressBar.Visibility = ViewStates.Visible;
                    WebClient client = new WebClient();
                    Uri uri = new Uri("http://hashtildb.pe.hu/insertscan.php");
                    NameValueCollection parameters = new NameValueCollection();
                    parameters.Add("user", ScanName);
                    parameters.Add("message", Dbmessage);

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

        private void Send_Click3(object sender, EventArgs e)
        {
            if (ScanResult.Length != 5)
            {
                Intent err2 = new Intent(this, typeof(AfterScanError));
                err2.PutExtra("User", usrname);

                this.StartActivity(err2);
            }
            else
            {
                progressBar.Visibility = ViewStates.Visible;
                btn_id = "2";
                Intent intent = new Intent(this, typeof(Moving_Passport));
                intent.PutExtra("User", usrname);
                intent.PutExtra("BTN_id", btn_id);
                intent.PutExtra("passport", ScanResult);

                this.StartActivity(intent);
                progressBar.Visibility = ViewStates.Invisible;
            }

        }

    }

}