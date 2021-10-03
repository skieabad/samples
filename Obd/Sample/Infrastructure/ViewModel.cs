using Xamarin.Forms;

namespace Sample
{
    public abstract class ViewModel : Shiny.NotifyPropertyChanged
    {
        public INavigation Navigation => App.Current.MainPage.Navigation;
        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }


        bool isBusy;
        public bool IsBusy
        {
            get => this.isBusy;
            set => this.Set(ref this.isBusy, value);
        }
    }
}
