using System;
using Foundation;


namespace ShinySponsors.iOS.CallDirectoryExtension
{
    [Register("CallDirectoryHandler")]
    public class CallDirectoryHandler : Shiny.CallDirectory.ShinyCallDirectoryExtension
    {
        public CallDirectoryHandler(IntPtr handle) : base(handle)
        {
        }
    }
}
