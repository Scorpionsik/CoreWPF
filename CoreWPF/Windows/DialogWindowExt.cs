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
                else base.DataContext = value;
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
}
