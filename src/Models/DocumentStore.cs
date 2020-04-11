using System;
using AutoDI;
using Gtk;
using Norka.Services;

namespace Norka.Models
{
    public class DocumentStore : ListStore
    {
        readonly IDocumentsStorage storage;
        public DocumentStore(params Type[] types) : base(types)
        {
            storage = GlobalDI.GetService<IDocumentsStorage>();
        }
    }
}