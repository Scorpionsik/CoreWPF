using CoreWPF.MVVM;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWPF.Utilites
{
    /// <summary>
    /// Строка с исчезающим со временем текстом; реализует интерфейс <see cref="INotifyPropertyChanged"/>
    /// </summary>
    /// <remarks>
    /// Удобно использовать в качестве статус-бара окна.
    /// </remarks>
    [Serializable]
    public class StatusString : NotifyPropertyChanged
    {
        /// <summary>
        /// Константа для метода <see cref="SetAsync(string, double)"/>; задает 5-секундный интервал отображения текста.
        /// </summary>
        public static double LongTime = 5000;

        /// <summary>
        /// Константа для метода <see cref="SetAsync(string, double)"/>; задает 3-секундный интервал отображения текста.
        /// </summary>
        public static double ShortTime = 3000;

        /// <summary>
        /// Константа для метода <see cref="SetAsync(string, double)"/>; текст не исчезнет, пока вы не вызовите метод <see cref="ClearAsync"/> или заново <see cref="SetAsync(string, double)"/>.
        /// </summary>
        public static double Infinite = 0;

        private string status;
        /// <summary>
        /// Строка для отображения статуса
        /// </summary>
        public string Status
        {
            get { return this.status; }
            private set
            {
                this.status = value;
                this.OnPropertyChanged("Status");
            }
        } //---свойство Status

        /// <summary>
        /// Метод для установки текста
        /// </summary>
        /// <param name="status">Текст для отображения</param>
        /// <param name="milliseconds">Интервал отображения текста; можно использовать константы <see cref="LongTime"/>, <see cref="ShortTime"/> и <see cref="Infinite"/>.</param>
        public async void SetAsync(string status, double milliseconds)
        {
            await Task.Run(() =>
            {
                this.Status = status;
                if (milliseconds > 0)
                {
                    Thread.Sleep((int)milliseconds);
                    this.Status = "";
                }
            });
        } //---метод SetAsync

        /// <summary>
        /// Метод для стирания текста
        /// </summary>
        public async void ClearAsync()
        {
            await Task.Run(() =>
            {
                this.Status = "";
            });
        } //---метод ClearAsync
    } //---класс StatusString
} //---пространство имён CoreWPF.Utilites
//---EOF