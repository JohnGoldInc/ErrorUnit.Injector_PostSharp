﻿# ErrorUnit.Injector_PostSharp
Compatibility library for ErrorUnit to work with PostSharp AOP.
Unlike integrating with an IoC Library this will enable all methods to become Unit Test endpoints. 
(IoC library integrations are limited to IoC managed class methods)

Currently a purchase of at minimum *PostSharp Diagnostics* would be necessary after PostSharp's evaluation period https://www.postsharp.net/purchase

ErrorUnitAspect is to be added to Assemblies you want ErrorUnit to log errors for:
Add the line `[assembly:ErrorUnitAspect]` to your AssemblyInfo.cs file for each project you want error logs for

Note: Since this is a AOP and not a IoC library you will not need to set up the Injector with:

`ErrorUnitCentral._Injector = new ErrorUnitInjector();`
or
`ErrorUnitCentral._LinkInjector(container);`