using Shiny;
using System;
using System.Reactive.Linq;


namespace Sample
{
    public class BasicViewModel : ViewModel
    {
        readonly IAppSettings appSettings;


        public BasicViewModel()
        {
            this.appSettings = ShinyHost.Resolve<IAppSettings>();
        }


        bool isChecked;
        public bool IsChecked
        {
            get => this.isChecked;
            set => this.Set(ref this.isChecked, value);
        }

        string yourText;
        public string YourText
        {
            get => this.yourText;
            set => this.Set(ref this.yourText, value);
        }


        DateTime? lastUpdated;
        public DateTime? LastUpdated
        {
            get => this.lastUpdated;
            set => this.Set(ref this.lastUpdated, value);
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.IsChecked = this.appSettings.IsChecked;
            this.YourText = this.appSettings.YourText;
            this.LastUpdated = this.appSettings.LastUpdated;

            //this.appSettings
            //    .WhenAnyValue(x => x.LastUpdated)
            //    .Subscribe(x => this.LastUpdated = x)
            //    .DisposeWith(this.DeactivateWith);

            //this.WhenAnyValue(
            //    x => x.IsChecked,
            //    x => x.YourText
            //)
            //.Subscribe(_ =>
            //{
            //    this.appSettings.IsChecked = this.IsChecked;
            //    this.appSettings.YourText = this.YourText;
            //})
            //.DisposeWith(this.DeactivateWith);
        }
    }
}
