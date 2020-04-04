using System;
using Gtk;

namespace Norka.Widgets
{
    public class EditorView : TextView
    {
        public EditorView(TextBuffer buffer) : base(buffer)
        {
            Margin = 6;
            WrapMode = WrapMode.Word;
            Vexpand = true;
        }
    }
}