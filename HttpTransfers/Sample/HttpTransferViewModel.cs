using System.Windows.Input;


namespace Sample
{
    public class HttpTransferViewModel : Shiny.NotifyPropertyChanged
    {
        string id;
        public string Identifier
        {
            get => this.id;
            set => this.Set(ref this.id, value);
        }


        bool upload;
        public bool IsUpload
        {
            get => this.upload;
            set => this.Set(ref this.upload, value);
        }


        string status;
        public string Status
        {
            get => this.status;
            set => this.Set(ref this.status, value);
        }


        string uri;
        public string Uri
        {
            get => this.uri;
            set => this.Set(ref this.uri, value);
        }


        string percent;
        public string PercentCompleteText
        {
            get => this.percent;
            set => this.Set(ref this.percent, value);
        }


        double pcomp;
        public double PercentComplete
        {
            get => this.pcomp;
            set => this.Set(ref this.pcomp, value);
        }


        string xfersp;
        public string TransferSpeed
        {
            get => this.xfersp;
            set => this.Set(ref this.xfersp, value);
        }


        string estTime;
        public string EstimateTimeRemaining
        {
            get => this.estTime;
            set => this.Set(ref this.estTime, value);
        }


        public ICommand Cancel { get; set; }
    }
}
