using System;
using Gtk;

namespace Muon.Widgets
{
    public class FormatBar : Box
    {
        public FormatBar(Orientation orientation, int spacing) : base(orientation, spacing)
        {
            StyleContext.AddClass("search-bar");

            var label = new Label("Format Bar");
            label.Hexpand = true;
            label.Halign = Align.Center;
            label.Margin = 6;

            // Format Bar
            var boldButton = new Button();
            boldButton.Image = Image.NewFromIconName("format-text-bold-symbolic", IconSize.SmallToolbar);
            boldButton.ActionName = "format.bold";

            var italicButton = new Button();
            italicButton.Image = Image.NewFromIconName("format-text-italic-symbolic", IconSize.SmallToolbar);
            italicButton.ActionName = "format.italic";

            var underlineButton = new Button();
            underlineButton.Image = Image.NewFromIconName("format-text-underline-symbolic", IconSize.SmallToolbar);
            underlineButton.ActionName = "format.underline";

            var formatGrid = new Grid();
            formatGrid.StyleContext.AddClass("linked");
            formatGrid.Add(boldButton);
            formatGrid.Add(italicButton);
            formatGrid.Add(underlineButton);

            // Justify Bar
            var justifyLeftButton = new Button();
            justifyLeftButton.Image = Image.NewFromIconName("format-justify-left-symbolic", IconSize.SmallToolbar);
            justifyLeftButton.ActionName = "win.justify-left";

            var justifyRightButton = new Button();
            justifyRightButton.Image = Image.NewFromIconName("format-justify-right-symbolic", IconSize.SmallToolbar);
            justifyRightButton.ActionName = "win.justify-right";

            var justifyCenterButton = new Button();
            justifyCenterButton.Image = Image.NewFromIconName("format-justify-center-symbolic", IconSize.SmallToolbar);
            justifyCenterButton.ActionName = "win.justify-center";

            var justifyGrid = new Grid();
            justifyGrid.StyleContext.AddClass("linked");
            justifyGrid.Add(justifyLeftButton);
            justifyGrid.Add(justifyCenterButton);
            justifyGrid.Add(justifyRightButton);

            // Remove formatting button
            var clearButton = new Button();
            clearButton.Image = Image.NewFromIconName("edit-clear-symbolic", IconSize.SmallToolbar);
            clearButton.ActionName = "win.format-clear";

            Add(formatGrid);
            Add(justifyGrid);
            PackEnd(clearButton, false, true, 0);
        }
    }
}