using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Penguin.Web.Mvc
{
    public class ResponseCapture : IDisposable
    {
        #region Fields

        private readonly Stream originalWriter;
        private readonly HttpResponse response;
        private Stream localWriter;

        #endregion Fields

        #region Constructors

        public ResponseCapture(HttpResponse response)
        {
            this.response = response;
            this.originalWriter = response.Body;
            this.localWriter = new System.IO.MemoryStream();
            response.Body = this.localWriter;
        }

        #endregion Constructors

        #region Methods

        public void Dispose()
        {
            if (this.localWriter != null)
            {
                this.localWriter.Dispose();
                this.localWriter = null;
                this.response.Body = this.originalWriter;
            }
        }

        public override string ToString()
        {
            using (TextReader tr = new StreamReader(this.localWriter))
            {
                return tr.ReadToEnd();
            }
        }

        #endregion Methods
    }
}
