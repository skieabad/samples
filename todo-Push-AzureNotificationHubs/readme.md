# Push


## Compiling
* Unlike other samples, Push has some special connection tokens that are used.  Please create an appsettings.json along side the solution file for this project.  It should look similar to the following
```js
{
    "Blah": ""
}
```

2. Copy your own google-services.json to the root of this project (where this solution exists) and it will be copied into the android project during compile

## 3rd Party Libs
* [Xamarin Forms](https://github.com/xamarin/xamarin.forms) - The cross platform library for .NET that we all know and love
* [SQLite.NET](https://github.com/praeclarum/sqlite-net) - An open source SQLite lightweight object relational mapper to store & read our background events
* [Mobile Build Tools](https://mobilebuildtools.com/) - This is an awesome open source library by Dan Siegel for compiling with secrets like a connection string or token