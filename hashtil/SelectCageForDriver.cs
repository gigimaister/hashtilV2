using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Syncfusion.Licensing;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Xamarin.Essentials;

namespace hashtil
{
    [Activity(Theme = "@style/Theme.AppCompat.DayNight.NoActionBar")]
    public class SelectCageForDriver : AppCompatActivity
    {
        EditText NumOfCage;
        Button SendCageNum;
        int new_NumOfCage;
        LocationRequest locationRequest;
        FusedLocationProviderClient fusedLocationProviderClient;
        public double Lon, Lan;
        private LocationManager lm;
        private string bestProvider;
        private object tvLat;
        private ProgressBar progressBar;
        public string Username, UserLevel, ScanResult,UpdownRecognizer,cx_id;
        int Preventduplicate = 0;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.select_cage_for_drivers);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            progressBar.Visibility = ViewStates.Invisible;

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {

                    Lon = location.Longitude;
                    Lan = location.Latitude;
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("שגיאה");
                alert.SetMessage("מכשיר לא נתמך");
                alert.SetButton("אוקיי", (c, ev) =>
                {
                    Intent intent = new Intent(this, typeof(DriversMain));
                    intent.PutExtra("User", Username);

                    this.StartActivity(intent);
                });
                alert.Show();
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("שגיאה");
                alert.SetMessage("יש לאפשר שירותי מיקום");
                alert.SetButton("אוקיי", (c, ev) =>
                {
                    Intent intent = new Intent(this, typeof(DriversMain));
                    intent.PutExtra("User", Username);

                    this.StartActivity(intent);
                });
                alert.Show();
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("שגיאה");
                alert.SetMessage("חובה לתת הרשאה למיקום");
                alert.SetButton("אוקיי", (c, ev) =>
                {
                    Intent intent = new Intent(this, typeof(DriversMain));
                    intent.PutExtra("User", Username);

                    this.StartActivity(intent);
                });
                alert.Show();
            }
            catch (Exception)
            {
                // Unable to get location
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("שגיאה");
                alert.SetMessage("המערכת לא מצליחה לאתר מיקום");
                alert.SetButton("אוקיי", (c, ev) =>
                {
                    Intent intent = new Intent(this, typeof(DriversMain));
                    intent.PutExtra("User", Username);

                    this.StartActivity(intent);
                });
                alert.Show();
            }




            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
           
            NumOfCage = FindViewById<EditText>(Resource.Id.u_numOfcage);
            SendCageNum = FindViewById<Button>(Resource.Id.button1);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            UserLevel = pref.GetString("UserLevel", string.Empty);
            Username = Intent.GetStringExtra("User");
            ScanResult = Intent.GetStringExtra("cx");
            cx_id = Intent.GetStringExtra("cx_id");
            UpdownRecognizer = Intent.GetStringExtra("updownrecognizer");
            



            SendCageNum.Click += SendCageNum_Click;


            // Create your application here
        }


      


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void SendCageNum_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt16(NumOfCage.Text);

                //int x = (int)NumOfCage;
                if (x > 18 || x < 1)
                {
                    Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                    Android.App.AlertDialog alert = dialog.Create();
                    alert.SetTitle("שגיאה");
                    alert.SetMessage("יש להזין מס בין 1-18");
                    alert.SetButton("אוקיי", (c, ev) =>
                    {
                        // Ok button click task  
                    });
                    alert.Show();
                }
                else
                {

                    try
                    {
                        Preventduplicate ++ ;
                        if (Preventduplicate > 1)
                        {
                            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                            Android.App.AlertDialog alert = dialog.Create();
                            alert.SetTitle("אזהרת כפילות!");
                            alert.SetMessage("בוצעה שליחה-יש להמתין בסבלנות!");
                            alert.SetButton("אוקיי", (c, ev) =>
                            {
                                // Ok button click task  
                            });
                            alert.Show();
                        }
                        else
                        {
                            progressBar.Visibility = ViewStates.Visible;
                            WebClient client = new WebClient();
                            Uri uri = new Uri("http://hashtildb.pe.hu/add_cx_cx.php");
                            NameValueCollection parameters = new NameValueCollection();
                            parameters.Add("userlevel", UserLevel);
                            parameters.Add("username", Username);
                            parameters.Add("cx", ScanResult);
                            parameters.Add("cx_id", cx_id);
                            parameters.Add("status", UpdownRecognizer);
                            parameters.Add("num_of_cage", x.ToString());
                            parameters.Add("Lon", Lon.ToString());
                            parameters.Add("Lan", Lan.ToString());

                            client.UploadValuesCompleted += Client_UploadValuesCompleted;
                            client.UploadValuesAsync(uri, parameters);
                        }
                    }
                    catch (SystemException)
                    {
                        Intent intent = new Intent(this, typeof(AfterScanError));
                        intent.PutExtra("User", Username);

                        this.StartActivity(intent);

                    }

                }

            }
            catch (SystemException)
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("שגיאה");
                alert.SetMessage("יש להזין ספרות בלבד");
                alert.SetButton("אוקיי", (c, ev) =>
                {
                    // Ok button click task  
                });
                alert.Show();
            }
        }

        public void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                try
                {

                   
                    string id = "";
                    id = Encoding.UTF8.GetString(e.Result);
                   // int newId = 0;

                   // int.TryParse(id, out newId);

                    switch (id)
                    {

                        case "0":

                            Intent main = new Intent(this, typeof(DriversMain));
                            main.PutExtra("User", Username);
                            this.StartActivity(main);
                            progressBar.Visibility = ViewStates.Invisible;
                            break;


                        case "1":
                            Intent intent = new Intent(this, typeof(AfterScanError));
                            intent.PutExtra("User", Username);
                            this.StartActivity(intent);
                            progressBar.Visibility = ViewStates.Invisible;
                            break;

                        default:

                            Intent error = new Intent(this, typeof(AfterScanError));
                            error.PutExtra("User", Username);
                            this.StartActivity(error);
                            progressBar.Visibility = ViewStates.Invisible;
                            break;
                    }

                }

                catch (System.Reflection.TargetInvocationException)
                {
                    Intent error = new Intent(this, typeof(AfterScanError));
                    error.PutExtra("User", Username);
                    this.StartActivity(error);
                    progressBar.Visibility = ViewStates.Invisible;

                }
            });
        }
    }
}