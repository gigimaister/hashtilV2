using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Widget;
using System;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class AfterToken : Activity
    {
        private Button Back;
        Android.App.ProgressDialog progress;
        string UserName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.after_token);


            UserName = Intent.GetStringExtra("User") ?? "PROBLEM";
            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("בטעינה...");
            progress.SetCancelable(false);

            Back = FindViewById<Button>(Resource.Id.btnback);
            Back.Click += Back_Click;
            // Create your application here
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainManager));
            intent.PutExtra("User", UserName);
            this.StartActivity(intent);
            progress.Hide();
        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        public override void OnBackPressed() { }

        protected override void OnResume()
        {
            base.OnResume();

        }
    }
}