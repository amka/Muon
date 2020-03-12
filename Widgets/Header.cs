using Gtk;

namespace Muon.Widgets
{
    public class Header : HeaderBar
    {
        public Button OpenButton;
        public Button SaveButton;


        public Header()
        {
            Title = "Muon";
            Subtitle = "the editor";
            HasSubtitle = Subtitle.Length > 0;
            ShowCloseButton = true;
            StyleContext.AddClass("default-decoration");

            OpenButton = new Button("document-open", IconSize.LargeToolbar);
            OpenButton.TooltipMarkup = "Open document\n<span weight='600' size='smaller' alpha='75%'>Ctrl+O</span>";
            SaveButton = new Button("document-save", IconSize.LargeToolbar);
            SaveButton.TooltipMarkup = "Save document as...\n<span weight='600' size='smaller' alpha='75%'>Ctrl+S</span>";

            PackStart(OpenButton);
            PackStart(SaveButton);

            var menuButton = new MenuButton();
            PackEnd(menuButton);
        }
    }
}