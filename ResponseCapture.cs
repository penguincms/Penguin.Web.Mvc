using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Penguin.Web.Mvc
{
    /// <summary>
    /// Captures an HTTP response to a string
    /// </summary>
    public class ResponseCapture : IDisposable
    {
        #region Fields

        private readonly Stream originalWriter;
        private readonly HttpResponse response;
        private Stream localWriter;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructs the class using the given response
        /// </summary>
        /// <param name="response">The response to capture</param>
        public ResponseCapture(HttpResponse response)
        {
            this.response = response ?? throw new ArgumentNullException(nameof(response));
            originalWriter = response.Body;
            localWriter = new System.IO.MemoryStream();
            response.Body = localWriter;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Disposes of the capture and releases resources
        /// </summary>
        public void Dispose()
        {
            if (localWriter != null)
            {
                localWriter.Dispose();
                localWriter = null;
                response.Body = originalWriter;
            }
        }

        /// <summary>
        /// Converts the resposne to a string
        /// </summary>
        /// <returns>The response as a string</returns>
        public override string ToString()
        {
            using TextReader tr = new StreamReader(localWriter);
            return tr.ReadToEnd();
        }

        #endregion Methods
    }
}