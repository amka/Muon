

using Norka.Models;

namespace Norka.Services
{
    interface ITextHistory
    {
        void Push(TextAction action);
        TextAction Pop();
    }
}