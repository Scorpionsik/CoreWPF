using CoreWPF.MVVM;
using System;

namespace CoreWPF.Windows
{
    public partial class DialogWindowExt : WindowExt
    {
        public new object DataContext
        {
            get { return base.DataContext; }
            set
            {
                if (value is ViewModel vm)
                {
                    vm.Event_save += new Action(this.Save);
                    base.DataContext = vm;
                }
            }
        }

        public DialogWindowExt() : base() { }

        public new bool? Show()
        {
            return this.ShowDialog();
        }

        public void Save()
        {
            this.DialogResult = true;
        }
    }

    public partial class DialogWindowExt<T> : DialogWindowExt
    {
        public new object DataContext
        {
            get { return base.DataContext; }
            set
            {
                if (value is ViewModel<T>)
                {
                    base.DataContext = value;
                }
            }
        }

        public virtual T ReturnResult
        {
            get
            {
                if (this.DataContext is ViewModel<T>) return ((ViewModel<T>)this.DataContext).ReturnResult;
                else return default(T);
            }
        }

        public DialogWindowExt() : base() { }
    }
}
