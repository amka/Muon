using System;
using System.IO;
using Gtk;
using Norka.Services;
using Norka.Widgets;
using AutoDI;

namespace Norka
{
    class MainWindow : ApplicationWindow
    {
        Header header;
        Norka.Widgets.Paned container;
        Editor _editor;
        DocumentsList _docList;

        IDocumentsStorage storage;

        public MainWindow(Gtk.Application application) : base(application)
        {
            storage = GlobalDI.GetService<IDocumentsStorage>();

            DefaultSize = new Gdk.Size(800, 600);

            header = new Header();
            Titlebar = header;

            DeleteEvent += Window_DeleteEvent;

            container = new Norka.Widgets.Paned(Orientation.Horizontal, this);
            // Link to class vars
            _docList = container.Sidebar.DocumentsList;
            _editor = container.Editor;
            Add(container);

            container.Sidebar.DocumentsList.RefreshItems();

            var FormatBar = new FormatBar(Orientation.Horizontal, 6);
            var FormatRevealer = new Revealer();
            FormatRevealer.Add(FormatBar);

            FormatRevealer.RevealChild = false;

            var content = new Box(Orientation.Vertical, 0);
            content.PackStart(FormatRevealer, false, true, 0);
            content.PackStart(container, true, true, 0);
            Add(content);

            // header.FormatButton.Toggled += (sender, args) =>
            // {
            //     FormatRevealer.RevealChild = !FormatRevealer.RevealChild;
            // };

            SetupActions();
        }

        void SetupActions()
        {
            // Format Actions
            var formatActions = new GLib.SimpleActionGroup();

            var actionBold = new GLib.SimpleAction("bold", null);
            actionBold.Activated += (sender, args) => _editor.ToggleTag("bold");
            formatActions.AddAction(actionBold);

            var actionFormatClear = new GLib.SimpleAction("clear", null);
            actionFormatClear.Activated += (sender, args) => _editor.ClearTags();
            formatActions.AddAction(actionFormatClear);

            InsertActionGroup("format", formatActions);

            // Document Actions
            var documentActions = new GLib.SimpleActionGroup();

            var actionDocumentOpen = new GLib.SimpleAction("create", null);
            actionDocumentOpen.Activated += (sender, args) => CreateDocument();
            documentActions.AddAction(actionDocumentOpen);

            var actionDocumentRemove = new GLib.SimpleAction("remove", null);
            actionDocumentRemove.Activated += (sender, args) => RemoveDocument();
            documentActions.AddAction(actionDocumentRemove);

            InsertActionGroup("document", documentActions);
        }

        private void UpdateSubtitle(object sender, EventArgs args)
        {
            // header.Subtitle = edi.Document.Name;
        }

        public void CreateDocument()
        {
            storage.AddDocument();
        }

        public void RemoveDocument()
        {
            var row = _docList.SelectedRow;
            if (row == null) return;

            var child = row.Child as DocumentListRow;
            storage.RemoveDocument(child.DocumentId);
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        void SelectionEvent(object o, EventArgs args)
        {
            Console.WriteLine($"Got Event {args.ToString()}");
        }
    }
}
