using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ErrorUnit.Injector_PostSharp
{
    /// <summary>
    /// ErrorUnitAspect is to be added to Assemblies you want ErrorUnit to log errors for.
    /// Add the line <c>[assembly:ErrorUnitAspect]</c> to your AssemblyInfo.cs file for each project you want error logs for
    /// </summary>
    [Serializable]
    public class ErrorUnitAspect : AssemblyLevelAspect, IOnMethodBoundaryAspect, IAspectProvider
    {
        private ErrorPrecondition stackInfo = null;

        /// <summary>
        ///  Method executed before the body of methods to which this aspect is applied.
        /// </summary>
        /// <param name="args">Event arguments specifying which method is being executed, which are its arguments,
        /// and how should the execution continue after the execution of PostSharp.Aspects.IOnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs).</param>
        public void OnEntry(MethodExecutionArgs args)
        {
            stackInfo = new ErrorPrecondition(args);
            ErrorUnitInjector.ErrorUnitCentral.CurrentStack_Add(stackInfo);
        }

        /// <summary>
        /// Method executed after the body of methods to which this aspect is applied, in
        /// case that the method resulted with an exception.
        /// </summary>
        /// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
        public void OnException(MethodExecutionArgs args)
        {
            ErrorUnitInjector.ErrorUnitCentral.ThrowErrorStack(args.Exception);
        }

        /// <summary>
        /// Method executed after the body of methods to which this aspect is applied, even
        /// when the method exists with an exception (this method is invoked from the finally
        /// block).
        /// </summary>
        /// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
        public void OnExit(MethodExecutionArgs args)
        {
            stackInfo.End = DateTime.Now;
            ErrorUnitInjector.ErrorUnitCentral.CleanUp(stackInfo.End.Value);
        }

        /// <summary>
        /// Method executed after the body of methods to which this aspect is applied, but
        /// only when the method successfully returns (i.e. when no exception flies out the
        /// method.).
        /// </summary>
        /// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
        public void OnSuccess(MethodExecutionArgs args)
        {
          
        }

        /// <summary>
        /// Provides new aspects.
        /// </summary>
        /// <param name="targetElement">
        /// Code element (System.Reflection.Assembly, System.Type, System.Reflection.FieldInfo,
        /// System.Reflection.MethodBase, System.Reflection.PropertyInfo, System.Reflection.EventInfo,
        /// System.Reflection.ParameterInfo, or PostSharp.Reflection.LocationInfo) to which
        /// the current aspect has been applied.</param>
        /// <returns>A set of aspect instances.</returns>
        public IEnumerable<AspectInstance> ProvideAspects(object targetElement)
        {
            var type = (Assembly)targetElement;

            return type.GetTypes()
                       .SelectMany(t=>t.GetMethods())
                       .Select(m => new AspectInstance(m, new ErrorUnitAspect()) );
        }

        /// <summary>
        /// Initializes the current aspect.
        /// </summary>
        /// <param name="method"></param>
        public void RuntimeInitialize(MethodBase method)
        {
           
        }
    }
}
