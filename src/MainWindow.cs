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

            // Widget initialization
            DefaultSize = new Gdk.Size(800, 600);
            DeleteEvent += Window_DeleteEvent;

            // Init children widgets
            header = new Header();
            Titlebar = header;

            container = new Norka.Widgets.Paned(Orientation.Horizontal, this);
            
            _docList = container.Sidebar.DocumentsList;
            _docList.ListRowActivated += (sender, args) => DocumentSelected();
            _editor = container.Editor;
            Add(container);

            container.Sidebar.DocumentsList.RefreshItems();

            var content = new Box(Orientation.Vertical, 0);
            content.PackStart(container, true, true, 0);
            Add(content);

            // Finally connect actins to window
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

            var actionFormatToggle = new GLib.SimpleAction("toggle", null);
            actionFormatToggle.Activated += (sender, args) => _editor.ToggleFormatBar();
            formatActions.AddAction(actionFormatToggle);

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

        public void CreateDocument()
        {
            storage.AddDocument();
        }

        public void RemoveDocument()
        {
            var docId = _docList.SelectedDocumentId();
            if (docId == null) return;

            storage.RemoveDocument(docId.GetValueOrDefault());
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        void SelectionEvent(object o, EventArgs args)
        {
            Console.WriteLine($"Got Event {args.ToString()}");
        }

        void DocumentSelected()
        {
            var docId = _docList.SelectedDocumentId();
            if (docId == null) return;

            _editor.LoadDocument(docId.GetValueOrDefault());
        }
    }
}
