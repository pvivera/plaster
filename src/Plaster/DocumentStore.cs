using System;
using System.IO;

namespace Plaster
{
    public class DocumentStore : IDisposable
    {
        private readonly string _path;

        public DocumentStore(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            _path = path;

            Directory.CreateDirectory(_path);
        }

        public DocumentSession OpenSession()
        {
            return new DocumentSession(_path);
        }

        public void Dispose()
        {
        }
    }
}
