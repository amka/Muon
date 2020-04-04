using System;
using Gtk;

namespace Norka
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            AutoDI.Generated.AutoDI.Init();
            Application.Init();

            var app = new Application("com.github.amka.norka", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            Actions.SetupFormatActions(app);

            var win = new MainWindow(app);

            // Handle Quit action
            var actionQuit = new GLib.SimpleAction("quit", null);
            actionQuit.Activated += (object sender, GLib.ActivatedArgs args) =>
            {
                Application.Quit();
            };
            app.AddAction(actionQuit);

            win.ShowAll();
            Application.Run();
        }
    }
}
