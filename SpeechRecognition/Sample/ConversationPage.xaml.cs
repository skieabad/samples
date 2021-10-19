using System;
using Xamarin.Forms;


namespace Sample
{
    public partial class ConversationPage : SampleContentPage
    {
        public ConversationPage()
        {
            this.InitializeComponent();
            this.BindingContext = new ConversationViewModel();
        }
    }
}
