using System;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using Sample.Infrastructure;
using Shiny;
using Shiny.Push;


namespace Sample
{
    public class SetupViewModel : ViewModel
    {
        readonly IPushManager pushManager;


        public SetupViewModel()
        {
            this.pushManager = ShinyHost.Resolve<IPushManager>();
            var config = ShinyHost.Resolve<IConfiguration>();

            this.RequestAccess = this.LoadingCommand(async () =>
            {
                var result = await this.pushManager.RequestAccess();
                this.AccessStatus = result.Status;
                this.Refresh();
#if NATIVE
                if (this.AccessStatus == AccessState.Available)
                    await SampleApi.Current.Register(result.RegistrationToken!);
#endif
            });

            this.UnRegister = this.LoadingCommand(async () =>
            {
                var deviceToken = this.pushManager.CurrentRegistrationToken;
                await this.pushManager.UnRegister();
                this.AccessStatus = AccessState.Disabled;
                this.Refresh();
#if NATIVE
                await SampleApi.Current.UnRegister(deviceToken!);
#endif
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
            this.RegToken = this.pushManager.CurrentRegistrationToken ?? "-";
            this.RegDate = this.pushManager.CurrentRegistrationTokenDate?.ToLocalTime();
        }
    }
}
