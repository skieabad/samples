using System;
using System.Windows.Input;
using Shiny.BluetoothLE;


namespace Sample.Standard
{
    public class CharacteristicViewModel : SampleViewModel
    {
        public CharacteristicViewModel(IGattCharacteristic characteristic)
        {

        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
        }


        public ICommand Read { get; }
        public ICommand Write { get; }
        public ICommand ToggleNotify { get; }


        bool notifying;
        public bool IsNotifying
        {
            get => this.notifying;
            private set => this.Set(ref this.notifying, value);
        }


        string readValue;
        public string ReadValue
        {
            get => this.readValue;
            private set => this.Set(ref this.readValue, value);
        }


        string writeValue;
        public string WriteValue
        {
            get => this.writeValue;
            set => this.Set(ref this.writeValue, value);
        }


        string lastValueTime;
        public string LastValueTime
        {
            get => this.lastValueTime;
            private set => this.Set(ref this.lastValueTime, value);
        }
    }
}
