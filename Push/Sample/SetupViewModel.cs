using System;
using System.Windows.Input;
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
#if NATIVE
#endif
                this.Refresh();
            });

            this.UnRegister = this.LoadingCommand(async () =>
            {
                await this.pushManager.UnRegister();
                this.AccessStatus = AccessState.Disabled;
#if NATIVE
#endif
                this.Refresh();
            });
        }


        public ICommand RequestAccess { get; }
        public ICommand UnRegister { get; }

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


        void Refresh()
        {
            //this.UnRegister.ChangeCanExecute();
            this.RegToken = this.pushManager.CurrentRegistrationToken ?? "-";
            this.RegDate = this.pushManager.CurrentRegistrationTokenDate?.ToLocalTime();
        }
    }
}
