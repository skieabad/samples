using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace Sample
{
    public abstract class SampleViewModel : Shiny.NotifyPropertyChanged
    {
        protected Page MainPage => Application.Current.MainPage;
        public INavigation Navigation => this.MainPage.Navigation;

        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }


        protected virtual Command LoadingCommand(Func<Task> taskFunc) => new Command(() => this.LoadingCommand(taskFunc));

        protected virtual async Task Loading(Func<Task> taskFunc)
        {

            this.IsBusy = true;
            try
            {
                await taskFunc.Invoke();
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


        protected virtual ICommand NavigateCommand<TPage>() where TPage: Page, new() => this.NavigateCommand(() => new TPage());
        protected virtual ICommand NavigateCommand(Func<Page> getPage) => new Command(async () =>
            await this.Navigation.PushAsync(getPage())
        );

        protected virtual ICommand ConfirmCommand(string question, Func<Task> taskFunc) => new Command(async () =>
        {
            var result = await this.Confirm(question);
            if (result)
                await taskFunc();
        });


        protected virtual Task Alert(string message, string title = "Info")
            => this.MainPage.DisplayAlert(title, message, "OK");

        protected virtual Task<bool> Confirm(string question, string title = "Question")
            => this.MainPage.DisplayAlert(title, question, "Ok", "Cancel");

        protected virtual Task<string> Prompt(string question)
            => this.MainPage.DisplayPromptAsync("Question", question);

        protected virtual Task<string> Choose(string title, params string[] choices)
            => this.MainPage.DisplayActionSheet(title, null, null, choices);


        bool isBusy;
        public bool IsBusy
        {
            get => this.isBusy;
            set => this.Set(ref this.isBusy, value);
        }
    }
}
