using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Refit;
using SfGrid_Android;
using Syncfusion.SfDataGrid;
using Syncfusion.SfDataGrid.Exporting;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using Orientation = Android.Widget.Orientation;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]

    public class ReportTable : AppCompatActivity
    {
        static readonly string TAG = "MainActivity";

        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }


        SfDataGrid dataGrid;
        LinearLayout layout;
        Button button;
        private WebClient mClient;
        private Uri mUri;
        string UserName;
        Button btn1;

        protected override void OnCreate(Bundle bundle)
        {
            CreateNotificationChannel();
            UserName = Intent.GetStringExtra("User") ?? "";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTk4NTE0QDMxMzcyZTM0MmUzME1tZFFQek1JUUpwWHJDU2JjR3Q5eTErRk1SWmxCNVRMeGJVL052UmNkVjQ9");
            dataGrid = new SfDataGrid(this);
            layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;
            button = new Button(this);
            base.OnCreate(bundle);


            // Set our view from the "main" layout resource    
            //SetContentView(Resource.Layout.moving_passports_inside_greenhouse);


            dataGrid.AutoGenerateColumns = false;

            GridTextColumn idColumn = new GridTextColumn();
            idColumn.MappingName = "Date";
            idColumn.HeaderText = "תאריך";
            //idColumn.Width = 70;

            GridTextColumn brandColumn = new GridTextColumn();
            brandColumn.MappingName = "Time";
            brandColumn.HeaderText = "משתמש";
            //brandColumn.Width = 60;

            GridTextColumn product_typeColumn = new GridTextColumn();
            product_typeColumn.MappingName = "User";
            product_typeColumn.HeaderText = "משתמש";
            //product_typeColumn.Width = 65;

            GridTextColumn priceColumn = new GridTextColumn();
            priceColumn.MappingName = "Message";
            priceColumn.HeaderText = "פרטים";
            priceColumn.Width = 180;



            dataGrid.Columns.Add(idColumn);
            dataGrid.Columns.Add(brandColumn);
            dataGrid.Columns.Add(product_typeColumn);
            dataGrid.Columns.Add(priceColumn);
            dataGrid.ColumnSizer = ColumnSizer.Star;

            btn1 = new Button(BaseContext);
            btn1.Text = "Export To Excel";
            btn1.Click += ExportToExcel_Clicked;

            dataGrid.AllowEditing = false;
            dataGrid.SelectionMode = SelectionMode.Single;
            dataGrid.NavigationMode = NavigationMode.Cell;
            dataGrid.EditTapAction = TapAction.OnTap;

            dataGrid.AllowSwiping = true;
            dataGrid.AllowSorting = true;
            dataGrid.AllowMultiSorting = true;
            dataGrid.AllowTriStateSorting = true;

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
        private void ExportToExcel_Clicked(object sender, EventArgs e)
        {
            DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
            var excelEngine = excelExport.ExportToExcel(this.dataGrid, new DataGridExcelExportingOption() { ExportRowHeight = false, ExportColumnWidth = false, DefaultColumnWidth = 100, DefaultRowHeight = 60 });
            var workbook = excelEngine.Excel.Workbooks[0];
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            workbook.Close();
            excelEngine.Dispose();
            //Save("DataGrid.xlsx", "application/msexcel", stream, dataGrid.Context);
        }

        private async void getData()
        {

            var url = RestService.For<RefitApi>("http://hashtildb.pe.hu");
            var orderInfo = await url.GetCompanyAsync("reporttablejson.php");
            ObservableCollection<ReportTableInfo> records = JsonConvert.DeserializeObject<ObservableCollection<ReportTableInfo>>(orderInfo);
            dataGrid.ItemsSource = records;
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainManager));
            intent.PutExtra("User", UserName);
            this.StartActivity(intent);
        }

    }
}

