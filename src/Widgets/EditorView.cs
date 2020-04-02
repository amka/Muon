using System;
using System.Text;
using Gtk;

namespace Muon.Widgets
{
    public class EditorView : TextView
    {
        public EditorView(TextBuffer buffer) : base(buffer)
        {
            Margin = 6;
            WrapMode = WrapMode.Word;
            Vexpand = true;

            var cssProvider = new CssProvider();
            cssProvider.LoadFromData(@"
            textview {
                font: 15 monospace;
            }");
            StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            // Buffer.RegisterDeserializeFormat("text/markdown", Markdown.Deserialize);
        }
    }
}