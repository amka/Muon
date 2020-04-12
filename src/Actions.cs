using Gtk;
using GLib;
using System.Collections.Generic;

namespace Norka
{
    public static class Actions
    {
        static Dictionary<string, string> FormatActionMap = new Dictionary<string, string>(){
            {"app.quit", "<Control>Q"},
            {"document.create", "<Control>n"},
            {"document.open", "<Control>o"},
            {"document.remove", "<Shift><Del>"},
            {"document.save", "<Control>s"},
            {"document.save-as", "<Control><Shift>s"},
            {"format.bold", "<Control>b"},
            {"format.italic", "<Control>i"},
            {"format.underline", "<Control>u"},
            {"format.clear", ""},
            {"format.toggle", ""},
            {"justify.left", "<Control>b"},
            {"justify.right", "<Control>i"},
            {"justify.center", "<Control>u"},
            {"justify.fill", "<Control>u"},
            {"font.size-up", "<Control>plus"},
            {"font.size-down", "<Control>minus"},
            {"font.size-reset", "<Control>0"},
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