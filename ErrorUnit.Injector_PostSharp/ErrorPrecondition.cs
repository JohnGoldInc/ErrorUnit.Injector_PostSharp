using ErrorUnit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace ErrorUnit.Injector_PostSharp
{
    public class ErrorPrecondition : aErrorPrecondition
    {
        private MethodExecutionArgs args;

        public ErrorPrecondition(MethodExecutionArgs args)
        {
            this.args = args;
        }


        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public override TypeNameAndObjectValue[] Arguments { get { return args.Arguments == null ? null : args.Arguments.Select(a => new TypeNameAndObjectValue(a == null ? "" : a.GetType().FullName, a)).ToArray(); } set { } }

        /// <summary>
        /// Gets the full type name of the class the method that failed is in.
        /// </summary>
        /// <value>
        /// The full type name of the class.
        /// </value>
        public override string InvocationClassName { get { return args.Method.ReflectedType.FullName; } set { } }

        /// <summary>
        /// Gets the invocation class.
        /// </summary>
        /// <value>
        /// The invocation class.
        /// </value>
        public override TypeNameAndObjectValue InvocationClassValue { get { return args.Instance == null ? null : new TypeNameAndObjectValue(args.Instance.GetType().FullName, args.Instance); } set { } }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <value>
        /// The name of the method.
        /// </value>
        public override string MethodName { get { return args.Method.Name; } set { } }
    }
}
