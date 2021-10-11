# All Code Gen

This project shows how ALL of Shiny codegen can literally wire up EVERYTHING imaginable

Things to take a look at:
* Sample\AssemblyInfo.cs - to see how your custom services can be wired in.
* Your delegates are automatically wired
* Any Shiny service that requires a delegate (ie. GeofenceDelegate) is not auto-registered when a delegate of that type is not presented
* Any Shiny service that has an optional delegate (ie. GpsDelegate) is auto-registered in a foreground only mode
* You get a compiler warning about Shiny.Push.AzureNotificationHubs because it requires additional config that the Shiny generator can't supply - Shiny.Push & Shiny.Push.FirebaseMessaging are the only push providers that can be "auto wired"
* There are some 3rd party plugins that also get registered in Android/iOS Project/References/Analyzers/Shiny.Generators
* Look at how MyCustomTestService.cs registers itself with Shiny in this full auto-register mode
* Auto-registration is achieved simply by leaving ShinyStartupTypeName on the [assembly:ShinyApplicationAttribute] blank 

## 3rd Party Libs
* [Xamarin Forms](https://github.com/xamarin/xamarin.forms) - The cross platform library for .NET that we all know and love
* [Xamarin Essentials](https://github.com/xamarin/essentials) - The other cross platform library for .NET that we all know and love
* [RG Plugins Popup](https://github.com/rotorgames/Rg.Plugins.Popup) - Popular Xamrin Forms plugin
* [XF Material](https://github.com/Baseflow/XF-Material-Library) - Adds a nice API and Material themeing on top of RG Popups
* [ACR User Dialogs](https://github.com/aritchie/userdialogs) - An oldie, but still loved, somehow - something I created years ago
* [AIForms.SettingsView](https://github.com/muak/AiForms.SettingsView) - This makes tableviews 10x more powerful for XF
* [Microsoft.Identity.Client](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet) - The MSAL library that everyone knows (love it or hate it, we use it)