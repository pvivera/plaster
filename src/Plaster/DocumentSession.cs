using System;
using System.Collections.Generic;

namespace Plaster
{
    public class DocumentSession : IDisposable
    {
        private readonly string _path;
        private bool _disposed;
        private List<object> _documents = new List<object>();

        ~DocumentSession()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }
            }
            _disposed = true;
        }

        public DocumentSession(string path)
        {
            _path = path;
        }

        public void Store(object obj)
        {
            _documents.Add(obj);
        }

//        public void SaveChanges()
//        {
//            _
//        }
    }

//    public class ObjectDocumentStream : DocumentStream
//    {
//        private readonly IEnumerable<DomainEvent> _events;
//
//        public ObjectDocumentStream(IEnumerable<DomainEvent> events, string primaryKeyFieldName = null)
//            : base(primaryKeyFieldName)
//        {
//            _events = events;
//        }
//
//        public override IEnumerable<Document> ReadSource()
//        {
//            foreach (var e in _events)
//            {
//                yield return ConvertToDocument(e);
//            }
//        }
//
//        private Document ConvertToDocument(object e)
//        {
//            var fields = new List<Field>();
//
//            foreach (var propertyInfo in e.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
//                .Where(p => p.Name != "Meta"))
//            {
//                fields.Add(new Field(propertyInfo.Name, propertyInfo.GetValue(e)));
//            }
//
//            foreach (var metaKey in e.Meta.Keys)
//            {
//                fields.Add(new Field($"Metadata.{metaKey}", e.Meta[metaKey]));
//            }
//
//            fields.Add(new Field($"Metadata.{DomainEvent.MetadataKeys.Type}", e.GetType()));
//
//            return new Document(fields);
//        }
//    }
}