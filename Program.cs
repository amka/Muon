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

            var app = new Application("org.pico.pico", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow(app);

            win.ShowAll();
            Application.Run();
        }
    }
}
