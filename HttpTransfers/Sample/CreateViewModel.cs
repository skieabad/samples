using System;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            //this.Url = "http://acrmac:44378/upload";
            this.Url = "http://acrmonster:44378/upload";

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

                var verb = this.HttpVerb.ToLower() switch
                {
                    "post" => HttpMethod.Post,
                    "get" => HttpMethod.Get,
                    "put" => HttpMethod.Put,
                    _ => null
                };
                if (verb == null)
                {
                    await this.Alert("Invalid HTTP Verb - " + this.HttpVerb);
                    return;
                }
                var request = new HttpTransferRequest(this.Url, this.FilePath, this.IsUpload)
                {
                    UseMeteredConnection = this.UseMeteredConnection,
                    PostData = this.PostData,
                    HttpMethod = verb
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
                if (upload)
                {
                    this.Title = "New Upload";
                    this.HttpVerb = "POST";
                    var path = this.GetRandomFilePath();
                    if (File.Exists(path))
                        this.FilePath = path;
                }
                else
                {
                    this.Title = "New Download";
                    this.HttpVerb = "GET";
                    this.FilePath = Path.Combine(this.platform.AppData.FullName, Guid.NewGuid().ToString() + ".download");
                }
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


        string postData;
        public string PostData
        {
            get => this.postData;
            set => this.Set(ref this.postData, value);
        }


        string httpVerb;
        public string HttpVerb
        {
            get => this.httpVerb;
            set => this.Set(ref this.httpVerb, value);
        }


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
