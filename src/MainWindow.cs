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
            var actions = new GLib.SimpleActionGroup();

            Editor = new Editor(this);
            Editor.Opened += UpdateSubtitle;
            Editor.Saved += UpdateSubtitle;

            header.OpenButton.Clicked += Editor.Open;
            header.SaveButton.Clicked += Editor.SaveAs;

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
                Console.WriteLine("format-bold activated");
            };
            formatActions.AddAction(actionBold);
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
