using System;
using Gtk;

namespace Norka.Models
{
    public class TextAction
    {
        public string ActionName { get; set; }
        public TextIter StartIter { get; set; }
        public TextIter EndIter { get; set; }

        public TextAction(string ActionName, TextIter StartIter, TextIter EndIter)
        {
            this.ActionName = ActionName;
            this.StartIter = StartIter;
            this.EndIter = EndIter;
        }
    }
}