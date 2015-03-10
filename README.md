# eskom-stages
This small webapp exposes the current Eskom load shedding stage data in several formats.

It is written in C# and targets .NET Framework 4.5.1, although it will probably work just fine on lower versions too. It uses the Razor view engine with MVC 5 and Newtonsoft's Json.NET.

### Compiling and Running
As this project was created in Visual Studio 2013, the simplest would be to use that to build it. If you don't have Visual Studio, you can get [Visual Studio Community 2013](https://www.visualstudio.com/en-us/products/visual-studio-community-vs) for free. Alternatively, you can try [MonoDevelop](http://www.monodevelop.com/), which is cross-platform.

### Usage
Once the webapp is up and running, you can access the various output types the correct way, by varying the Accept header when requesting `/`, or the lazy way, by URL.

  - **HTML** - `Accept: text/html` or `Accept: */*` or by URL `/html`
  - **JSON** - `Accept: application/json` or `Accept: text/json` or by URL `/json`
  - **XML** - `Accept: text/xml` or `Accept: application/xml` or by URL `/xml`
  - **Text** - `Accept: text/plain` or by URL `/text`


### Notes
The data returned by this app is cached for 2 minutes if the call to the Eskom endpoint was successful. If unsuccessful, the data is cached for 5 seconds to avoid hammering the Eskom endpoint.

The Eskom endpoint in use? http://loadshedding.eskom.co.za/loadshedding/GetStatus