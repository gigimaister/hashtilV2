using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Widget;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace hashtil
{
    [Activity(Label = "MessageChat")]
    public class MessageChat : ListActivity
    {

        string UserName;





        List<string> list = new List<string>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string userName = pref.GetString("User", string.Empty);
            string password = pref.GetString("Password", string.Empty);


            UserName = Intent.GetStringExtra("User") ?? userName;

            try
            {

                MySqlConnection con = new MySqlConnection("Server=185.201.10.94;database=u319907874_hashtil_db;userid=u319907874_kobi_gigi;Password=K5$@F991Sw;charset=utf8;AllowUserVariables=True;old guids=true");
                if (con.State == ConnectionState.Closed)
                {


                    con.Open();


                    string sql = "SELECT * FROM report_table ORDER BY message;";


                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataReader myReader = cmd.ExecuteReader();



                    while (myReader.Read())
                    {
                        list.Add(myReader.GetString(1) + " " + myReader.GetString(2) + "\n" + myReader.GetString(3));
                    }


                    myReader.Close();
                    con.Close();

                }
            }
            catch (MySqlException)
            {

                Toast.MakeText(Application, "משהו השתבש...פנה למנהל", ToastLength.Short).Show();
                Intent intent = new Intent(this, typeof(MainManager));
                intent.PutExtra("User", UserName);
                this.StartActivity(intent);

            }
            catch (SystemException)
            {
                Toast.MakeText(Application, "משהו השתבש...", ToastLength.Long).Show();
                Intent intent = new Intent(this, typeof(MainManager));
                intent.PutExtra("User", UserName);
                this.StartActivity(intent);
            }
            // Create your application here
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.message_chat, list);

            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();

            };


        }
        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = Application.Context.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }
        protected override void OnResume()
        {
            base.OnResume();

        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainManager));
            intent.PutExtra("User", UserName);
            this.StartActivity(intent);
        }
    }
}