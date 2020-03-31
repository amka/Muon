using Gtk;
using GLib;
using System.Collections.Generic;

namespace Muon
{
    public static class Actions
    {
        static Dictionary<string, string> FormatActionMap = new Dictionary<string, string>(){
            {"document.open", "<Control>o"},
            {"document.save", "<Control>s"},
            {"document.save-as", "<Control><Shift>s"},
            {"format.bold", "<Control>b"},
            {"format.italic", "<Control>i"},
            {"format.underline", "<Control>u"},
        };

        public static SimpleAction ActionFromGroup(string actionName, SimpleActionGroup actionGroup)
        {
            return ((SimpleAction)actionGroup.LookupAction(actionName));
        }

        public static void SetupFormatActions(Gtk.Application app)
        {
            foreach (var item in FormatActionMap)
            {
                // app.AddAction(new GLib.SimpleAction(item.Key, null));
                app.AddAccelerator(item.Value, item.Key, null);
            }
        }
    }
}