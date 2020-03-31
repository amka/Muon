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
            SaveButton = new Button("document-save-as", IconSize.LargeToolbar);
            SaveButton.TooltipMarkup = "Save document as...";

            PackStart(OpenButton);
            PackStart(SaveButton);

            menuButton = new MenuButton();
            menuButton.Image = Image.NewFromIconName("open-menu", IconSize.LargeToolbar);
            menuButton.TooltipText = "Menu";
            menuButton.ShowAll();
            PackEnd(menuButton);

            // Connect Popover
            var popover = new MenuPopover(menuButton);
            menuButton.Popover = popover;
        }
    }
}