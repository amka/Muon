using System;
using System.IO;
using Gtk;
using Muon.Widgets;

namespace Muon
{
    class MainWindow : ApplicationWindow
    {
        Header header;
        EditorView Editor;

        public MainWindow(Gtk.Application application) : base(application)
        {
            DefaultSize = new Gdk.Size(800, 600);

            header = new Header();
            Titlebar = header;

            DeleteEvent += Window_DeleteEvent;

            Editor = new EditorView(this);
            Editor.Opened += UpdateSubtitle;
            Editor.Saved += UpdateSubtitle;

            header.OpenButton.Clicked += Editor.Open;
            header.SaveButton.Clicked += Editor.SaveAs;

            Add(Editor.View);
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
    }
}
