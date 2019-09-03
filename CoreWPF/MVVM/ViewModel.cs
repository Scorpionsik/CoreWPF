using CoreWPF.Windows;
using System;

namespace CoreWPF.MVVM
{
    /// <summary>
    /// Класс, который автоматически привязывается к событиям <see cref="WindowExt"/>
    /// </summary>
    public abstract partial class ViewModel : NotifyPropertyChanged
    {
        private event Action event_close;
        public virtual Action Event_close
        {
            get { return this.event_close; }
            set
            {
                this.event_close = new Action(value);
            }
        }

        private event Action event_minimized;
        public virtual Action Event_minimized
        {
            get { return this.event_minimized; }
            set
            {
                this.event_minimized = new Action(value);
            }
        }

        private event Action event_state;
        public virtual Action Event_state
        {
            get { return this.event_state; }
            set
            {
                this.event_state = new Action(value);
            }
        }

        private string title;
        public virtual string Title
        {
            get { return this.title; }
            set
            {
                this.title = value;
                this.OnPropertyChanged("Title");
            }
        }
    }

    public abstract partial class ViewModel<T> : ViewModel
    {
        public virtual T ReturnResult { get; set; }
    }
}
