using AutoDI;
using Gtk;
using Norka.Services;

namespace Norka.Widgets
{
    public class DocumentsList : ListBox
    {
        readonly IDocumentsStorage storage;

        public DocumentsList()
        {
            Hexpand = true;
            Vexpand = true;

            storage = GlobalDI.GetService<IDocumentsStorage>();
            storage.DocumentAdded += (sender, args) => RefreshItems();
            storage.DocumentRemoved += (sender, args) => RefreshItems();
        }

        public void AddItem(string title, int documentId, int position = 0)
        {
            var item = new DocumentListRow(title);
            item.DocumentId = documentId;
            Insert(item, position);
            ShowAll();
        }

        public void RefreshItems()
        {
            System.Console.WriteLine("RefreshItems");
            var docs = storage.All();

            var selected = SelectedRow;

            foreach (var child in Children)
            {
                Remove(child);
            }

            foreach (var doc in docs)
            {
                AddItem(doc.Title, doc.Id);
            }
            SelectRow(selected);

            ShowAll();
        }
    }
}