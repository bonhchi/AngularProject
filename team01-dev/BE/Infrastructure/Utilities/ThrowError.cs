using Infrastructure.Constants;
using System;

namespace Infrastructure.Utilities
{
    public static class ThrowError
    {
        public static void ArgumentIsNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
        /// <summary>
        /// Throws an exception of type <typeparamref name="TException"/> with the specified message
        /// when the assertion statement is true.
        /// </summary>
        /// <typeparam name="TException">The type of exception to throw.</typeparam>
        /// <param name="assertion">The assertion to evaluate. If true then the <typeparamref name="TException"/> exception is thrown.</param>
        /// <param name="message">string. The exception message to throw.</param>
        public static void Against<TException>(bool assertion, string message) where TException : Exception
        {
            if (assertion)
                throw (TException)Activator.CreateInstance(typeof(TException), message + "^" + StatusCodeErrorConst.ErrorDefault);
        }
        public static void Against<TException>(bool assertion, int codeError, string message) where TException : Exception
        {
            //Execute the lambda and if it evaluates to true then throw the exception.
            if (assertion)
                throw (TException)Activator.CreateInstance(typeof(TException), message + "^" + codeError);
        }
        /// <summary>
        /// Throws an exception of type <typeparamref name="TException"/> with the specified message
        /// when the assertion
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="assertion"></param>
        /// <param name="message"></param>
        public static void Against<TException>(Func<bool> assertion, string message) where TException : Exception
        {
            //Execute the lambda and if it evaluates to true then throw the exception.
            if (assertion())
                throw (TException)Activator.CreateInstance(typeof(TException), message + "^" + StatusCodeErrorConst.ErrorDefault);
        }
        public static void Against<TException>(Func<bool> assertion, int codeError, string message) where TException : Exception
        {
            //Execute the lambda and if it evaluates to true then throw the exception.
            if (assertion())
                throw (TException)Activator.CreateInstance(typeof(TException), message + "^" + codeError);
        }
    }
}
