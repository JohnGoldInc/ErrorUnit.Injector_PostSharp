using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ErrorUnit.Injector_PostSharp
{
    public class ErrorUnitAspect : IOnMethodBoundaryAspect
    {
        private ErrorPrecondition stackInfo = null;

        public void OnEntry(MethodExecutionArgs args)
        {
            stackInfo = new ErrorPrecondition(args);
            ErrorUnitInjector.ErrorUnitCentral.CurrentStack_Add(stackInfo);
        }

        public void OnException(MethodExecutionArgs args)
        {
            ErrorUnitInjector.ErrorUnitCentral.ThrowErrorStack(args.Exception);
        }

        public void OnExit(MethodExecutionArgs args)
        {
            stackInfo.End = DateTime.Now;
            ErrorUnitInjector.ErrorUnitCentral.CleanUp(stackInfo.End.Value);
        }

        public void OnSuccess(MethodExecutionArgs args)
        {
          
        }

        public void RuntimeInitialize(MethodBase method)
        {
           
        }
    }
}
