# All Code Gen

This project shows how ALL of Shiny codegen can literally wire up EVERYTHING imaginable

Things to take a look at:
* Sample\AssemblyInfo.cs - to see how your custom services can be wired in.
* Notice how your delegates are automatically wired
* Notice how any Shiny service that requires a delegate (ie. GeofenceDelegate) is not auto-registered when a delegate of that type is not presented
* Notice how any Shiny service that has an optional delegate (ie. GpsDelegate) is auto-registered in a foreground only mode
* You get a compiler warning about Shiny.Push.AzureNotificationHubs because it requires additional config that the Shiny generator can't supply - Shiny.Push & Shiny.Push.FirebaseMessaging are the only push providers that can be "auto wired"
* Notice all of the 3rd party plugins that also get registered in Android/iOS Project/References/Analyzers/Shiny.Generators

## 3rd Party Libs
* [Xamarin Forms](https://github.com/xamarin/xamarin.forms) - The cross platform library for .NET that we all know and love
* [Xamarin Essentials](https://github.com/xamarin/essentials) - The other cross platform library for .NET that we all know and love
* RG Popup Plugin
* XF Material
* ACR User Dialogs
* AIForms.SettingsView
* Microsoft.Identity.Client