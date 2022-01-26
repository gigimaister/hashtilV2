namespace hashtil
{
    public class NewMagash
    {
        private string Passport;
        private string Hamama;
        private string Gamlon;
        private string Date;
        private string User;

        public string date
        {
            get { return Date; }
            set { this.Date = value; }
        }
        public string user
        {
            get { return User; }
            set { this.User = value; }
        }
        public string passport
        {
            get { return Passport; }
            set
            {
                this.Passport = value;
            }
        }
        public string hamama
        {
            get { return Hamama; }
            set
            {
                this.Hamama = value;
            }
        }
        public string gamlon
        {
            get { return Gamlon; }
            set
            {
                this.Gamlon = value;
            }
        }
        public NewMagash(string Date, string User, string Passport, string Hamama, string Gamlon)
        {
            this.date = Date;
            this.user = User;
            this.passport = Passport;
            this.hamama = Hamama;
            this.gamlon = Gamlon;

        }
    }

}