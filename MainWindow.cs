using System;
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
            Editor.Opened += (sender, args) => {
                header.SaveButton.Sensitive = true;
                header.Subtitle = Editor.FilePath.PathAndQuery;
            };

            header.OpenButton.Clicked += Editor.Open;
            header.SaveButton.Clicked += Editor.Save;

            Add(Editor.View);
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
