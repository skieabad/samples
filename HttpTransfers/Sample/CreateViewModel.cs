using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Shiny;
using Shiny.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Sample
{
    public class CreateViewModel : SampleViewModel
    {
        const string RANDOM_FILE_NAME = "upload.random";
        readonly IPlatform platform;
        IDisposable? sub;


        public CreateViewModel()
        {
            this.Url = "https://acrmonster:44378/upload";

            this.platform = ShinyHost.Resolve<IPlatform>();
            var httpTransfers = ShinyHost.Resolve<IHttpTransferManager>();

            this.SelectUpload = new Command(async () =>
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a file to upload"
                });
                if (result != null)
                    this.FilePath = result.FullPath;
            });

            this.Save = this.LoadingCommand(async () =>
            {
                this.ErrorMessage = "";
                if (this.FilePath.IsEmpty())
                {
                    this.ErrorMessage = "Enter a filename";
                    return;
                }
                if (!Uri.TryCreate(this.Url, UriKind.Absolute, out var uri))
                {
                    this.ErrorMessage = "Please enter a valid URI";
                    return;
                }
                if (this.IsUpload && !File.Exists(this.FilePath))
                {
                    await this.Alert("This file does not exist");
                    return;
                }
                var request = new HttpTransferRequest(this.Url, this.FilePath, this.IsUpload)
                {
                    UseMeteredConnection = this.UseMeteredConnection
                };
                await httpTransfers.Enqueue(request);

                await this.Navigation.PopAsync();
            });

            this.CreateRandom = this.LoadingCommand(async () =>
            {
                if (this.SizeInMegabytes <= 0)
                {
                    await this.Alert("Invalid File Size");
                    return;
                }
                await this.GenerateRandom();
                this.FilePath = this.GetRandomFilePath();
            });
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.sub = this.WhenAnyProperty(x => x.IsUpload).Subscribe(upload =>
            {
                if (!upload)
                {
                    this.FilePath = Path.Combine(this.platform.AppData.FullName, Guid.NewGuid().ToString() + ".download");
                }
                else
                {
                    var path = this.GetRandomFilePath();
                    if (File.Exists(path))
                        this.FilePath = path;
                }
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
        public ICommand Delete { get; }
        public ICommand CreateRandom { get; }


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


        bool meterConn = true;
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


        string filePath;
        public string FilePath
        {
            get => this.filePath;
            set => this.Set(ref this.filePath, value);
        }


        int fsize = 100;
        public int SizeInMegabytes
        {
            get => this.fsize;
            set => this.Set(ref this.fsize, value);
        }


        string GetRandomFilePath() => Path.Combine(this.platform.AppData.FullName, RANDOM_FILE_NAME);
        Task GenerateRandom() => Task.Run(() =>
        {
            var path = this.GetRandomFilePath();
            if (File.Exists(path))
                File.Delete(path); // delete previous random file

            var byteSize = this.SizeInMegabytes * 1024 * 1024;
            var data = new byte[8192];
            var rng = new Random();

            using (var fs = new FileStream(path, FileMode.Create))
            {
                while (fs.Length < byteSize)
                {
                    rng.NextBytes(data);
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        });
    }
}
