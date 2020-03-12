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
        public event EventHandler Saved;

        public EditorView(Window parent)
        {
            Parent = parent;
            TextBuffer = new TextBuffer(null);
            Editor = new TextView(TextBuffer)
            {
                Margin = 6,
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

        public async void Save(object sender, EventArgs e)
        {
            var dlg = new FileChooserDialog("Save document", Parent, FileChooserAction.Save, new object[] {
                "Cancel", ResponseType.Cancel,
                "Open", ResponseType.Accept,
            });
            AddFilters(dlg);

            // Read result and hide the dialog
            int result = dlg.Run();
            dlg.Hide();

            if (result == (int)ResponseType.Accept)
            {
                Console.WriteLine($"Save called for {dlg.Filename}");
                try
                {
                    await File.WriteAllTextAsync(dlg.Filename, TextBuffer.Text);
                    FilePath = new Uri(dlg.Filename);
                    Saved(this, null);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }

        }

        public async void Open(object sender, EventArgs e)
        {
            var dlg = new FileChooserDialog("Open document", Parent, FileChooserAction.Open, new object[] {
                "Cancel", ResponseType.Cancel,
                "Open", ResponseType.Accept,
            });
            AddFilters(dlg);

            // Read result and hide the dialog
            int result = dlg.Run();
            dlg.Hide();

            if (result == (int)ResponseType.Accept)
            {
                Console.WriteLine($"Open called for {dlg.Filename}");
                try
                {
                    var text = await File.ReadAllTextAsync(dlg.Filename, System.Text.Encoding.UTF8);
                    TextBuffer.Text = text;

                    FilePath = new Uri(dlg.Filename);

                    Opened(this, null);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }

        void AddFilters(FileChooserDialog dialog)
        {
            var filterText = new FileFilter();
            filterText.AddMimeType("text/plain");
            filterText.Name = "Text files";
            dialog.AddFilter(filterText);

            var filterAny = new FileFilter();
            filterAny.AddPattern("*");
            filterAny.Name = "Any";
            dialog.AddFilter(filterAny);
        }
    }
}