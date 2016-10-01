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
    public class ErrorUnitAspect : OnMethodBoundaryAspect
    {
        private ErrorPrecondition stackInfo = null;

        /// <summary>
        ///  Method executed before the body of methods to which this aspect is applied.
        /// </summary>
        /// <param name="args">Event arguments specifying which method is being executed, which are its arguments,
        /// and how should the execution continue after the execution of PostSharp.Aspects.IOnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs).</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            stackInfo = new ErrorPrecondition(args);
            ErrorUnitCentral.Instance.CurrentStack_Add(stackInfo);
        }

        /// <summary>
        /// Method executed after the body of methods to which this aspect is applied, in
        /// case that the method resulted with an exception.
        /// </summary>
        /// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
        public override void OnException(MethodExecutionArgs args)
        {
            ErrorUnitCentral.Instance.ThrowErrorStack(args.Exception);
        }

        /// <summary>
        /// Method executed after the body of methods to which this aspect is applied, even
        /// when the method exists with an exception (this method is invoked from the finally
        /// block).
        /// </summary>
        /// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
        public override void OnExit(MethodExecutionArgs args)
        {
            stackInfo.End = DateTime.Now;
            ErrorUnitCentral.Instance.CleanUp(stackInfo.End.Value);
        }

    }
}
