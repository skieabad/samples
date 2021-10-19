using System;
using Foundation;


namespace Sample.iOS.CallDirectoryExtension
{
    [Register("CallDirectoryHandler")]
    public class CallDirectoryHandler : Shiny.CallDirectory.ShinyCallDirectoryExtension
    {
        public CallDirectoryHandler(IntPtr handle) : base(handle)
        {
        }
    }
}
