using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Aon18.api.Services
{
    public class SkeletResponse: IHttpActionResult
    {
        private MemoryStream _memorystream;
        private string _fileName;
        private HttpRequestMessage _httpRequestMessage;
        private HttpResponseMessage _httpResponseMessage;


        public byte[] DataBytes { get; set; }


        public SkeletResponse(byte[] dataBytes, HttpRequestMessage httpRequestMessage, string fileName)
        {
            this.DataBytes = dataBytes;
            this._memorystream = new MemoryStream(dataBytes); ;
            this._httpRequestMessage = httpRequestMessage;
            this._fileName = fileName;
        }

        public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            _httpResponseMessage = _httpRequestMessage.CreateResponse(HttpStatusCode.OK);
            _httpResponseMessage.Content = new StreamContent(_memorystream);
            _httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            _httpResponseMessage.Content.Headers.ContentDisposition.FileName = _fileName;
            _httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return System.Threading.Tasks.Task.FromResult(_httpResponseMessage);
        }
    }
}