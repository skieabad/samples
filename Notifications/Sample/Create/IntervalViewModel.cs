using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;


namespace Sample.Create
{
    public class IntervalViewModel : SampleViewModel
    {
        public IntervalViewModel()
        {
            this.Use = new Command(() =>
            {
                //this.Navigation.PopAsync()
            });
            
            this.Days = Enum
                .GetNames(typeof(DayOfWeek))
                .Select((name, index) => (name, index))
                .ToArray();
        }


        public ICommand Use { get; }

        public int? SelectedDay { get; set; }
        public (string Text, int Value)[] Days { get; }

        public int TimeOfDayHour { get; set; } = 8;
        public int TimeOfDayMinutes { get; set; } = 0;
    }
}
