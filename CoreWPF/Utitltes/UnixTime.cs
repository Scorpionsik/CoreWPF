using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace CoreWPF.Utilites
{
    /// <summary>
    /// Реализует методы для работы с Unix Timestamp (в секундах)
    /// </summary>
    public static partial class UnixTime
    {
        #region Возвращают DateTime
        /// <summary>
        /// Преобразовывает Unix Timestamp в объект класса DateTime
        /// </summary>
        /// <param name="seconds">Принимает Unix Timestamp (в секундах)</param>
        /// <returns>Возвращает объект класса DateTime</returns>
        public static DateTime ToDateTime(double seconds)
        {
            return new DateTime(1970, 1, 1).AddSeconds(seconds);
        } //---метод ToDateTime

        /// <summary>
        /// Переводит строку определенного формата в <see cref="DateTime"/>
        /// </summary>
        /// <param name="string_time">Принимает строку в формате "%год%-%месяц%-%день%Т%часы%:%минуты%:%секунды%"</param>
        /// <returns>Возвращает <see cref="DateTime"/> принятой строки</returns>
        /// <exception cref="ArgumentException"/>
        public static DateTime ToDateTime(string string_time)
        {
            try
            {
                string date = string_time.Remove(string_time.IndexOf("T"));
                string time = string_time;
                if (string_time.Contains("+")) time = time.Remove(string_time.IndexOf("+"));
                time = time.Remove(0, string_time.IndexOf("T") + 1);
            
                DateTime tmp_send = new DateTime(
                Convert.ToInt32(date.Split('-')[0]), 
                Convert.ToInt32(date.Split('-')[1]), 
                Convert.ToInt32(date.Split('-')[2]),
                Convert.ToInt32(time.Split(':')[0]),
                Convert.ToInt32(time.Split(':')[1]),
                Convert.ToInt32(time.Split(':')[2])
                );
                return UnixTime.ToDateTime(UnixTime.ToUnixTimestamp(tmp_send));
            }
            catch
            {
                throw new ArgumentException("Введённая строка имеет недопустимый формат.");
            }
        } //---метод ToDateTime
        #endregion

        #region Возвращают строку
        /// <summary>
        /// Преобразовывает Unix Timestamp в строчное представление времени
        /// </summary>
        /// <param name="seconds">Принимает Unix Timestamp</param>
        /// <returns>Возвращает строчное представление времени</returns>
        public static string ToString(double seconds)
        {
            return UnixTime.ToDateTime(seconds).ToLocalTime().ToString();
        } //---метод ToString

        /// <summary>
        /// Преобразовывает <see cref="DateTime"/> в строчное представление времени
        /// </summary>
        /// <param name="date_time">Принимает <see cref="DateTime"/></param>
        /// <returns>Возвращает строчное представление времени</returns>
        public static string ToString(DateTime date_time)
        {
            return UnixTime.ToString(UnixTime.ToUnixTimestamp(date_time));
        } //---метод ToString
        #endregion

        #region Возвращают Unix Timestamp
        /// <summary>
        /// Преобразовывает <see cref="DateTime"/> в Unix Timestamp
        /// </summary>
        /// <param name="date_time">>Принимает <see cref="DateTime"/></param>
        /// <returns>Возвращает Unix Timestamp</returns>
        public static double ToUnixTimestamp(DateTime date_time)
        {
            return (DateTime.UtcNow.Subtract(date_time)).TotalSeconds;
        } //---метод ToUnixTimestamp
        #endregion

        #region Методы для вычисления текущего времени
        /// <summary>
        /// Вычисляет текущее время
        /// </summary>
        /// <returns>Возвращает Unix Timestamp текущего времени</returns>
        public static double CurrentUnixTimestamp()
        {
            return UnixTime.ToUnixTimestamp(new DateTime(1970, 1, 1));
        } //---метод CurrentUnixTimestamp

        /// <summary>
        /// Вычисляет текущее время
        /// </summary>
        /// <returns>Возвращает строкое представление текущего времени</returns>
        public static string CurrentString()
        {
            return UnixTime.ToString(UnixTime.CurrentUnixTimestamp());
        } //---метод CurrentString

        /// <summary>
        /// Вычисляет текущее время
        /// </summary>
        /// <returns>Возвращает <see cref="DateTime"/> текущего времени</returns>
        public static DateTime CurrentDateTime()
        {
            return UnixTime.ToDateTime(UnixTime.CurrentUnixTimestamp());
        }//---метод CurrentDateTime
        #endregion
    } //---класс UnixTime

    public class UnixTimeConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double unix_stamp)
            {
                return UnixTime.ToString(unix_stamp);
            }
            else return DependencyProperty.UnsetValue;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string string_time)
            {
                return UnixTime.ToUnixTimestamp(UnixTime.ToDateTime(string_time));
            }
            else return DependencyProperty.UnsetValue;
        }
    }
} //---пространство имён CoreWPF.Utilites
//---EOF