using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;

namespace hashtil
{
    class NewMagashRepository
    {
        private WebClient mClient;
        private Uri mUri;
        ObservableCollection<NewMagash> mNewMovingInside = new ObservableCollection<NewMagash>();


        private ObservableCollection<NewMagash> newMagashInfo;
        public ObservableCollection<NewMagash> NewMagashInfoCollection
        {
            get { return newMagashInfo; }
            set { this.newMagashInfo = value; }
        }

        public NewMagashRepository()
        {
            newMagashInfo = new ObservableCollection<NewMagash>();
            this.GenerateOrders();
        }

        private void GenerateOrders()
        {
            mClient = new WebClient();
            mUri = new Uri("http://hashtildb.pe.hu/newpassportsjson.php");
            mClient.DownloadDataAsync(mUri);
            mClient.DownloadDataCompleted += mClient_DownloadDataCompleted;

        }

        private void mClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {

            string json = Encoding.UTF8.GetString(e.Result);
            mNewMovingInside = JsonConvert.DeserializeObject<ObservableCollection<NewMagash>>(json);
            //movingInsideInfo = mMovingInside;
            //movingInsideInfo.Add(new MovingInsideInfo( "Maria Anders", "Germany", "ALFKI"));
            foreach (var item in mNewMovingInside)
            {
                newMagashInfo.Add(item);
            }



        }
    }
}