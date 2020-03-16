using System;
using System.IO;
using Gtk;
using Muon.Models;

namespace Muon.Widgets
{
    public class EditorView
    {
        TextBuffer TextBuffer;
        TextView Editor;
        public ScrolledWindow View;
        Window Parent;
        public Document Document;


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

            Editor.KeyReleaseEvent += KeyPressReleased;
        }

        private void KeyPressReleased(object o, KeyReleaseEventArgs args)
        {
            Console.WriteLine($"KeyReleased: {args.Event.Key}");
            if (args.Event.Key == Gdk.Key.Control_L && Editor.Buffer.HasSelection)
            {
                var formatPopover = new Popover(Editor);
                formatPopover.Position = PositionType.Bottom;

                var formatGrid = new Grid();
                formatGrid.StyleContext.AddClass("linked");
                formatGrid.Orientation = Orientation.Horizontal;
                formatGrid.Margin = 8;
                formatGrid.Add(new Button("format-text-bold-symbolic", IconSize.Menu));
                formatGrid.Add(new Button("format-text-italic-symbolic", IconSize.Menu));
                formatGrid.Add(new Button("format-text-underline-symbolic", IconSize.Menu));

                formatPopover.Add(formatGrid);
                formatPopover.ShowAll();

                TextIter iterStart;
                TextIter iterEnd;
                TextBuffer.GetSelectionBounds(out iterStart, out iterEnd);
                var iterStartLoc = Editor.GetIterLocation(iterStart);
                var iterEndLoc = Editor.GetIterLocation(iterEnd);

                var selectionSize = (iterEndLoc.X - iterStartLoc.X) / 2;

                int window_x;
                int window_y;
                Editor.BufferToWindowCoords(TextWindowType.Widget, iterStartLoc.X, iterStartLoc.Y, out window_x, out window_y);

                formatPopover.PointingTo = new Gdk.Rectangle(window_x + selectionSize, window_y, selectionSize, iterEndLoc.Height);
                formatPopover.Popup();
            }
        }

        public async void SaveAs(object sender, EventArgs e)
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
                    Document.FilePath = new Uri(dlg.Filename);
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

                    Document = new Document()
                    {
                        FilePath = new Uri(dlg.Filename),
                    };

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