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
using System;
using System.Collections.ObjectModel;
using System.Net;
using Orientation = Android.Widget.Orientation;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class AuditPassportsToCheckTable : AppCompatActivity
    {
        SfDataGrid dataGrid;
        LinearLayout layout;
        Button button;
        private WebClient mClient;
        private Uri mUri;
        string UserName;
        protected override void OnCreate(Bundle bundle)
        {
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

            GridTextColumn product_typeColumn = new GridTextColumn();
            product_typeColumn.MappingName = "Passport";
            //product_typeColumn.Width = 60;

            //GridTextColumn gidul_column = new GridTextColumn();
            // product_typeColumn.MappingName = "Gidul";
            //product_typeColumn.Width = 60;

            GridTextColumn zancolumn = new GridTextColumn();
            zancolumn.MappingName = "Zan";
            //zancolumn.Width = 90;

            GridTextColumn priceColumn = new GridTextColumn();
            priceColumn.MappingName = "Hamama";
            //priceColumn.Width = 60;

            GridTextColumn gamlon = new GridTextColumn();
            gamlon.MappingName = "Gamlon";
            //gamlon.Width = 60;

            GridTextColumn magash = new GridTextColumn();
            magash.MappingName = "Magash";
            //magash.Width = 60;

            dataGrid.Columns.Add(product_typeColumn);
            //dataGrid.Columns.Add(gidul_column);
            dataGrid.Columns.Add(zancolumn);
            dataGrid.Columns.Add(priceColumn);
            dataGrid.Columns.Add(gamlon);
            dataGrid.Columns.Add(magash);

            dataGrid.ColumnSizer = ColumnSizer.Star;

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
        private async void getData()
        {

            var url = RestService.For<RefitApi>("http://hashtildb.pe.hu");
            var orderInfo = await url.GetCompanyAsync("auditpassporttocheckjson.php");
            ObservableCollection<AuditPassportsJsonTable> records = JsonConvert.DeserializeObject<ObservableCollection<AuditPassportsJsonTable>>(orderInfo);
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