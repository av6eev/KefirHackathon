using System;
using Dialogs.Specification;

namespace Dialogs
{
    public abstract class DialogModel : IDialogModel
    {
        public event Action OnClose;
        
        public DialogSpecification Specification { get; }
        public bool IsOpened { get; set; }

        protected DialogModel(DialogSpecification specification)
        {
            Specification = specification;
        }

        public void Close()
        {
            IsOpened = false;
            OnClose?.Invoke();
        }
    }
}