using System;
using Gtk;

namespace Norka.Widgets
{
    public class SearchDialog : Dialog
    {
        Entry entry;
        EntryCompletion completion;

        public SearchDialog()
        {
            Modal = true;
            WindowPosition = WindowPosition.CenterOnParent;

            entry = new Entry();
            entry.Margin = 6;
            entry.PlaceholderText = "Search for document";

            completion = new Gtk.EntryCompletion();
            entry.Completion = completion;

            // Create, fill & register a ListStore:
            Gtk.ListStore list_store = new Gtk.ListStore(typeof(string), typeof(string));
            completion.Model = list_store;
            completion.TextColumn = 0;

            var cell = new Gtk.CellRendererText();
            completion.PackStart(cell, false);
            completion.AddAttribute(cell, "text", 1);

            Gtk.TreeIter iter;

            iter = list_store.Append();
            list_store.SetValue(iter, 0, "Burgenland");
            iter = list_store.Append();
            list_store.SetValue(iter, 0, "Berlin");
            iter = list_store.Append();
            list_store.SetValue(iter, 0, "Lower Austria");
            iter = list_store.Append();
            list_store.SetValue(iter, 0, "Upper Austria");
            iter = list_store.Append();
            list_store.SetValue(iter, 0, "Salzburg");
            iter = list_store.Append();
            list_store.SetValue(iter, 0, "Styria");
            iter = list_store.Append();
            list_store.SetValue(iter, 0, "Tehran");
            iter = list_store.Append();
            list_store.SetValue(iter, 0, "Vorarlberg");
            iter = list_store.Append();
            list_store.SetValue(iter, 0, "Vienna");

            var grid = new Grid();
            grid.Add(entry);
            grid.ShowAll();

            ContentArea.Add(grid);
        }
    }
}