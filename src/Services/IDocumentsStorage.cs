using System;
using System.Collections.Generic;
using Norka.Models;

namespace Norka.Services
{
    public interface IDocumentsStorage
    {
        event EventHandler DocumentAdded;
        event EventHandler DocumentRemoved;

        IEnumerable<Document> All();
        Document AddDocument();
        void RemoveDocument(Document doc);
    }
}