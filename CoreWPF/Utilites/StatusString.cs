using CoreWPF.MVVM;
using MessagePack;
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
    [MessagePackObject(keyAsPropertyName: true)]
    public class StatusString : NotifyPropertyChanged
    {
        /// <summary>
        /// Таймер для своевременного стирания текста.
        /// </summary>
        private Timer SingleTimer;

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
        /// Строка для отображения статуса.
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
        /// Метод для установки текста.
        /// </summary>
        /// <param name="status">Текст для отображения.</param>
        /// <param name="milliseconds">Интервал отображения текста; можно использовать константы <see cref="LongTime"/>, <see cref="ShortTime"/> и <see cref="Infinite"/>.</param>
        public async Task SetAsync(string status, double milliseconds)
        {
            await Task.Run(() =>
            {
                this.ClearTimer();
                this.Status = status;
                if (milliseconds > 0)
                {
                    this.SingleTimer = new Timer(new TimerCallback(this.Clear), null, (int)milliseconds, Timeout.Infinite);
                }
            });
        } //---метод SetAsync

        public void Set(string status, double milliseconds)
        {
            this.ClearTimer();
            this.Status = status;
            if (milliseconds > 0)
            {
                this.SingleTimer = new Timer(new TimerCallback(this.Clear), null, (int)milliseconds, Timeout.Infinite);
            }
        } //---метод SetAsync

        /// <summary>
        /// Метод для стирания текста.
        /// </summary>
        public async Task ClearAsync()
        {
            await Task.Run(() =>
            {
                this.ClearTimer();
                this.Status = "";
            });
        } //---метод ClearAsync

        public void Clear()
        {
            this.ClearTimer();
            this.Status = "";
        } //---метод ClearAsync

        /// <summary>
        /// Метод для стирания текста и очистки таймера; используется для <see cref="SingleTimer"/>.
        /// </summary>
        /// <param name="obj">Не используется; всегда получает null.</param>
        private void Clear(object obj)
        {
            this.ClearTimer();
            this.Status = "";
        } //---метод Clear

        /// <summary>
        /// Метод для очистки таймера; используется для того, чтобы обновлять время отображения сообщений.
        /// </summary>
        private void ClearTimer()
        {
            if (this.SingleTimer != null)
            {
                this.SingleTimer.Dispose();
                this.SingleTimer = null;
            }
        } //---метод ClearTimer
    } //---класс StatusString
} //---пространство имён CoreWPF.Utilites
//---EOF