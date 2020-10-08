namespace CustomExceptions
{
    using System;

    /// <summary>
    /// Based class for all handled API exceptions, 
    /// which will contain error code and custom message to be displayed to the end user
    /// </summary>
    public class HandledApiException : HandledApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandledApiException"/> class.
        /// </summary>
        /// <param name="errorCode">Error code, to be sent to browser</param>
        /// <param name="customErrorMessage">Custom error message, to be sent to browser</param>
        public HandledApiException(string errorCode, string customErrorMessage) : base(errorCode, customErrorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandledApiException"/> class.
        /// </summary>
        /// <param name="errorCode">Error code, to be sent to browser</param>
        /// <param name="customErrorMessage">Custom error message, to be sent to browser</param>
        /// <param name="originalMessage">original exception message, if any</param>
        /// <param name="inner">original exception, if any</param>
        public HandledApiException(string errorCode, string customErrorMessage, string originalMessage, Exception inner) : base(errorCode, customErrorMessage, originalMessage, inner)
        {
        }
    }
}
