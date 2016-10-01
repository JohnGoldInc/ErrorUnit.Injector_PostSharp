# ErrorUnit.Injector_PostSharp
Compatibility library for ErrorUnit to work with PostSharp AOP.

ErrorUnitAspect is to be added to Assemblies you want ErrorUnit to log errors for:
Add the line `[assembly:ErrorUnitAspect]` to your AssemblyInfo.cs file for each project you want error logs for

Note: Since this is a AOP and not a IoC library you will not need to set up the Injector with:

`ErrorUnitCentral._Injector = new ErrorUnitInjector();`
or
`ErrorUnitCentral._LinkInjector(container);`