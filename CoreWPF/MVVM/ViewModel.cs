using CoreWPF.Utilites;
using CoreWPF.Windows;
using CoreWPF.Windows.Enums;
using System;
using System.Windows.Threading;

namespace CoreWPF.MVVM
{
    /// <summary>
    /// Класс, который автоматически привязывается к событиям <see cref="WindowExt"/>
    /// </summary>
    [Serializable]
    public abstract class ViewModel : NotifyPropertyChanged
    {
        private Dispatcher dispatcher;
        public Dispatcher Dispatcher
        {
            set
            {
                this.dispatcher = value;
            }
        }

        private event Action event_close;
        public virtual Action Event_close
        {
            get { return this.event_close; }
            set
            {
                this.event_close = new Action(value);
            }
        }

        private event Action event_save;
        public virtual Action Event_save
        {
            get { return this.event_save; }
            set
            {
                this.event_save = new Action(value);
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

        public virtual RelayCommand Command_save
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    this.Event_save?.Invoke();
                });
            }
        }

        public void InvokeInMainThread(Action action)
        {
            this.dispatcher.Invoke(action);
        }
    }

    public abstract partial class ViewModel<T> : ViewModel
    {
        public virtual T ReturnResult { get; private set; }
    }
}
