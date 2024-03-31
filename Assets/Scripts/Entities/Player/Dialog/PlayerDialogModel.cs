using System.Collections.Generic;

namespace Entities.Player.Dialog
{
    public class PlayerDialogModel
    {
        public readonly Queue<string> Queue = new();

        public void Add(string text)
        {
            Queue.Enqueue(text);
        }
    }
}