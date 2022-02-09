using System;
using Shiny;
using Shiny.Push;
using Xamarin.Forms;


namespace Sample
{
    public class SetupViewModel : ViewModel
    {
        readonly IPushManager pushManager;


        public SetupViewModel()
        {
            this.pushManager = ShinyHost.Resolve<IPushManager>();

            this.RequestAccess = this.LoadingCommand(async () =>
            {
                var result = await this.pushManager.RequestAccess();
                this.AccessStatus = result.Status;
                this.Refresh();
            });

            this.UnRegister = this.LoadingCommand(async () =>
            {
                await this.pushManager.UnRegister();
                this.AccessStatus = AccessState.Disabled;
                this.Refresh();
            });

            this.Refresh();
        }


        public Command RequestAccess { get; }
        public Command UnRegister { get; }

        public bool IsTagsSupported => this.pushManager.IsTagsSupport();
        public string Implementation => this.pushManager.GetType().FullName;

        string regToken;
        public string RegToken
        {
            get => this.regToken;
            private set => this.Set(ref this.regToken, value);
        }


        DateTime? regDate;
        public DateTime? RegDate
        {
            get => this.regDate;
            private set => this.Set(ref this.regDate, value);
        }


        AccessState access = AccessState.Unknown;
        public AccessState AccessStatus
        {
            get => this.access;
            private set => this.Set(ref this.access, value);
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.Refresh();
        }


        async void Refresh()
        {
            //this.UnRegister.ChangeCanExecute();
            this.RegToken = this.pushManager.CurrentRegistrationToken ?? "-";
            this.RegDate = this.pushManager.CurrentRegistrationTokenDate?.ToLocalTime();
        }
    }
}
