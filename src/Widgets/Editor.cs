using System;
using System.IO;
using System.Collections.Generic;
using Gtk;
using Norka.Models;
using Norka.Services;
using AutoDI;

namespace Norka.Widgets
{
    public class Editor
    {
        readonly IDocumentsStorage storage;

        Window Parent;
        TextBuffer TextBuffer;
        EditorView EditorView;
        FormatBar FormatBar;
        Revealer FormatRevealer;

        public Box View;
        public Document Document;

        public event EventHandler Opened = delegate { };
        public event EventHandler Saved = delegate { };

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
            View = new Box(Orientation.Vertical, 0);

            // Load services
            storage = GlobalDI.GetService<IDocumentsStorage>();

            // Init widgets
            var scrolled = new ScrolledWindow()
            {
                Expand = true
            };
            View.StyleContext.AddClass("view");

            FormatBar = new FormatBar(Orientation.Horizontal, 6);
            FormatRevealer = new Revealer();
            FormatRevealer.Add(FormatBar);

            FormatRevealer.RevealChild = false;

            TextTagTable tagTable = new TextTagTable();
            foreach (var tag in Tags)
            {
                tagTable.Add(tag.Value);
            }
            TextBuffer = new TextBuffer(tagTable);
            EditorView = new EditorView(TextBuffer);
            scrolled.Add(EditorView);

            View.PackStart(FormatRevealer, false, true, 0);
            View.PackEnd(scrolled, true, true, 0);
        }

        public void ToggleFormatBar()
        {
            FormatRevealer.RevealChild = !FormatRevealer.RevealChild;
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

        internal void ClearTags()
        {
            TextIter start, end;
            var hasBounds = TextBuffer.GetSelectionBounds(out start, out end);
            if (!hasBounds)
            {
                start = TextBuffer.StartIter;
                end = TextBuffer.EndIter;
            }

            TextBuffer.RemoveAllTags(start, end);
        }

        internal void LoadDocument(int docId)
        {
            if (Document != null)
            {
                Document.Content = TextBuffer.Text;
                storage.UpdateDocument(Document);
            }
            Document = storage.DocumentById(docId);
            TextBuffer.Text = Document.Content ?? "";
        }
    }
}