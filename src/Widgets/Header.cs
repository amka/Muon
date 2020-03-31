using System;
using Gtk;

namespace Muon.Widgets
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
            Title = "Muon";
            Subtitle = "";
            HasSubtitle = Subtitle.Length > 0;
            ShowCloseButton = true;
            // StyleContext.AddClass("default-decoration");

            OpenButton = new Button("document-open", IconSize.LargeToolbar);
            OpenButton.TooltipMarkup = "Open document";
            OpenButton.ActionName = "document.open";
            SaveButton = new Button("document-save-as", IconSize.LargeToolbar);
            SaveButton.TooltipMarkup = "Save document as...";
            SaveButton.ActionName = "document.save-as";

            var searchButton = new ToggleButton();
            searchButton.Image = Image.NewFromIconName("edit-find", IconSize.LargeToolbar);
            // searchButton.Toggled += SearchToggled;

            FormatButton = new ToggleButton();
            FormatButton.Image = Image.NewFromIconName("insert-text", IconSize.LargeToolbar);

            menuButton = new MenuButton();
            menuButton.Image = Image.NewFromIconName("open-menu", IconSize.LargeToolbar);
            menuButton.TooltipText = "Menu";
            menuButton.ShowAll();

            PackStart(OpenButton);
            PackStart(SaveButton);
            PackEnd(menuButton);
            PackEnd(searchButton);
            PackEnd(FormatButton);

            // Connect Popover
            var popover = new MenuPopover(menuButton);
            menuButton.Popover = popover;
        }
    }
}