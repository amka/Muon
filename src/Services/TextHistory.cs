using System;
using System.Collections.Generic;
using Gtk;
using Norka.Models;

namespace Norka.Services
{
    public class TextHistory : ITextHistory
    {

        List<TextAction> Actions;
        int CurrentActionIndex = 0;

        public int MaxActions = 200;

        public TextHistory()
        {
            Actions = new List<TextAction>(MaxActions);
        }

        public void Push(TextAction action)
        {
            if (CurrentActionIndex < MaxActions)
            {
                CurrentActionIndex++;
            }
            else
            {
                Actions.RemoveAt(0);
            }
            Actions.Insert(CurrentActionIndex, action);
        }

        public TextAction Pop()
        {
            TextAction action = null;
            if (CurrentActionIndex > 0)
            {
                action = Actions[CurrentActionIndex];
                CurrentActionIndex--;
            }
            return action;
        }
    }
}