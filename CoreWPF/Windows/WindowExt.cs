using CoreWPF.MVVM;
using System;
using System.Windows;

namespace CoreWPF.Windows
{
    public partial class WindowExt : Window
    {
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
                    base.DataContext = vm;
                }
                else base.DataContext = value;
            }
        } //---свойство DataContext

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WindowExt"/>
        /// </summary>
        public WindowExt() : base() { }

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
    } //-класс WindowExt

    public partial class WindowExt<T> : WindowExt
    {
        public new object DataContext
        {
            get { return base.DataContext; }
            set
            {
                if(value is ViewModel<T>)
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
    }
}
