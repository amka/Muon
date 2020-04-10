using System;
using AutoDI;
using Gtk;
using Norka.Services;

namespace Norka.Widgets
{
    public class DocumentsList : ListBox
    {
        readonly IDocumentsStorage storage;

        public event EventHandler ItemRenamed = delegate { };

        public DocumentsList()
        {
            Hexpand = true;
            Vexpand = true;

            storage = GlobalDI.GetService<IDocumentsStorage>();
            storage.DocumentAdded += (sender, args) => RefreshItems();
            storage.DocumentRemoved += (sender, args) => RefreshItems();
        }

        public int? SelectedDocumentId()
        {
            if (SelectedRow == null) return null;
            var row = SelectedRow.Child as DocumentListRow;
            return row.DocumentId;
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

        protected override bool OnButtonPressEvent(Gdk.EventButton evnt)
        {
            bool result = false;

            result = base.OnButtonPressEvent(evnt);
            if (evnt.Type == Gdk.EventType.DoubleButtonPress && evnt.Button == 1)
            {
                ItemRenamed(this, EventArgs.Empty);
            }

            if (evnt.Type == Gdk.EventType.ButtonPress && evnt.Button == 3)
            {
                var menuDocumentRemove = new Button("Remove document");
                menuDocumentRemove.ActionName = "document.remove";

                var menu = new Menu();
                menu.Add(menuDocumentRemove);
                menu.ShowAll();
                menu.PopupAtPointer(evnt);
            }
            return result;
        }
    }
}