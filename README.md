# Shiny Samples
 
## THESE SAMPLES ARE NOT DONE (EVEN THE ONES THAT DON"T SAY TODO - THIS NOTE WILL DISAPPEAR WHEN THEY ARE)

## Libraries

|Link/Title|Description|
|----------|-----------|
|[Beacons](Beacons)|Foreground & Background detection of BluetoothLE iBeacons|
|[Beacons - Managed](Beacons)|iBeacon ranging on steroids - the managed mechanism manages broadcasting to the main thread, throttles output to keep your UI nice & snappy|
|[Push - Native](Push-Native)|Uses purely native push notification API's provided by the OS|
|[Push - Firebase](Push-Firebase)|Uses firebase messaging for push notifications on both iOS & Android|
|[Push - Azure Notification Hubs](Push-AzureNotificationsHub)|Push notifications via Azure Notification Hubs and only 1 line of code to be changed|
|[Push - OneSignal](Push-OneSignal)|Push notifications via OneSignal and only 1 line of code to be changed|
|[NFC](Nfc)|Near Field Communication|
|[Jobs](Jobs)|How to register jobs at startup or dynamically|
|[Key/Value Storage](Stores)|How Shiny uses a "viewmodel" style object to bind to OS preferences, secure storage, and others|
|[Bluetooth LE - Client](BluetoothLE-Client)|The biggest module in all of Shiny|BluetoothLE powered by Reactive Extensions|
|[Bluetooth LE - Client - Managed](BluetoothLE-Managed)|The BLE Managed functions make it super easy to scan and work with a BLE peripheral|
|[Bluetooth LE - Hosting](BluetoothLE-Hosting)|BLE Hosting|Foreground only on this one|sorry :)|
|[HTTP Transfers](HttpTransfers)|Perform upload & download operations while in the background.  Even track statistics while in the foreground|
|[GPS](Locations-Gps)|Background GPS|
|[Geofencing](Locations-Geofencing)|Geofence in your app with ease!  We even handle the automatic transition to different APIs when Google Play Services aren't around|
|[Motion Activity](Locations-MotionActivity)|Query your users activity|see walks, runs, drives, and more!|
|[Local Notifications](Notifications)|See how the best-of-breed local notifications for Xamarin module runs|
|[Sensors](Sensors)|Reactive sensors|RX was made for this!|
|[Speech Recognition](SpeechRecognition)|Local speech recognition|while not officially supported by Shiny|we give this away just because|
|Platform Intrisics|TODO|

## Integrations
|Link/Title|Description|
|----------|-----------|
|[Prism + DryIoc + Shiny](Integration-Prism)|Prism Integration Example
|[Better Prism/RXUI Integration via Shiny.Framework](Integration-Best-Prism-RXUI)|Prism + ReactiveUI + Shiny with ease
|SQLite|Stores, Settings, & Logging|
|[AppCenter](AppCenter)|This sample shows how to configure AppCenter logging in Shiny using Microsoft.Extensions.Logging and Shiny.Logging.AppCenter|
|Firebase Analytics|TODO|

## Boilerplate Stuff
|Link/Title|Description|
|----------|-----------|
|[No Code Gen](Boilerplate-NoCodeGen)|AppDelegate, Android App, & Android Activity are all manually hooked for Shiny to fully operate|
|[All Code Gen](Boilerplate-All-CodeGen)|Everything from your iOS AppDelegate, Android Application & Activity, and the Shiny startup are generated|along with a lot of third party libraries|
|[Standard/Recommended](Jobs)|This will wire in your Xamarin Forms app as well as any Android/iOS boilerplate - you are in charge of the startup file|

## Advanced Customization
|Link/Title|Description|
|----------|-----------|
|[Custom Dependency Injection](DI-custom)|This example shows how to configure Shiny to use Autofac|
|[Register Platform Services](DI-platform)|This example shows how to register a platform specific services with Shiny|