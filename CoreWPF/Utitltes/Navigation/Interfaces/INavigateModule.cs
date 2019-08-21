namespace CoreWPF.Utitltes.Navigation.Interfaces
{
    public interface INavigateModule
    {
        string Subtitle { get; }
        void OnNavigatingTo(object arg);
        void OnNavigatingFrom();
    }
}
