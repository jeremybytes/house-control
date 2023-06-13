# Jeremy's House Control

## History
This application is a rewrite of a legacy application that interacts with X10 hardware. The legacy application was written in Delphi and ran in my apartment for several years to lights and A/C on and off based on a schedule.

The intial code explored a minimum viable produce (MVP) that pulled the essential functionality from the original application. The MVP code is explored in a seris of articles: [http://www.jeremybytes.com/Downloads.aspx#LegacyRewrite](http://www.jeremybytes.com/Downloads.aspx#LegacyRewrite)

> Current code can be run without hardware by using the "FakeCommander" object.

## Updates
Since the intial MVP, the code has been updated to support a scheduling tied to sunrise/sunset values for the current location. The location information in hard-coded into the code as a latitude/longitude value.  

**2023-06-13**  
Significant changes to the code were committed. These changes have developed over a period of time as this code has been used for labs and workshops relating to C# programming.  

Technical updates include the following:  
* Update to .NET 7.0
* Enabling of nullable reference types
* Use of pattern matching and switch expressions  

These updates have not affected functionality. Additional updates are forthcoming.
