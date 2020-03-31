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

            var FormatBar = new FormatBar();
            var FormatRevealer = new Revealer();
            FormatRevealer.Add(FormatBar);

            FormatRevealer.RevealChild = false;

            var content = new Box(Orientation.Vertical, 0);
            content.PackStart(FormatRevealer, false, true, 0);
            content.PackStart(Editor.View, true, true, 0);
            Add(content);

            header.FormatButton.Toggled += (sender, args) => {
                FormatRevealer.RevealChild = !FormatRevealer.RevealChild;
            };
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
