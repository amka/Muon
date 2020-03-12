using System;
using System.IO;
using Gtk;

namespace Muon.Widgets
{
    public class EditorView
    {
        TextBuffer TextBuffer;
        TextView Editor;
        public ScrolledWindow View;
        public Uri FilePath;
        Window Parent;


        public event EventHandler Opened;

        public EditorView(Window parent)
        {
            Parent = parent;
            TextBuffer = new TextBuffer(null);
            Editor = new TextView(TextBuffer)
            {
                Margin = 16,
                WrapMode = WrapMode.Word,
                Vexpand = true,
            };

            View = new ScrolledWindow()
            {
                Expand = true
            };
            View.StyleContext.AddClass("view");
            View.Add(Editor);
        }

        public void Save(object sender, EventArgs e)
        {
            Console.WriteLine("Saved");
        }

        public async void Open(object sender, EventArgs e)
        {
            var dlg = new FileChooserDialog("", Parent, FileChooserAction.Open, new object[] {
                "Cancel", ResponseType.Cancel,
                "Open", ResponseType.Accept,
            });
            dlg.DefaultResponse = ResponseType.Accept;

            if (dlg.Run() == -3)
            {
                Console.WriteLine($"Opened for {dlg.Filename}");

                var text = await File.ReadAllTextAsync(dlg.Filename, System.Text.Encoding.UTF8);
                TextBuffer.Text = text;

                FilePath = new Uri(dlg.Filename);

                Opened(this, null);
            }
            dlg.Hide();
        }
    }
}