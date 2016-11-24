# ErrorUnit.Injector_PostSharp
Compatibility library for ErrorUnit to work with PostSharp AOP.
Unlike integrating with an IoC Library this will enable all classes/methods in an assembly to become Unit Test endpoints. 
(IoC library integrations are limited to IoC managed classes/methods)

Currently while ErrorUnit is not past beta ErrorUnit.Injector_PostSharp has an open source licence for the use of Postsharp (Thanks Gael!);
you will have to download the free PostSharp Express ( https://www.postsharp.net/download ); 
But check out the full version and the other cool stuff you can do with AOP in general and PostSharp in specific. 
( In the future with enough users we hope to include Postsharp technology fully integrated into ErrorUnit. )

## Instructions 

Add `[assembly: ErrorUnit.Injector_PostSharp.ErrorUnitAspect]` to the AssemblyInfo.cs file of each project you want ErrorUnit on.

Note:
To log your ErrorUnit formatted errors instead of just generating Unit Tests while in Visual Studio you will still have to set up a logger when your application starts, with  `ErrorUnitCentral._Logger = new ErrorUnitLogger();`

But since this is an AOP and not an IoC library you will not need to set up the Injector with:

`ErrorUnitCentral._Injector = new ErrorUnitInjector();`
and
`ErrorUnitCentral._LinkInjector(container);`