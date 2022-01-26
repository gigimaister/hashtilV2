using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class AfterScanDrivers : AppCompatActivity
    {

        public TextView scantxt;
        public ProgressBar progressBar;
        ImageButton Upload, Download;
        public string ScanResult, usrname,cx_id;
        public string UpDownRecognizer;
        public TextView driverscantxt;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQ3MTY2QDMxMzgyZTMzMmUzMFFKaE1kT3ZaczFPYUFYTDByT1NTSTBPb01MMmFLTC94a2ZrZG1GOGdha0E9");

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.after_scan_drivers);
            
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            Upload = FindViewById<ImageButton>(Resource.Id.loadingtruck);
            Download = FindViewById<ImageButton>(Resource.Id.unloadingtruck);
            driverscantxt = FindViewById<TextView>(Resource.Id.cx_scan_text);
            progressBar.Visibility = ViewStates.Invisible;
            ScanResult = Intent.GetStringExtra("cx");
            usrname = Intent.GetStringExtra("User");
            cx_id = Intent.GetStringExtra("cx_id");

            driverscantxt.Text = ScanResult;

            Upload.Click += Upload_Click;
            Download.Click += Download_Click;

        }

        public void Download_Click(object sender, EventArgs e)
        {
            if (ScanResult.Length < 4)
            {
                Intent err2 = new Intent(this, typeof(AfterScanError));
                err2.PutExtra("User", usrname);

                this.StartActivity(err2);
            }
            else
            {
                progressBar.Visibility = ViewStates.Visible;
                UpDownRecognizer = "0";
                Intent intent = new Intent(this, typeof(SelectCageForDriver));
                intent.PutExtra("User", usrname);
                intent.PutExtra("cx", ScanResult);
                intent.PutExtra("updownrecognizer", UpDownRecognizer);
                intent.PutExtra("cx_id", cx_id);

                this.StartActivity(intent);
                progressBar.Visibility = ViewStates.Invisible;
            }
        }

        public void Upload_Click(object sender, EventArgs e)
        {
            if (ScanResult.Length < 4)
            {
                Intent err2 = new Intent(this, typeof(AfterScanError));
                err2.PutExtra("User", usrname);

                this.StartActivity(err2);
            }
            else
            {
                progressBar.Visibility = ViewStates.Visible;
                UpDownRecognizer = "1";
                Intent intent = new Intent(this, typeof(SelectCageForDriver));
                intent.PutExtra("User", usrname);
                intent.PutExtra("cx", ScanResult);
                intent.PutExtra("updownrecognizer", UpDownRecognizer);
                intent.PutExtra("cx_id", cx_id.ToString());

                this.StartActivity(intent);
                progressBar.Visibility = ViewStates.Invisible;
            }
        }
    }
}