using Gtk;
using GLib;

namespace Muon.Widgets
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