using Gtk;

namespace Muon.Widgets
{
    public class Header : HeaderBar
    {
        public Button OpenButton;
        public Button SaveButton;
        public Button MenuButton => menuButton;
        MenuButton menuButton;

        public Header()
        {
            Title = "Muon";
            Subtitle = "";
            HasSubtitle = Subtitle.Length > 0;
            ShowCloseButton = true;
            // StyleContext.AddClass("default-decoration");

            OpenButton = new Button("document-open", IconSize.LargeToolbar);
            OpenButton.TooltipMarkup = "Open document";
            SaveButton = new Button("document-save", IconSize.LargeToolbar);
            SaveButton.TooltipMarkup = "Save document as...";

            PackStart(OpenButton);
            PackEnd(SaveButton);

            // Init menu action buttons
            var zoomOutButton = new Button("zoom-out-symbolic", IconSize.Menu)
            {
                ActionName = "win.zoom_out",
            };

            var zoomDefaultButton = new Button("100%")
            {
                ActionName = "win.zoom_reset",
            };

            var zoomInButton = new Button("zoom-in-symbolic", IconSize.Menu)
            {
                ActionName = "win.zoom_in",
            };

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
            menuSeparator.MarginTop = 12;

            var menuGrid = new Grid()
            {
                MarginBottom = 3,
                Orientation = Orientation.Vertical,
                WidthRequest = 200,
            };

            menuGrid.Attach(fontSizeGrid, 0, 0, 3, 1);
            menuGrid.Attach(menuSeparator, 0, 2, 3, 1);
            menuGrid.ShowAll();

            menuButton = new MenuButton();
            menuButton.Image = new Image("open-menu-symbolic", IconSize.LargeToolbar);
            menuButton.TooltipText = "Menu";
            menuButton.ShowAll();
            PackEnd(menuButton);

            var popover = new Popover(MenuButton);
            popover.Add(menuGrid);
            menuButton.Popover = popover;
        }
    }
}