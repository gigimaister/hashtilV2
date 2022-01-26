using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Refit;
using SfGrid_Android;
using Syncfusion.Android.ComboBox;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class DriversComboBox : Activity
    {
        public string Username, UserLevel,choosen_cx,CxId;
        Android.App.ProgressDialog progress;
        private ArrayAdapter<string> adapter;
        
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            UserLevel = pref.GetString("UserLevel", string.Empty);

            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("בטעינה...");
            progress.SetCancelable(false);
            progress.Hide();

            Username = Intent.GetStringExtra("User") ?? "KOBI";
            // Set the layout to display the control
            LinearLayout linearLayout = new LinearLayout(this);
            linearLayout.LayoutParameters = new ViewGroup.LayoutParams(500, ViewGroup.LayoutParams.MatchParent); linearLayout.SetBackgroundColor(Android.Graphics.Color.White);

            // Add the Combobox Control
            SfComboBox comboBox = new SfComboBox(this);
            comboBox.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, 50);
            comboBox.IsEditableMode = true;
            comboBox.AllowFiltering = true;
            comboBox.ComboBoxMode = ComboBoxMode.SuggestAppend;
            comboBox.NoResultsFoundText = "לא נמצאו תוצאות";
            //Add the control in layout to display
            linearLayout.AddView(comboBox);
            SetContentView(linearLayout);

            var url = RestService.For<RefitApi>("http://hashtildb.pe.hu");
            var cxinfo =  await  url.GetCompanyAsync("cx_list_json.php");
            ObservableCollection<CxListClass> records = JsonConvert.DeserializeObject<ObservableCollection<CxListClass>>(cxinfo);

            List<String> cx_names = new List<String>();
            List<CxListClass> kobi = new List<CxListClass>();
            //adapter.AddAll(records);
            foreach (var obj in records)    
            {
                //CxListClass CxListClass = new CxListClass();               
                cx_names.Add(obj.Name);
                kobi.Add(new CxListClass() { Name = obj.Name.ToString(), Id = obj.Id.ToString() });
                

            }
            comboBox.DataSource = cx_names;
            comboBox.ComboBoxMode = ComboBoxMode.Suggest;
            comboBox.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            {
                choosen_cx = e.Value.ToString();

                 foreach(var t in kobi)
                {
                    if(t.Name== choosen_cx)
                    {
                        CxId = t.Id.ToString();
                    }
                }

                //var w = kobi.Where(x => x.Name == choosen_cx).Select(x => x.Id.ToString()).ToString();
                Intent mainmngr = new Intent(this, typeof(AfterScanDrivers));
                mainmngr.PutExtra("User", Username.Trim());
                mainmngr.PutExtra("cx", choosen_cx);
                mainmngr.PutExtra("cx_id", CxId);
               



                this.StartActivity(mainmngr);
            };

            progress.Hide();


            // Create your application here
        }


        public override void OnBackPressed()
        {
            Intent mainmngr = new Intent(this, typeof(DriversMain));
            mainmngr.PutExtra("User", Username.Trim());
            this.StartActivity(mainmngr);
        }
    }
}