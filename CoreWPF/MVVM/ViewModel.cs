using CoreWPF.Utilites;
using CoreWPF.Windows;
using CoreWPF.Windows.Enums;
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

        public virtual WindowClose CloseMethod()
        {
            return WindowClose.Confirm;
        }

        public virtual RelayCommand Command_close
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    this.Event_close?.Invoke();
                });
            }
        }

        public virtual RelayCommand Command_minimized
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    this.Event_minimized?.Invoke();
                });
            }
        }

        public virtual RelayCommand Command_state
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    this.Event_state?.Invoke();
                });
            }
        }
    }

    public abstract partial class ViewModel<T> : ViewModel
    {
        public virtual T ReturnResult { get; private set; }

        protected void SetReturnResult(T result)
        {
            this.ReturnResult = result;
        }
    }
}
