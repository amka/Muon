using Gtk;
using GLib;

namespace Norka.Widgets
{
    public class PreferencesDialog : Dialog
    {
        public PreferencesDialog()
        {
            DefaultSize = new Gdk.Size(360, 320);
            Title = "Preferences";
        }
    }
}