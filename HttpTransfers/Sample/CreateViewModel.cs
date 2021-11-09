using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Shiny;
using Shiny.Net.Http;
using Xamarin.Forms;


namespace Sample
{
    public class CreateViewModel : SampleViewModel
    {
        IDisposable? sub;


        public CreateViewModel()
        {
            var platform = ShinyHost.Resolve<IPlatform>();
            var httpTransfers = ShinyHost.Resolve<IHttpTransferManager>();

            this.ManageUploads = new Command(
                async () => this.Navigation.PushAsync(new ManageUploadsPage())
            );

            this.SelectUpload = new Command(async () =>
            {
                var files = platform.AppData.GetFiles("upload.*", SearchOption.TopDirectoryOnly);
                if (!files.Any())
                {
                    await this.Alert("There are not files to upload.  Use 'Manage Uploads' below to create them");
                }
                else
                {
                    var cfg = new Dictionary<string, Action>();
                    foreach (var file in files)
                        cfg.Add(file.Name, () => this.FileName = file.Name);

                    //await dialogs.ActionSheet("Actions", cfg);
                }
            });

            this.Save = new Command(async () =>
            {
                this.ErrorMessage = "";
                if (this.FileName.IsEmpty())
                {
                    this.ErrorMessage = "Enter a filename";
                    return;
                }
                if (!Uri.TryCreate(this.Url, UriKind.Absolute, out var uri))
                {
                    this.ErrorMessage = "Please enter a valid URI";
                    return;
                }
                var path = Path.Combine(platform.AppData.FullName, this.FileName);
                var request = new HttpTransferRequest(this.Url, path, this.IsUpload)
                {
                    UseMeteredConnection = this.UseMeteredConnection
                };
                await httpTransfers.Enqueue(request);

                await this.Navigation.PopAsync();
            });
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.sub = this.WhenAnyProperty(x => x.IsUpload).Subscribe(upload =>
            {
                if (!upload && this.FileName.IsEmpty())
                    this.FileName = Guid.NewGuid().ToString();

                this.Title = upload ? "New Upload" : "New Download";
            });
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.sub?.Dispose();
        }


        public ICommand Save { get; }
        public ICommand SelectUpload { get; }
        public ICommand ManageUploads { get; }


        string title;
        public string Title
        {
            get => this.title;
            set => this.Set(ref this.title, value);
        }


        string errMsg;
        public string ErrorMessage
        {
            get => this.errMsg;
            private set => this.Set(ref this.errMsg, value);
        }


        string url;
        public string Url
        {
            get => this.url;
            set => this.Set(ref this.url, value);
        }


        bool meterConn;
        public bool UseMeteredConnection
        {
            get => this.meterConn;
            set => this.Set(ref this.meterConn, value);
        }


        bool upload;
        public bool IsUpload
        {
            get => this.upload;
            set => this.Set(ref this.upload, value);
        }


        string fileName;
        public string FileName
        {
            get => this.fileName;
            set => this.Set(ref this.fileName, value);
        }
    }
}
