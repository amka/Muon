using System;
using System.IO;
using System.Collections.Generic;
using Gtk;
using Muon.Models;

namespace Muon.Widgets
{
    public class Editor
    {
        TextBuffer TextBuffer;
        EditorView EditorView;
        public ScrolledWindow View;
        Window Parent;
        public Document Document;


        public event EventHandler Opened;
        public event EventHandler Saved;

        Dictionary<string, TextTag> Tags = new Dictionary<string, TextTag>(){
            {"bold", new TextTag("bold"){Weight=Pango.Weight.Bold}},
            {"italic", new TextTag("italic"){Style=Pango.Style.Italic}},
            {"underline", new TextTag("underline"){Underline=Pango.Underline.Single}},
            {"justify-left", new TextTag("justify-left"){Justification=Justification.Left}},
            {"justify-right", new TextTag("justify-right"){Justification=Justification.Right}},
            {"justify-center", new TextTag("justify-center"){Justification=Justification.Center}},
            {"justify-fill", new TextTag("justify-fill"){Justification=Justification.Fill}},
        };

        public Editor(Window parent)
        {
            Parent = parent;

            TextTagTable tagTable = new TextTagTable();
            foreach (var tag in Tags)
            {
                tagTable.Add(tag.Value);
            }
            TextBuffer = new TextBuffer(tagTable);
            EditorView = new EditorView(TextBuffer);

            View = new ScrolledWindow()
            {
                Expand = true
            };
            View.StyleContext.AddClass("view");
            View.Add(EditorView);

            Document = new Document();

            // Editor.KeyReleaseEvent += KeyPressReleased;
            // EditorView.PopulatePopup += PopulatePopup;
        }


        private void KeyPressReleased(object o, KeyReleaseEventArgs args)
        {
            Console.WriteLine($"KeyReleased: {args.Event.Key}");
            if (args.Event.Key == Gdk.Key.Control_L && EditorView.Buffer.HasSelection)
            {
                var formatPopover = new EditPopover(EditorView);

                TextIter iterStart;
                TextIter iterEnd;
                TextBuffer.GetSelectionBounds(out iterStart, out iterEnd);
                var iterStartLoc = EditorView.GetIterLocation(iterStart);
                var iterEndLoc = EditorView.GetIterLocation(iterEnd);

                var selectionSize = (iterEndLoc.X - iterStartLoc.X) / 2;

                int window_x;
                int window_y;
                EditorView.BufferToWindowCoords(TextWindowType.Widget, iterStartLoc.X, iterStartLoc.Y, out window_x, out window_y);

                formatPopover.PointingTo = new Gdk.Rectangle(window_x + selectionSize, window_y, selectionSize, iterEndLoc.Height);
                formatPopover.Popup();
            }
        }

        internal void ToggleTag(string tagName)
        {
            var tag = Tags[tagName];
            TextIter start, end;
            var hasBounds = TextBuffer.GetSelectionBounds(out start, out end);
            if (hasBounds)
            {
                if (start.HasTag(tag))
                {
                    TextBuffer.RemoveTag(tag, start, end);
                }
                else
                {
                    TextBuffer.ApplyTag(tag, start, end);
                }
            }
        }

        internal void ClearTags() {
            TextIter start, end;
            var hasBounds = TextBuffer.GetSelectionBounds(out start, out end);
            if (!hasBounds)
            {
                start = TextBuffer.StartIter;
                end = TextBuffer.EndIter;
            }

            TextBuffer.RemoveAllTags(start, end);
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