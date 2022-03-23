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
            this.Use = new Command(async () =>
            {
                var trigger = new Shiny.Notifications.IntervalTrigger
                {
                    TimeOfDay = new TimeSpan(0, this.TimeOfDayHour, this.TimeOfDayMinutes, 0)
                };
                if (this.SelectedDay != null)
                    trigger.DayOfWeek = (DayOfWeek)this.SelectedDay.Value.Value;
                
                State.CurrentNotification!.RepeatInterval = trigger;
                State.CurrentNotification!.Geofence = null;
                State.CurrentNotification!.ScheduleDate = null;
                
                await this.Navigation.PopModalAsync();
            });
            
            this.Days = Enum
                .GetNames(typeof(DayOfWeek))
                .Select((name, index) => (name, index))
                .ToArray();
        }


        public ICommand Use { get; }

        public (string Text, int Value)? SelectedDay { get; set; }
        public (string Text, int Value)[] Days { get; }

        public int TimeOfDayHour { get; set; } = 8;
        public int TimeOfDayMinutes { get; set; } = 0;
    }
}
