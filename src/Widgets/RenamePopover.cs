using System;
using Gtk;

namespace Norka.Widgets
{
    public class RenamePopover : Popover
    {
        Entry entry;
        Button renameButton;
        string originTitle;

        public string Text => entry.Text;

        public event EventHandler RenameClicked = delegate { };

        public RenamePopover(Widget relative_to, string originTitle) : base(relative_to)
        {
            this.originTitle = originTitle;
            Position = PositionType.Right;

            var labelTitle = new Label()
            {
                Markup = $"<b>Rename to</b>",
                Halign = Align.Start,
                Hexpand = true,
                Ellipsize = Pango.EllipsizeMode.End,
                TooltipMarkup = $"Rename <b>{originTitle}</b> to",
            };

            entry = new Entry(originTitle);
            entry.Hexpand = true;
            entry.Activated += (sender, args) => RenameClicked(entry, EventArgs.Empty);
            entry.Changed += TextChanged;

            renameButton = new Button("Rename");
            renameButton.MarginStart = 6;
            renameButton.Sensitive = false;
            renameButton.StyleContext.AddClass("destructive-action");
            renameButton.Clicked += (sender, args) => RenameClicked(entry, EventArgs.Empty);

            var box = new Box(Orientation.Horizontal, 6);
            box.Hexpand = true;
            box.PackStart(entry, true, true, 0);
            box.PackEnd(renameButton, false, true, 0);

            var grid = new Box(Orientation.Vertical, 6);
            grid.Margin = 12;
            grid.Add(labelTitle);
            grid.Add(box);

            Add(grid);
            ShowAll();
        }

        void TextChanged(object sender, EventArgs e)
        {
            renameButton.Sensitive = originTitle != entry.Text;
        }

        protected override void OnShown()
        {
            base.OnShown();
            entry.GrabFocus();
        }
    }
}