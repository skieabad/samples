using System;
using ReactiveUI;
using ReactiveUI.Fody;


namespace Sample
{
    public class SendViewModel : Shiny.ViewModel
    {
        public SendViewModel()
        {
            this.Send = ReactiveCommand.CreateFromTask(async () =>
            {

            });
        }


        public ICommand Send { get; }
        [Reactive] public string Message { get; set; }
        [Reactive] public string Latitude { get; set; }
        [Reactive] public string Longitude { get; set; }
        [Reactive] public double Radius { get; set; }
    }
}

