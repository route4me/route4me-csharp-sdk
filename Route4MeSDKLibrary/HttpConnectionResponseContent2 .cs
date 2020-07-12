using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public class HttpConnectionResponseContent2 : HttpContent
    {
        private Stream _stream;
        private bool _consumedStream; // separate from _stream so that Dispose can drain _stream

        public void SetStream(Stream stream)
        {
            Debug.Assert(stream != null);
            Debug.Assert(stream.CanRead);
            Debug.Assert(!_consumedStream);

            _stream = stream;
        }

        public Stream ConsumeStream()
        {
            if (_consumedStream || _stream == null)
            {
                throw new InvalidOperationException("SR.net_http_content_stream_already_read");
            }
            _consumedStream = true;

            return _stream;
        }

        protected sealed override Task SerializeToStreamAsync(Stream stream, TransportContext context) =>
            SerializeToStreamAsync(stream, context, CancellationToken.None);

        internal async Task SerializeToStreamAsync(Stream stream, TransportContext context, CancellationToken cancellationToken)
        {
            Debug.Assert(stream != null);

            using (Stream contentStream = ConsumeStream())
            {
                const int BufferSize = 8192;
                await contentStream.CopyToAsync(stream, BufferSize, cancellationToken).ConfigureAwait(false);
            }
        }

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return false;
        }

        protected sealed override Task<Stream> CreateContentReadStreamAsync() =>
            Task.FromResult<Stream>(ConsumeStream());

        internal  Stream TryCreateContentReadStream() =>
            ConsumeStream();

        internal bool AllowDuplex => false;

        protected sealed override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_stream != null)
                {
                    _stream.Dispose();
                    _stream = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
