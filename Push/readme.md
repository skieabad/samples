# Push
This sample shows how to use not just one push provider, but ALL push providers as configured during the build process.  Simply open Directory.build.props and change PushProvider to blank (native), firebase, onesignal, or azure to swap between providers.
This was designed to show how simple it is to swap between providers with Shiny

## Android
1. Copy your own google-services.json to the root of this project (where this solution exists) and it will be copied into the android project during compile
2. Ensure the AndroidManifest.xml package name matches your firebase setup

## iOS
1. Ensure the aps-environment is pointed at development or production depending on your setup in your Entitlements.plist
2. Set the 'CFBundleIdentifier' in your Info.plist to match your APN setup

## Firebase iOS
If using firebase on iOS, copy your GoogleService-Info.plist to the solution root

## Azure Notification Hubs & OneSignal
Make sure to enter the appropriate values under appsettings.json (OneSignalAppId or the Azure variables)

## 3rd Party Libs
* [Xamarin Forms](https://github.com/xamarin/xamarin.forms) - The cross platform library for .NET that we all know and love
* [SQLite.NET](https://github.com/praeclarum/sqlite-net) - An open source SQLite lightweight object relational mapper to store & read our background events
* [Mobile Build Tools](https://mobilebuildtools.com/) - This is an awesome open source library by Dan Siegel for compiling with secrets like a connection string or token


## Bad Practices
This sample uses several bad practices for the sake of the sample!!  

* It includes appsettings.json (secrets) & the firebase config files.  I have blanked them out for the sake of my own keys not being stolen, but they should always be excluded from your GIT repository and only inserted at build time.
* appsettings.json also contains both server and client side configurations.  Server side vars should obviously NOT be deployed to your client/mobile apps