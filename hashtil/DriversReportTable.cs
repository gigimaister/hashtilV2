using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Microsoft.Appcenter;
using Newtonsoft.Json;
using Refit;
using Syncfusion.Data;
using Syncfusion.SfDataGrid;
using Orientation = Android.Widget.Orientation;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class DriversReportTable : AppCompatActivity
    {
        SfDataGrid dataGrid;
        LinearLayout layout;
        Button button;  
        string UserName, reportIdentifier;
      

        protected override void OnCreate(Bundle bundle)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQ3MTcxQDMxMzgyZTMzMmUzMFFKaE1kT3ZaczFPYUFYTDByT1NTSTBPb01MMmFLTC94a2ZrZG1GOGdha0E9");

            reportIdentifier = Intent.GetStringExtra("ReportIdentifier") ?? "2";
            UserName = Intent.GetStringExtra("User") ?? "";
            dataGrid = new SfDataGrid(this);
            layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;
            button = new Button(this);
            base.OnCreate(bundle);


            // Set our view from the "main" layout resource    
            //SetContentView(Resource.Layout.moving_passports_inside_greenhouse);


            dataGrid.AutoGenerateColumns = false;

            GridTextColumn date = new GridTextColumn();
            date.MappingName = "Date";
            date.HeaderText = "תאריך";
            date.Width = 100;

            GridTextColumn time = new GridTextColumn();
            time.MappingName = "Time";
            time.HeaderText = "שעה";
            time.Width = 80;

            GridTextColumn user = new GridTextColumn();
            user.MappingName = "User";
            user.HeaderText = "נהג";
            user.Width = 100;

            GridTextColumn cx = new GridTextColumn();
            cx.MappingName = "Cx";
            cx.HeaderText = "לקוח";
            cx.Width = 80;

            GridTextColumn status = new GridTextColumn();
            status.MappingName = "Status";
            status.HeaderText = "מצב";
            status.Width = 80;

            GridNumericColumn numOfCage = new GridNumericColumn();
            numOfCage.MappingName = "NumOfCage";
            numOfCage.HeaderText = "מס כלובים";
            numOfCage.Width = 80;
            numOfCage.NumberDecimalDigits = 0;
            numOfCage.TextAlignment= (GravityFlags)17;

            GridTableSummaryRow summaryRow1 = new GridTableSummaryRow();
            summaryRow1.Title = "סהכ כלובים: {TotalSalary}";
            summaryRow1.ShowSummaryInRow = true;
            summaryRow1.Position = Position.Top;
            summaryRow1.SummaryColumns.Add(new GridSummaryColumn()
            {
                Name = "TotalSalary",
                MappingName = "NumOfCage",
                Format = "{Sum}",
                SummaryType = SummaryType.Int32Aggregate
            });
            dataGrid.TableSummaryRows.Add(summaryRow1);

            dataGrid.Columns.Add(date);
            dataGrid.Columns.Add(time);
            dataGrid.Columns.Add(user);
            dataGrid.Columns.Add(cx);
            dataGrid.Columns.Add(status);
            dataGrid.Columns.Add(numOfCage);

           


            dataGrid.AllowEditing = false;
            dataGrid.SelectionMode = SelectionMode.Single;
            dataGrid.NavigationMode = NavigationMode.Cell;
            dataGrid.EditTapAction = TapAction.OnTap;

            dataGrid.AllowSwiping = true;
            dataGrid.AllowSorting = true;
            dataGrid.AllowMultiSorting = true;
            dataGrid.AllowTriStateSorting = true;
           // dataGrid.CanUseViewFilter = true;

            SwipeView leftSwipeView = new SwipeView(BaseContext);
            SwipeView rightSwipeView = new SwipeView(BaseContext);
            LinearLayout editView = new LinearLayout(BaseContext);
            LinearLayout deleteView = new LinearLayout(BaseContext);


            TextView edit = new TextView(BaseContext);
            edit.Text = "עריכה";
            edit.SetTextColor(Color.White);
            edit.SetBackgroundColor(Color.ParseColor("#009EDA"));

            TextView delete = new TextView(BaseContext);
            delete.Text = "מחיקה";
            delete.SetTextColor(Color.White);
            delete.Gravity = GravityFlags.Center;
            delete.SetBackgroundColor(Color.ParseColor("#DC595F"));


            editView.AddView(edit, ViewGroup.LayoutParams.MatchParent, (int)dataGrid.RowHeight);

            deleteView.AddView(delete, ViewGroup.LayoutParams.MatchParent, (int)dataGrid.RowHeight);

            leftSwipeView.AddView(editView, dataGrid.MaxSwipeOffset, (int)dataGrid.RowHeight);
            rightSwipeView.AddView(deleteView, dataGrid.MaxSwipeOffset, (int)dataGrid.RowHeight);

            dataGrid.LeftSwipeView = leftSwipeView;
            dataGrid.RightSwipeView = rightSwipeView;

            


            layout.AddView(dataGrid, ViewGroup.LayoutParams.MatchParent);
           
            getData();
           
            
            SetContentView(layout);
        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
       

        private async void getData()
        {
            try
            {

                var url = RestService.For<SfGrid_Android.GetDriversRecord>("http://hashtildb.pe.hu");
                var orderInfo = await url.GetDriverAsync(UserName, reportIdentifier);
                ObservableCollection<DriversUpLoadingReportModel> records = JsonConvert.DeserializeObject<ObservableCollection<DriversUpLoadingReportModel>>(orderInfo);
                dataGrid.ItemsSource = records;
            }
            catch(Exception)
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("אין נתונים");
                alert.SetMessage("לא נמצאו רשומות");
                alert.SetButton("אוקיי", (c, ev) =>
                {
                    Intent intent = new Intent(this, typeof(DriversMain));
                    intent.PutExtra("User", UserName);
                    this.StartActivity(intent);
                });
                alert.Show();
            }

        }

        

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(DriverLoadingReportSearch));
            intent.PutExtra("User", UserName);
            this.StartActivity(intent);
        }

    }
}
