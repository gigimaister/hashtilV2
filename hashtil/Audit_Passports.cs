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
    public class Audit_Passports : AppCompatActivity
    {
        int audit_bin = 0;
        string scanresult, UserName, audit_diagnostic;
        private Button btn_Send, btn_Back;
        private EditText Remarks;
        private CheckBox Chgood, Chstuck, Chsick, Chburn, Chsproutingproblem;
        private ProgressBar progressBar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.audit_passports);
            Chgood = FindViewById<CheckBox>(Resource.Id.goodpassport);
            Chsick = FindViewById<CheckBox>(Resource.Id.sick);
            Chburn = FindViewById<CheckBox>(Resource.Id.burn);
            Chstuck = FindViewById<CheckBox>(Resource.Id.stuck);
            Chsproutingproblem = FindViewById<CheckBox>(Resource.Id.bpavg);
            btn_Send = FindViewById<Button>(Resource.Id.ok);
            btn_Back = FindViewById<Button>(Resource.Id.back);
            Remarks = null;
            Remarks = FindViewById<EditText>(Resource.Id.shortremarks);
            UserName = Intent.GetStringExtra("User");
            scanresult = Intent.GetStringExtra("passport");
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            progressBar.Visibility = ViewStates.Invisible;
            audit_diagnostic = "";
            btn_Send.Click += Send_Audit;
            btn_Back.Click += Back;




        }

        private void Back(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AfterScan));
            intent.PutExtra("User", UserName);
            intent.PutExtra("scanresult", scanresult);
            this.StartActivity(intent);
        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        private void Send_Audit(object sender, EventArgs e)
        {

            if (Chgood.Checked)
            {
                audit_bin += 1;
                audit_diagnostic += "תקין";
            }
            if (Chsick.Checked)
            {
                audit_bin += 2;
                audit_diagnostic += "חולה";
            }
            if (Chstuck.Checked)
            {
                audit_bin += 2;
                audit_diagnostic += "תקוע";
            }
            if (Chburn.Checked)
            {
                audit_bin += 2;
                audit_diagnostic += "שרוף";
            }
            if (Chsproutingproblem.Checked)
            {
                audit_bin += 2;
                audit_diagnostic += "בעיית נביטה קשה";
            }



            if (audit_diagnostic == "" || audit_bin > 2)
            {
                Intent err2 = new Intent(this, typeof(AfterScanError));
                err2.PutExtra("User", UserName);

                this.StartActivity(err2);
                return;

            }


            try
            {
                progressBar.Visibility = ViewStates.Visible;
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hashtildb.pe.hu/auditpassports.php");
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("username", UserName);
                parameters.Add("passport", scanresult);
                parameters.Add("audit_remark", audit_diagnostic);
                parameters.Add("audit_bin", audit_bin.ToString());
                parameters.Add("audit_free_remark", Remarks.Text.ToString());
                client.UploadValuesCompleted += Client_UploadValuesCompleted;
                client.UploadValuesAsync(uri, parameters);
            }
            catch (SystemException)
            {
                Intent err2 = new Intent(this, typeof(AfterScanError));
                err2.PutExtra("User", UserName);

                this.StartActivity(err2);

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
    }
}