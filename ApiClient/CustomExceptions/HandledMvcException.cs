namespace CustomExceptions
{
    using System;

    /// <summary>
    /// Based class for all handled MVC exceptions, 
    /// which will contain error code and custom message to be displayed to the end user
    /// </summary>
    public class HandledMvcException : HandledApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandledMvcException"/> class.
        /// </summary>
        /// <param name="errorCode">Error code, to be sent to browser</param>
        /// <param name="customErrorMessage">Custom error message, to be sent to browser</param>
        public HandledMvcException(string errorCode, string customErrorMessage) : base(errorCode, customErrorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandledMvcException"/> class.
        /// </summary>
        /// <param name="errorCode">Error code, to be sent to browser</param>
        /// <param name="customErrorMessage">Custom error message, to be sent to browser</param>
        /// <param name="originalMessage">original exception message, if any</param>
        /// <param name="inner">original exception, if any</param>
        public HandledMvcException(string errorCode, string customErrorMessage, string originalMessage, Exception inner) : base(errorCode, customErrorMessage, originalMessage, inner)
        {
        }
    }
}
