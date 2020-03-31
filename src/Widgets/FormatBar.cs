using System;
using Gtk;

namespace Muon.Widgets
{
    public class FormatBar : FlowBox
    {
        public FormatBar()
        {
            Hexpand = true;
            StyleContext.AddClass("search-bar");

            var label = new Label("Format Bar");
            label.Hexpand = true;
            label.Halign = Align.Center;
            label.Margin = 6;

            Add(label);
        }
    }
}