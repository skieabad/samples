using System;
using System.Windows.Input;
using Xamarin.Forms;


namespace Sample
{
    public class SendViewModel : ViewModel
    {
        public SendViewModel()
        {
            this.Send = new Command(() =>
            {
                this.IsBusy = true;
                this.IsBusy = false;
            });
        }


        public ICommand Send { get; }


        string notificationTitle;
        public string NotificationTitle
        {
            get => this.notificationTitle;
            set => this.Set(ref this.notificationTitle, value);
        }


        string notificationMsg;
        public string NotificationMessage
        {
            get => this.notificationMsg;
            set => this.Set(ref this.notificationMsg, value);
        }


        string tag;
        public string Tag
        {
            get => this.tag;
            set => this.Set(ref this.tag, value);
        }


        string propertyName;
        public string PropertyName
        {
            get => this.propertyName;
            set => this.Set(ref this.propertyName, value);
        }


        string propertyValue;
        public string PropertyValue
        {
            get => this.propertyValue;
            set => this.Set(ref this.propertyValue, value);
        }
    }
}
