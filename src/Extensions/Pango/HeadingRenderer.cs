// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.
using System.Globalization;
using Markdig.Syntax;

namespace Markdig.Renderers.Pango
{
    /// <summary>
    /// An HTML renderer for a <see cref="HeadingBlock"/>.
    /// </summary>
    /// <seealso cref="Markdig.Renderers.Pango.HtmlObjectRenderer{Markdig.Syntax.HeadingBlock}" />
    public class HeadingRenderer : HtmlObjectRenderer<HeadingBlock>
    {
        private static readonly string[] HeadingTexts = {
            "xx-large",
            "x-large",
            "medium",
            "h4",
            "h5",
            "h6",
        };

        protected override void Write(HtmlRenderer renderer, HeadingBlock obj)
        {
            int index = obj.Level - 1;
            string headingText = "span class=" + (((uint)index < (uint)HeadingTexts.Length)
                ? HeadingTexts[index] : "");

            if (renderer.EnableHtmlForBlock)
            {
                renderer.Write("<").Write(headingText).WriteAttributes(obj).Write(">");
            }

            renderer.WriteLeafInline(obj);

            if (renderer.EnableHtmlForBlock)
            {
                renderer.Write("</").Write(headingText).WriteLine(">");
            }

            renderer.EnsureLine();
        }
    }
}