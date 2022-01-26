using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
using Org.Json;
using Refit;
using SfGrid_Android;
using System.Collections.ObjectModel;
using System.Linq;
using Com.Toptoche.Searchablespinnerlibrary;
using System.Collections.Generic;
using Syncfusion.Android.ComboBox;
namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class DriversManual : AppCompatActivity
    {
        Spinner spinner;
        Button btnNext;
        private ArrayAdapter<string> adapter;
        public string Username, UserLevel;
        Android.App.ProgressDialog progress;
        SearchableSpinner searchableSpinner;
        
        List<string> Names;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.drivers_manual);

         
            
            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("בטעינה...");
            progress.SetCancelable(false);
            progress.Show();

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            UserLevel = pref.GetString("UserLevel", string.Empty);
            Username = Intent.GetStringExtra("User") ?? "KOBI";

            

            spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            btnNext = FindViewById<Button>(Resource.Id.btnNext);

            var url = RestService.For<RefitApi>("http://hashtildb.pe.hu");
            var cxinfo = await url.GetCompanyAsync("cx_list_json.php");
            ObservableCollection<CxListClass> records = JsonConvert.DeserializeObject<ObservableCollection<CxListClass>>(cxinfo);
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line);

            
            //adapter.AddAll(records);
            foreach (var obj in records)
            {
                //CxListClass CxListClass = new CxListClass();
                adapter.Add(obj.Name);
                
                

            }
            
            spinner.Adapter = adapter;
            progress.Hide();
          
            

        }
        public override void OnBackPressed()
        {
            Intent mainmngr = new Intent(this, typeof(DriversMain));
            mainmngr.PutExtra("User", Username.Trim());
            this.StartActivity(mainmngr);
        }
    }
}