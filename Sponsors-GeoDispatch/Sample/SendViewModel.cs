using System;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shiny;


namespace Sample
{
    public class SendViewModel : ViewModel
    {
        public SendViewModel(IDialogs dialogs)
        {
            this.Send = ReactiveCommand.CreateFromTask(
                async () =>
                {

                },
                this.WhenAny(
                    x => x.Message,
                    x => x.Radius,
                    x => x.Latitude,
                    x => x.Longitude,
                    (msg, rad, lat, lng) =>
                    {
                        if (!IsValidPosition(lat.GetValue(), 180))
                            return false;

                        if (!IsValidPosition(lng.GetValue(), 90))
                            return false;

                        if (!Double.TryParse(rad.GetValue(), out var r) || r <= 0)
                            return false;

                        return true;
                    }
                )
            );
        }


        public ICommand Send { get; }
        [Reactive] public string Message { get; set; }
        [Reactive] public string Latitude { get; set; }
        [Reactive] public string Longitude { get; set; }
        [Reactive] public string Radius { get; set; }


        static bool IsValidPosition(string value, double max)
        {
            if (Double.TryParse(value, out var result))
            {
                var min = max * -1;
                if (result >= min && result <= max)
                    return true;
            }
            return false;
        }
    }
}

