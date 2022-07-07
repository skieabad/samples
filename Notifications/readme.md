# Local Notifications

## iOS Setup
If you want to use time sensitive notifications, you must create channel with high priority.  You will also need to ensure you create a provisioning profile on the Apple Dev Portal with it enabled and add the following to your entitlements.plist file:

```xml
<key>com.apple.developer.usernotifications.time-sensitive</key>
<true />
```

## 3rd Party Libs
* [Xamarin Forms](https://github.com/xamarin/xamarin.forms) - The cross platform library for .NET that we all know and love
* [SQLite.NET](https://github.com/praeclarum/sqlite-net) - An open source SQLite lightweight object relational mapper to store & read our background events