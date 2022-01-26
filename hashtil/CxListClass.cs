using Newtonsoft.Json;
using System.ComponentModel;

namespace hashtil
{
    class CxListClass : INotifyPropertyChanged
    {
        private string name;
        private string id;

        [JsonProperty("Name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }

        }
        [JsonProperty("Id")]
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        private void RaisePropertyChanged(string v)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}