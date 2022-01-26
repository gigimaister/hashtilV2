using Newtonsoft.Json;
using SfGrid_Android;
using System;
using System.ComponentModel;

namespace hashtil
{
    class AuditPassportsJsonTable : ReportTbleInfo, INotifyPropertyChanged
    {


        private string status;
        private string remarks;
        private string time;





        [JsonProperty("Time")]
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                RaisePropertyChanged("Time");
            }
        }



        [JsonProperty("Status")]
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                RaisePropertyChanged("Status");
            }
        }



        [JsonProperty("Remarks")]
        public string Remarks
        {
            get
            {
                return remarks;
            }
            set
            {
                remarks = value;
                RaisePropertyChanged("Remarks");
            }


        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String Name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

    }
}