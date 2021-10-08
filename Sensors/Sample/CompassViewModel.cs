using System;
using Shiny.Sensors;


namespace Sample
{
    public class CompassViewModel : ViewModel
    {
        readonly ICompass? compass;
        IDisposable? sub;


        public CompassViewModel(ICompass? compass = null)
        {
            this.compass = compass;
        }


        double rotation;
        public double Rotation
        {
            get => this.rotation;
            private set => this.Set(ref this.rotation, value);
        }


        double heading;
        public double Heading
        {
            get => this.heading;
            private set => this.Set(ref this.heading, value);
        }


        public override async void OnAppearing()
        {
            base.OnAppearing();
            if (this.compass == null || !this.compass.IsAvailable)
            {
                await this.Alert("Compass is not available");
                return;
            }
            this.sub = this.compass
                .WhenReadingTaken()
                .SubOnMainThread(x =>
                {
                    this.Rotation = 360 - x.MagneticHeading;
                    this.Heading = x.MagneticHeading;
                });
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.sub?.Dispose();
        }
    }
}
