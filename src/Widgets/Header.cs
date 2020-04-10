using System;
using Gtk;

namespace Norka.Widgets
{
    public class Header : HeaderBar
    {
        public Button OpenButton;
        public Button SaveButton;
        public ToggleButton FormatButton;
        public Button MenuButton => menuButton;
        MenuButton menuButton;

        public event EventHandler SearchToggled;
        public event EventHandler FormatToggled;

        public Header()
        {
            Title = "Norka";
            Subtitle = "";
            HasSubtitle = Subtitle.Length > 0;
            ShowCloseButton = true;
            //StyleContext.AddClass("default-decoration");

            var searchButton = new ToggleButton();
            searchButton.Image = Image.NewFromIconName("edit-find", IconSize.LargeToolbar);
            searchButton.ActionName = "app.toggle-search";

            FormatButton = new ToggleButton();
            FormatButton.Image = Image.NewFromIconName("insert-text", IconSize.LargeToolbar);
            FormatButton.ActionName = "format.toggle";

            menuButton = new MenuButton();
            menuButton.Image = Image.NewFromIconName("open-menu", IconSize.LargeToolbar);
            menuButton.TooltipText = "Menu";
            menuButton.ShowAll();

            // PackStart(OpenButton);
            // PackStart(SaveButton);
            PackEnd(menuButton);
            PackEnd(searchButton);
            PackEnd(FormatButton);

            // Connect Popover
            var popover = new MenuPopover(menuButton);
            menuButton.Popover = popover;
        }
    }
}