using System;
using Gtk;

namespace Muon
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Application("com.github.amka.muon", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            Actions.SetupFormatActions(app);

            var win = new MainWindow(app);

            win.ShowAll();
            Application.Run();
        }
    }
}
