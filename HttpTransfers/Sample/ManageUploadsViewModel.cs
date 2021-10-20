using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Shiny;
using Xamarin.Forms;


namespace Sample
{
    public class ManageUploadsViewModel : ViewModel
    {
        readonly IPlatform platform;


        public ManageUploadsViewModel()
        {
            this.platform = ShinyHost.Resolve<IPlatform>();

            this.Delete = new Command<string>(async file =>
            {
                var path = Path.Combine(this.platform.AppData.FullName, file);
                if (!File.Exists(path))
                {
                    await this.Alert($"{file} does not exist");
                }
                else
                {
                    File.Delete(path);
                    await this.Alert($"{file} has been deleted");
                }
            });

            this.CreateRandom = this.LoadingCommand(async () =>
            {
                if (this.SizeInMegabytes <= 0)
                {
                    await this.Alert("Invalid File Size");
                    return;
                }
                await this.GenerateRandom();
            });
        }


        public ICommand Delete { get; }
        public ICommand CreateRandom { get; }


        int fsize = 100;
        public int SizeInMegabytes
        {
            get => this.fsize;
            set => this.Set(ref this.fsize, value);
        }


        Task GenerateRandom() => Task.Run(() =>
        {
            var path = Path.Combine(this.platform.AppData.FullName, "upload.random");
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
