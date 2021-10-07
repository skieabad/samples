using Xamarin.Forms;


namespace Sample
{
    public partial class SendPage : ContentPage
    {
        public SendPage()
        {
            this.InitializeComponent();
            this.BindingContext = new SendViewModel();
        }
    }
}