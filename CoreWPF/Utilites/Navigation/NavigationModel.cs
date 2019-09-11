using CoreWPF.MVVM;
using CoreWPF.Utilites.Navigation.Interfaces;

namespace CoreWPF.Utilites.Navigation
{
    public abstract partial class NavigationModel : Model, INavigateModule
    {
        public NavigationManager Navigator { get; private set; }
        public string Subtitle { get; private set; }

        public NavigationModel(string title, NavigationManager navigator) : base()
        {
            this.Subtitle = title;
            this.Navigator = navigator;
        }

        public abstract void OnNavigatingFrom();
        public abstract void OnNavigatingTo(object arg);
    }
}
