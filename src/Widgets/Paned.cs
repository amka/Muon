using System;
using Gtk;

namespace Muon.Widgets
{
    public class Paned : Gtk.Paned
    {
        public Sidebar Sidebar;
        public Editor Editor;

        public Paned(Orientation orientation, Window parentWindow) : base(orientation)
        {
            Sidebar = new Sidebar();

            Editor = new Editor(parentWindow);

            Pack1(Sidebar, false, false);
            Pack2(Editor.View, true, false);

            // SetSizeRequest(200, -1);
            Sidebar.SetSizeRequest(196, -1);
            Editor.View.SetSizeRequest(320, -1);
        }
    }
}