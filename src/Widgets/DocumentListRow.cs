using Gtk;

namespace Norka.Widgets
{
    public class DocumentListRow : Grid
    {
        [GLib.Property("DocumentId")]
        public int DocumentId;

        public DocumentListRow(string title)
        {
            var label = new Label();
            label.Markup = $"<b>{title}</b>";
            label.Vexpand = true;
            label.Valign = Align.Center;

            Margin = 6;
            Add(label);
        }
    }
}