# Push
This sample shows how to use not just one push provider, but ALL push providers as configured during the build process.  Simply open Directory.build.props and change PushProvider to blank (native), firebase, onesignal, or azure to swap between providers.
This was designed to show how simple it is to swap between providers with Shiny

## Android
1. Copy your own google-services.json to the root of this project (where this solution exists) and it will be copied into the android project during compile
2. Ensure the AndroidManifest.xml package name matches your firebase setup

## iOS
1. Ensure the aps-environment is pointed at development or production depending on your setup in your Entitlements.plist
2. Set the 'CFBundleIdentifier' in your Info.plist to match your APN setup

## 3rd Party Libs
* [Xamarin Forms](https://github.com/xamarin/xamarin.forms) - The cross platform library for .NET that we all know and love
* [SQLite.NET](https://github.com/praeclarum/sqlite-net) - An open source SQLite lightweight object relational mapper to store & read our background events
* [Mobile Build Tools](https://mobilebuildtools.com/) - This is an awesome open source library by Dan Siegel for compiling with secrets like a connection string or token