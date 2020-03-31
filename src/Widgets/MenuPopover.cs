using System;
using Gtk;

namespace Muon.Widgets
{
    public class MenuPopover : Popover
    {
        public event EventHandler PreferenceClicked;
        public event EventHandler ZoomOutClicked;
        public event EventHandler ZoomDefaultClicked;
        public event EventHandler ZoomInClicked;

        public MenuPopover(Widget relativeTo) : base(relativeTo)
        {
            // Init menu action buttons
            var zoomOutButton = new Button("zoom-out-symbolic", IconSize.Menu);
            zoomOutButton.ActionName = "win.zoom_out";

            var zoomDefaultButton = new Button("100%");
            zoomDefaultButton.ActionName = "win.zoom_reset";

            var zoomInButton = new Button("zoom-in-symbolic", IconSize.Menu);
            zoomInButton.ActionName = "win.zoom_in";

            var fontSizeGrid = new Grid()
            {
                ColumnHomogeneous = true,
                Hexpand = true,
                Margin = 12,
            };
            fontSizeGrid.StyleContext.AddClass("linked");
            fontSizeGrid.Add(zoomOutButton);
            fontSizeGrid.Add(zoomDefaultButton);
            fontSizeGrid.Add(zoomInButton);

            var menuSeparator = new Separator(Orientation.Horizontal);
            menuSeparator.MarginTop = 6;
            menuSeparator.MarginBottom = 6;

            var prefButton = new ModelButton();
            prefButton.Text = "Preferences";
            // prefButton.ActionName = "win.open_preferences";
            prefButton.Clicked += (sender, args) => {
                Console.WriteLine("Pref clicked");
                var dlg = new PreferencesDialog();
                dlg.Run();
            };

            var menuGrid = new Grid()
            {
                MarginBottom = 3,
                Orientation = Orientation.Vertical,
                WidthRequest = 200,
            };

            menuGrid.Attach(fontSizeGrid, 0, 0, 3, 1);
            menuGrid.Attach(menuSeparator, 0, 2, 3, 1);
            menuGrid.Attach(prefButton, 0, 3, 3, 1);
            
            menuGrid.ShowAll();

            Add(menuGrid);
        }
    }
}