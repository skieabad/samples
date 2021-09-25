namespace Sample
{
    public abstract class ViewModel : Shiny.NotifyPropertyChanged
    {
        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }
    }
}
