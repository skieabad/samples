using System;
using Shiny;
using Android.App;
using Android.Runtime;


namespace Sample.Droid
{
	[Application]
	public class MainApplication : Application
	{
		public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) { }

		public override void OnCreate()
		{
            var methodName = "Initialize";
            var type = typeof(JNIEnv);
            var first = true;
            var methods = type.GetMethods(bindingAttr: System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy);
            foreach (var method in methods)
            {
                if (method.Name.Contains(methodName))
                {
                    if (first)
                    {
                        first = false;
                        continue;
                    }
                    System.Diagnostics.Debug.WriteLine("Method: " + method.Name);
                    method.Invoke(null, null);
                }
            }

            this.ShinyOnCreate(new Startup());
			base.OnCreate();
		}
	}
}
