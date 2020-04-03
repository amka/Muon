using System;
using Gtk;

namespace Muon.Widgets
{
    public class DocumentsList : ListBox
    {
        public DocumentsList()
        {
            Hexpand = true;
            Vexpand = true;
        }

        public void AddItem(string title, int position = 0)
        {
            var label = new Label();
            label.Markup = $"<b>{title}</b>";
            label.Vexpand = true;
            label.Valign = Align.Center;

            var item = new Grid();
            item.Margin = 6;
            item.Add(label);

            Insert(item, position);
            ShowAll();
        }
    }
}