using System;
using Gtk;

namespace Muon.Widgets
{
    public class EditPopover : Popover
    {
        public EditPopover(Widget relative_to) : base(relative_to)
        {
            var vbox = new Box(Orientation.Vertical, 6);
            vbox.Margin = 6;

            // Justify Bar
            var justifyGrid = new Grid();
            justifyGrid.Orientation = Orientation.Horizontal;
            justifyGrid.StyleContext.AddClass("linked");
            // justifyGrid.Margin = 6;

            var JustifytLeftButton = new Button();
            JustifytLeftButton.Image = Image.NewFromIconName("format-justify-left", IconSize.LargeToolbar);

            var JustifyRightButton = new Button();
            JustifyRightButton.Image = Image.NewFromIconName("format-justify-right", IconSize.LargeToolbar);

            var JustifyCenterButton = new Button();
            JustifyCenterButton.Image = Image.NewFromIconName("format-justify-center", IconSize.LargeToolbar);

            var JustifyFillButton = new Button();
            JustifyFillButton.Image = Image.NewFromIconName("format-justify-fill", IconSize.LargeToolbar);

            justifyGrid.Attach(JustifytLeftButton, 0, 0, 1, 1);
            justifyGrid.Attach(JustifyCenterButton, 1, 0, 1, 1);
            justifyGrid.Attach(JustifyRightButton, 2, 0, 1, 1);
            justifyGrid.Attach(JustifyFillButton, 3, 0, 1, 1);

            // Format Bar
            var formatGrid = new Grid();
            formatGrid.Orientation = Orientation.Horizontal;
            formatGrid.StyleContext.AddClass("linked");
            // formatGrid.Margin = 6;

            var BoldButton = new Button();
            BoldButton.Image = Image.NewFromIconName("format-text-bold", IconSize.LargeToolbar);

            var ItalicButton = new Button();
            ItalicButton.Image = Image.NewFromIconName("format-text-italic", IconSize.LargeToolbar);

            var UnderlineButton = new Button();
            UnderlineButton.Image = Image.NewFromIconName("format-text-underline", IconSize.LargeToolbar);

            var StrikeButton = new Button();
            StrikeButton.Image = Image.NewFromIconName("format-text-strikethrough", IconSize.LargeToolbar);

            formatGrid.Attach(BoldButton, 0, 0, 1, 1);
            formatGrid.Attach(ItalicButton, 1, 0, 1, 1);
            formatGrid.Attach(UnderlineButton, 2, 0, 1, 1);
            formatGrid.Attach(StrikeButton, 3, 0, 1, 1);

            // Compose Popover grid
            vbox.Add(justifyGrid);
            vbox.Add(formatGrid);
            vbox.ShowAll();
            
            Add(vbox);
        }
    }
}