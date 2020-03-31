using System;
using System.IO;
using Gtk;
using Muon.Widgets;

namespace Muon
{
    class MainWindow : ApplicationWindow
    {
        Header header;
        Editor Editor;

        public MainWindow(Gtk.Application application) : base(application)
        {
            DefaultSize = new Gdk.Size(800, 600);

            header = new Header();
            Titlebar = header;

            DeleteEvent += Window_DeleteEvent;

            Editor = new Editor(this);
            Editor.Opened += UpdateSubtitle;
            Editor.Saved += UpdateSubtitle;

            var FormatBar = new FormatBar(Orientation.Horizontal, 6);
            var FormatRevealer = new Revealer();
            FormatRevealer.Add(FormatBar);

            FormatRevealer.RevealChild = false;

            var content = new Box(Orientation.Vertical, 0);
            content.PackStart(FormatRevealer, false, true, 0);
            content.PackStart(Editor.View, true, true, 0);
            Add(content);

            header.FormatButton.Toggled += (sender, args) =>
            {
                FormatRevealer.RevealChild = !FormatRevealer.RevealChild;
            };

            SetupActions();
        }

        void SetupActions()
        {
            var formatActions = new GLib.SimpleActionGroup();

            var actionBold = new GLib.SimpleAction("bold", null);
            actionBold.Activated += (sender, args) =>
            {
                Editor.ToggleTag("bold");
            };
            formatActions.AddAction(actionBold);

            var actionFormatClear = new GLib.SimpleAction("clear", null);
            actionFormatClear.Activated += (sender, args) =>
            {
                Editor.ClearTags();
            };
            formatActions.AddAction(actionFormatClear);
            
            InsertActionGroup("format", formatActions);

            var documentActions = new GLib.SimpleActionGroup();
            var actionDocumentOpen = new GLib.SimpleAction("open", null);
            actionDocumentOpen.Activated += Editor.Open;
            var actionDocumentSaveAs = new GLib.SimpleAction("save-as", null);
            actionDocumentSaveAs.Activated += Editor.SaveAs;

            documentActions.AddAction(actionDocumentOpen);
            documentActions.AddAction(actionDocumentSaveAs);

            InsertActionGroup("document", documentActions);
        }

        private void UpdateSubtitle(object sender, EventArgs args)
        {
            header.Subtitle = Editor.Document.Name;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        void SelectionEvent(object o, EventArgs args)
        {
            Console.WriteLine($"Got Event {args.ToString()}");
        }

        void AddTab()
        {

        }
    }
}
