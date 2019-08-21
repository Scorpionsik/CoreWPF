namespace CoreWPF.Windows
{
    public partial class DialogWindowExt : WindowExt
    {
        public DialogWindowExt() : base() { }

        public new bool? Show()
        {
            return this.ShowDialog();
        }

        public void Save()
        {
            this.DialogResult = true;
        }

        public new void Close()
        {
            this.DialogResult = false;
        }
    }
}
