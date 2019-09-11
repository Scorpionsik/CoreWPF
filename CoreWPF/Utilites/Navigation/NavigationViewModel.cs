using CoreWPF.MVVM;
using CoreWPF.Utilites.Navigation.Interfaces;
using System;

namespace CoreWPF.Utilites.Navigation
{
    public abstract partial class NavigationViewModel : ViewModel
    {
        private readonly NavigationManager navigator;
        private string subtitle;
        public string Subtitle
        {
            get { return this.subtitle; }
            set
            {
                this.subtitle = value;
                this.OnPropertyChanged("Title");
            }
        }

        public override string Title
        {
            get
            {
                string tmp_send = base.Title;
                if(this.Subtitle != null && this.Subtitle != "") tmp_send += " [" + this.Subtitle + "]";
                return tmp_send;
            }
            set
            {
                base.Title = value;
                this.OnPropertyChanged("Title");
            }
        }

        public NavigationViewModel(NavigationManager navigator) : base()
        {
            navigator.Navigation_invoke = new Action<INavigateModule>(this.SetContent);
            this.navigator = navigator;
        }

        public abstract void SetContent(INavigateModule module);
    }
}
