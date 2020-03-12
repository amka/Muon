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

            OpenButton = new Button("document-open", IconSize.SmallToolbar);
            SaveButton = new Button("document-save", IconSize.SmallToolbar)
            {
                Sensitive = false,
            };

            PackStart(OpenButton);
            PackStart(SaveButton);

            var menuButton = new MenuButton();
            PackEnd(menuButton);
        }
    }
}