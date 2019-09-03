using CoreWPF.MVVM;

namespace CoreWPF.Windows
{
    public partial class DialogWindowExt : WindowExt
    {
        public DialogWindowExt() : base() { }

        public new bool? Show()
        {
            return this.ShowDialog();
        }

        public void Save()
        {
            this.DialogResult = true;
        }

        public new void Close()
        {
            this.DialogResult = false;
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
