using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Firebase.Iid;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class MainManager : AppCompatActivity
    {

        Button btnToken, btnnewmagash, btninsideshinua, btnauditreport, btnaudditproblem;
        Button btnMainMsg;
        Button btnLogOut;
        TextView User;
        string UserName, UserLevel;
        Android.App.ProgressDialog progress;



        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.main_manager);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string userName = pref.GetString("User", string.Empty);
            string password = pref.GetString("Password", string.Empty);

            btnToken = FindViewById<Button>(Resource.Id.btntoken);
            btnnewmagash = FindViewById<Button>(Resource.Id.newpassportinsidegreenhouse);
            btninsideshinua = FindViewById<Button>(Resource.Id.btninsideshinua);
            btnauditreport = FindViewById<Button>(Resource.Id.btnauditreport);
            btnaudditproblem = FindViewById<Button>(Resource.Id.btnauditproblemreport);
            btnMainMsg = FindViewById<Button>(Resource.Id.btnmainmsg);
            btnLogOut = FindViewById<Button>(Resource.Id.logout);
            User = FindViewById<TextView>(Resource.Id.user);

            UserName = Intent.GetStringExtra("User") ?? userName;
            UserLevel = pref.GetString("UserLevel", string.Empty);
            User.Text = UserName;

            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("בטעינה...");
            progress.SetCancelable(false);
            progress.Hide();




            btnToken.Click += btnToken_Click;
            btnMainMsg.Click += btnMainMsg_Click;
            btnLogOut.Click += btnLogOut_Click;
            btnnewmagash.Click += Btnnewmagash_Click;
            btninsideshinua.Click += Btninsideshinua_Click;
            btnauditreport.Click += Btnauditreport_Click;
            btnaudditproblem.Click += btnaudditproblem_Click;

            // Create your application here
        }

        private void btnaudditproblem_Click(object sender, EventArgs e)
        {
            progress.Show();
            Intent mainm2 = new Intent(this, typeof(MainManager2));
            mainm2.PutExtra("User", UserName);
            this.StartActivity(mainm2);
        }

        private void Btnauditreport_Click(object sender, EventArgs e)
        {
            progress.Show();
            Intent auditreport = new Intent(this, typeof(AuditPassportsToCheckTable));
            auditreport.PutExtra("User", UserName);
            this.StartActivity(auditreport);
        }

        private void Btninsideshinua_Click(object sender, EventArgs e)
        {
            progress.Show();
            Intent insideshinua = new Intent(this, typeof(Moving_Passports_Inside_Greenhouse));
            insideshinua.PutExtra("User", UserName);
            this.StartActivity(insideshinua);


        }

        private void Btnnewmagash_Click(object sender, EventArgs e)
        {
            progress.Show();
            Intent newmagash = new Intent(this, typeof(New_Magash));
            newmagash.PutExtra("User", UserName);
            this.StartActivity(newmagash);

        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        protected override void OnResume()
        {
            base.OnResume();

        }

        public override void OnBackPressed()
        {

            if (UserLevel == "2")
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("User", User.Text.Trim());
                intent.PutExtra("UserLevel", "2");
                this.StartActivity(intent);
            }
            if (UserLevel == "4")
            {
                Intent intent = new Intent(this, typeof(DriversMain));
                intent.PutExtra("User", User.Text.Trim());
                intent.PutExtra("UserLevel", "4");
                this.StartActivity(intent);
            }
            else
            {
                { }
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            progress.Show();
            Intent logout = new Intent(this, typeof(Login_screen));
            this.StartActivity(logout);
        }

        private void btnMainMsg_Click(object sender, EventArgs e)
        {
            progress.Show();
            Intent msgchat = new Intent(this, typeof(ReportTable));
            msgchat.PutExtra("User", UserName);
            this.StartActivity(msgchat);

        }

        private void btnToken_Click(object sender, EventArgs e)
        {
            progress.Show();
            try
            {

                string Token = FirebaseInstanceId.Instance.Token;
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hashtildb.pe.hu/sendtoken.php");
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("user", User.Text.Trim());
                parameters.Add("token", Token);

                client.UploadValuesCompleted += Client_UploadValuesCompleted;
                client.UploadValuesAsync(uri, parameters);
            }
            catch (SystemException ex)
            {
                progress.Hide();
                User.Text = ex.ToString();
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
                            Intent intent = new Intent(this, typeof(AfterToken));
                            intent.PutExtra("User", User.Text.Trim());
                            this.StartActivity(intent);
                            progress.Hide();
                            break;


                        case 1:
                            User.Text = "שליחה נכשלה...נא לנסות שנית";
                            progress.Hide();
                            break;

                        default:
                            User.Text = "שליחה נכשלה...נא לנסות שנית";
                            progress.Hide();
                            break;
                    }

                }

                catch (System.Reflection.TargetInvocationException)
                {
                    Intent error = new Intent(this, typeof(AfterScanError));
                    error.PutExtra("User", UserName);
                    this.StartActivity(error);
                    progress.Hide();

                }
            });
        }
    }
}