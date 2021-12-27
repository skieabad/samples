using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Shiny.BluetoothLE;


namespace Sample
{
    public class ServiceViewModel : SampleViewModel
    {
        public ServiceViewModel(IGattService service)
        {
            this.Title = service.Uuid;

            this.Load = this.LoadingCommand(async () =>
            {
                this.Characteristics = (await service.GetCharacteristicsAsync())
                    .Select(x => new CharacteristicViewModel(x))
                    .ToList();

                this.RaisePropertyChanged(nameof(this.Characteristics));
            });
        }


        public string Title { get; }
        public ICommand Load { get; }
        public List<CharacteristicViewModel> Characteristics { get; private set; }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.Load.Execute(null);
        }
    }
}
