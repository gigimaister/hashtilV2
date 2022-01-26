using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class MainManager2 : AppCompatActivity
    {
        Button BtnAudit, BtnDestroyed;
        ProgressBar progressBar;
        string UserName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainmanager2);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            BtnAudit = FindViewById<Button>(Resource.Id.button1);
            BtnDestroyed = FindViewById<Button>(Resource.Id.button2);
            progressBar.Visibility = ViewStates.Invisible;
            UserName = Intent.GetStringExtra("User") ?? "";

            BtnAudit.Click += GgToBadAuditTable;
            BtnDestroyed.Click += DestroyTable;
            // Create your application here
        }

        private void DestroyTable(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            Intent destroy = new Intent(this, typeof(DestroyTable));
            destroy.PutExtra("User", UserName);
            this.StartActivity(destroy);
        }

        public override void OnBackPressed()
        {

            Intent intent = new Intent(this, typeof(MainManager));
            intent.PutExtra("User", UserName);
            this.StartActivity(intent);

        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        private void GgToBadAuditTable(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            Intent auditproblem = new Intent(this, typeof(AuditPassportsTable));
            auditproblem.PutExtra("User", UserName);
            this.StartActivity(auditproblem);
        }


    }
}