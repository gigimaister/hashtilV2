using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Refit;
using Syncfusion.SfDataGrid;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class DriverLoadingReportSearch : AppCompatActivity
    {
        ProgressBar progressBar;
        SfDataGrid dataGrid;
        Button BtnDailyReport, BtnCurrentMonthReport, BtnLastMonthReport;
        string UserName;
        string ReportIdentifier;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.driver_loading_report_search);

            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            BtnDailyReport = this.FindViewById<Button>(Resource.Id.btndayreport);
            BtnCurrentMonthReport = this.FindViewById<Button>(Resource.Id.btncurrentmonth);
            BtnLastMonthReport = this.FindViewById<Button>(Resource.Id.btnlastmonth);

            progressBar.Visibility = ViewStates.Invisible;
            UserName = Intent.GetStringExtra("User") ?? "KOBI";

            BtnDailyReport.Click += BtnDailyReport_Click;
            BtnCurrentMonthReport.Click += BtnCurrentMonthReport_Click;
            BtnLastMonthReport.Click += BtnLastMonthReport_Click;

        }

        private  void BtnLastMonthReport_Click(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            ReportIdentifier = "3";
            Intent intent = new Intent(this, typeof(DriversReportTable));
            intent.PutExtra("User", UserName);
            intent.PutExtra("ReportIdentifier", ReportIdentifier);
            this.StartActivity(intent);

        }

        

        private void BtnCurrentMonthReport_Click(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            ReportIdentifier = "2";
            Intent intent = new Intent(this, typeof(DriversReportTable));
            intent.PutExtra("User", UserName);
            intent.PutExtra("ReportIdentifier", ReportIdentifier);
            this.StartActivity(intent);
        }

        private void BtnDailyReport_Click(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            ReportIdentifier = "1";
            Intent intent = new Intent(this, typeof(DriversReportTable));
            intent.PutExtra("User", UserName);
            intent.PutExtra("ReportIdentifier", ReportIdentifier);
            this.StartActivity(intent);
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(DriversMain));
            intent.PutExtra("User", UserName);
            this.StartActivity(intent);
        }
    }

}