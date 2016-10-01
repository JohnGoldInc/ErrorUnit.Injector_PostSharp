using ErrorUnit.Interfaces;

    /// <summary>
    /// The Injector link for PostSharp
    /// </summary>
    public class ErrorUnitInjector : IInjector
    {
     
        /// <summary>
        /// The json serializer to use
        /// </summary>
        public static IErrorUnitCentral ErrorUnitCentral;

        /// <summary>
        /// Links the injector.
        /// </summary>
        /// <typeparam name="C"></typeparam>
        /// <param name="ioc">The Assembly</param>
        /// <param name="errorUnitCentral">The ErrorUnitCentral.Instance</param>
        /// <returns></returns>
        public C LinkInjector<C>(C ioc, IErrorUnitCentral errorUnitCentral)
        {
            ErrorUnitInjector.ErrorUnitCentral = errorUnitCentral;

            return ioc;
        }
    }
}
