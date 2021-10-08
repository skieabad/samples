using System;
using System.Reactive.Linq;


namespace Sample
{
    public interface IAppSettings
    {
        bool IsChecked { get; set; }
        string YourText { get; set; }
        DateTime? LastUpdated { get; set; }
    }


    public class AppSettings : Shiny.NotifyPropertyChanged, IAppSettings
    {
        public AppSettings()
        {
            //this.WhenAnyValue(
            //        x => x.IsChecked,
            //        x => x.YourText
            //    )
            //    .Skip(1)
            //    .Subscribe(_ =>
            //        this.LastUpdated = DateTime.Now
            //    );
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
    }
}
