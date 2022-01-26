using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace hashtil
{
    class DriversUpLoadingReportModel : INotifyPropertyChanged
    {
        private string date;
        private string time;
        private string user;
        private string cx;
        private string status;
        private int numOfcage;

        
       [JsonProperty("Date")]
        public string Date 
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
                RaisePropertyChanged("Date");
            }
        }


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
        [JsonProperty("User")]
        public string User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
                RaisePropertyChanged("User");
            }
        }
        [JsonProperty("Cx")]
        public string Cx
        {
            get
            {
                return cx;
            }

            set
            {
                cx = value;
                RaisePropertyChanged("Cx");
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
        [JsonProperty("NumOfCage")]
        public int NumOfCage
        {
            get
            {
                return numOfcage;
            }

            set
            {
                numOfcage = value;
                RaisePropertyChanged("NumOfCage");
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