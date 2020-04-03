using System;
using Gtk;

namespace Muon.Widgets
{
    public class Sidebar : Grid
    {
        public ActionBar ActionBar;
        public DocumentsList DocumentsList;

        public Sidebar()
        {
            Orientation = Orientation.Vertical;

            DocumentsList = new DocumentsList();

            var addButton = new Button();
            addButton.Image = Image.NewFromIconName("list-add-symbolic", IconSize.SmallToolbar);
            addButton.ActionName = "document.create";

            var removeButton = new Button();
            removeButton.Image = Image.NewFromIconName("list-remove-symbolic", IconSize.SmallToolbar);
            removeButton.ActionName = "document.remove";

            ActionBar = new ActionBar();
            ActionBar.Add(addButton);
            ActionBar.Add(removeButton);

            var scrolled = new ScrolledWindow();
            scrolled.Add(DocumentsList);

            Add(scrolled);
            Add(ActionBar);
        }
    }
}