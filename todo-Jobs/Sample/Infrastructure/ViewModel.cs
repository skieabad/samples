using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;


namespace Sample
{
    public abstract class ViewModel : Shiny.NotifyPropertyChanged
    {
        public INavigation Navigation => App.Current.MainPage.Navigation;
        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }


        protected virtual Command LoadingCommand(Func<Task> taskFunc) => new Command(() => this.LoadingCommand(taskFunc));

        protected virtual async Task Loading(Func<Task> taskFunc)
        {
            this.IsBusy = true;
            try
            {
                await taskFunc.Invoke().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                await this.Alert("Error");
            }
            finally
            {
                this.IsBusy = false;
            }
        }


        protected virtual ICommand ConfirmCommand(string question, Func<Task> taskFunc) => new Command(async () =>
        {
            var result = await this.Confirm(question);
            if (result)
                await taskFunc();
        });


        protected virtual Task<string> Choose(string title, params string[] choices)
            => App.Current.MainPage.DisplayActionSheet(title, null, null, choices);

        protected virtual Task Alert(string message, string title = "Info")
            => App.Current.MainPage.DisplayAlert(title, message, "OK");

        protected virtual Task<bool> Confirm(string question, string title = "Question")
            => App.Current.MainPage.DisplayAlert(title, question, "Ok", "Cancel");

        protected virtual Task<string> Prompt(string question)
            => App.Current.MainPage.DisplayPromptAsync("Question", question);


        bool isBusy;
        public bool IsBusy
        {
            get => this.isBusy;
            set => this.Set(ref this.isBusy, value);
        }
    }
}
