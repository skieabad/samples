using System;
using System.Windows.Input;
using Shiny.BluetoothLE;


namespace Sample.Standard
{
    public class CharacteristicsViewModel : SampleViewModel
    {
        public CharacteristicsViewModel(IGattService service)
        {
            this.Load = this.LoadingCommand(async () =>
            {
                await service.GetCharacteristicsAsync();
            });
        }


        public ICommand Load { get; }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.Load.Execute(null);
        }
    }
}
