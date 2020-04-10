using Gtk;

namespace Norka.Widgets
{
    public class DocumentListRow : Grid
    {
        [GLib.Property("DocumentId")]
        public int DocumentId;

        public DocumentListRow(string title)
        {
            var label = new Label(title)
            {
                Vexpand = true,
                Valign = Align.Center,
                Ellipsize = Pango.EllipsizeMode.End,
                TooltipText = title,
            };

            MarginStart = 6;
            MarginEnd = 6;
            MarginBottom = 4;
            MarginTop = 4;
            Add(label);
        }
    }
}