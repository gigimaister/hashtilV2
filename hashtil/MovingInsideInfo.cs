using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace SfGrid_Android
{
    public class ReportTbleInfo : INotifyPropertyChanged
    {
        private string date;
        private string user;
        private string passport;
        private string hamama;
        private string gamlon;
        private string zan;
        private string magash;
        private string gidul;


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

        [JsonProperty("Passport")]
        public string Passport
        {
            get
            {
                return passport;
            }
            set
            {
                passport = value;
                RaisePropertyChanged("Passport");
            }
        }

        [JsonProperty("Hamama")]
        public string Hamama
        {
            get
            {
                return hamama;
            }
            set
            {
                hamama = value;
                RaisePropertyChanged("Hamama");
            }
        }
        [JsonProperty("Gamlon")]
        public string Gamlon
        {
            get
            {
                return gamlon;
            }
            set
            {
                gamlon = value;
                RaisePropertyChanged("Gamlon");
            }
        }

        [JsonProperty("Gidul")]
        public string Gidul
        {
            get
            {
                return gidul;
            }
            set
            {
                gidul = value;
                RaisePropertyChanged("Gidul");
            }
        }
        [JsonProperty("Zan")]
        public string Zan
        {
            get
            {
                return zan;
            }
            set
            {
                zan = value;
                RaisePropertyChanged("Zan");
            }
        }

        [JsonProperty("Magash")]
        public string Magash
        {
            get
            {
                return magash;
            }
            set
            {
                magash = value;
                RaisePropertyChanged("Magash");
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