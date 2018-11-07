# SharpLibDisplay

C# library designed to manage small consumer electronic displays such as VFD and LCD through a Client/Server architecture based on [WCF](https://docs.microsoft.com/en-us/dotnet/framework/wcf/index).

# Binary Distribution

The easiest way to make use of this library in your own project is to add a reference to the following [NuGet package](https://www.nuget.org/packages/SharpLibDisplay/)

# Debugging

For testing purposes we use a console implementation of our Client/Server protocol.
In order to conveniently start both client and server processes you should follow the instructions linked below.
[How to: Set multiple startup projects](https://docs.microsoft.com/en-us/visualstudio/ide/how-to-set-multiple-startup-projects?view=vs-2017)

Depending on what you are testing you may want to adjust the console server timeouts. 
See [Configuring Timeout Values on a Binding](https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/configuring-timeout-values-on-a-binding)