using CoreWPF.MVVM;
using CoreWPF.Utitltes.Navigation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWPF.Utitltes.Navigation
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
