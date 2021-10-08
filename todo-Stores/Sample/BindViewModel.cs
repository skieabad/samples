using System;
using System.Reactive.Linq;

using Shiny;
using Shiny.Stores;


namespace Sample
{
    public class BindViewModel : ViewModel
    {
        readonly IObjectStoreBinder binder;


        public BindViewModel()
        {
            this.binder = ShinyHost.Resolve<IObjectStoreBinder>();
            //this.WhenAnyValue(
            //    x => x.YourText,
            //    x => x.IsChecked
            //)
            //.Skip(1)
            //.Subscribe(_ => this.LastUpdated = DateTime.Now);
        }


        [Reactive] public bool IsChecked { get; set; }
        [Reactive] public string YourText { get; set; }
        [Reactive] public DateTime? LastUpdated { get; set; }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.binder.Bind(this, "settings");
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.binder.UnBind(this);
        }
    }
}
