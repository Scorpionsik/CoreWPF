using CoreWPF.MVVM;
using CoreWPF.Windows.Enums;
using System;
using System.Windows;

namespace CoreWPF.Windows
{
    public partial class WindowExt : Window
    {
        private event Func<WindowClose> VMClosed;
        /// <summary>
        /// Получает или задает контекст данных для элемента, участвующего в привязке данных.
        /// </summary>
        public new object DataContext
        {
            get { return base.DataContext; }
            set
            {
                if (value is ViewModel vm)
                {
                    vm.Event_close += new Action(this.Close);
                    vm.Event_minimized += new Action(this.WinExtMinimized);
                    vm.Event_state += new Action(this.WinExtState);
                    this.VMClosed += new Func<WindowClose>(vm.CloseMethod);
                    base.DataContext = vm;
                }
                else base.DataContext = value;
            }
        } //---свойство DataContext

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WindowExt"/>
        /// </summary>
        public WindowExt() : base()
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ClosingMethod);
        }

        /// <summary>
        /// Метод для сворачивания текущего окна
        /// </summary>
        public void WinExtMinimized()
        {
            this.WindowState = WindowState.Minimized;
        } //---метод WinExtMinimized

        /// <summary>
        /// Метод для развертывания (и обратно) текущего окна
        /// </summary>
        public void WinExtState()
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        } //---метод WinExtState

        private void ClosingMethod(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(this.VMClosed != null)
            {
                switch (this.VMClosed())
                {
                    case WindowClose.Abort:
                        e.Cancel = true;
                        break;
                    default:
                        e.Cancel = false;
                        break;
                }
            }
        }
    } //-класс WindowExt

    public partial class WindowExt<T> : WindowExt
    {
        public new object DataContext
        {
            get { return base.DataContext; }
            set
            {
                if (value is ViewModel<T> vm)
                {
                    base.DataContext = vm;
                }
                else base.DataContext = value;
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

        public WindowExt() : base() { }
    }
}
