using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace SfGrid_Android
{
    public class ReportTableInfo : INotifyPropertyChanged
    {
        private string date;
        private string time;
        private string user;
        private string message;


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

        [JsonProperty("Message")]
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
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