using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CoreWPF.Utilites
{
    /// <summary>
    /// Содержит инструменты для работы с Unix Timestamp.
    /// </summary>
    public static class UnixTime
    {
        /// <summary>
        /// Разница во времени согласно текущему часовому поясу, с учётом летнего времени.
        /// </summary>
        public static TimeSpan Local
        {
            get
            {
                return TimeZoneInfo.Local.BaseUtcOffset;
            }
        } //---свойство Local

        /// <summary>
        /// Разница во времени по UTC.
        /// </summary>
        public static TimeSpan UTC
        {
            get
            {
                return new TimeSpan(0, 0, 0);
            }
        } //---свойство UTC

        /// <summary>
        /// Вычисляет Unix Timestamp текущего времени в милисекундах; для вычисления используется смещение времени по <see cref="UTC"/>.
        /// </summary>
        /// <returns>Возвращает Unix Timestamp (в милисекундах) текущего времени.</returns>
        public static double CurrentUnixTimestamp()
        {
            return Math.Round((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds);
        } //---метод CurrentUnixTimestamp

        /// <summary>
        /// Вычисляет <see cref="DateTimeOffset"/> текущего времени.
        /// </summary>
        /// <param name="timezone">Необходимое смещение во времени по часовому поясу.</param>
        /// <returns>Возвращает <see cref="DateTimeOffset"/> текущего времени.</returns>
        public static DateTimeOffset CurrentDateTimeOffset(TimeSpan timezone)
        {
            return UnixTime.ToDateTimeOffset(UnixTime.CurrentUnixTimestamp(), timezone);
        } //---метод CurrentDateTimeOffset

        /// <summary>
        /// Конвертирует Unix Timestamp в <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="unixtimestamp">Unix Timestamp в милисекундах со смещением времени по <see cref="UTC"/>.</param>
        /// <param name="timezone">Необходимое смещение во времени по часовому поясу.</param>
        /// <returns>Возвращает <see cref="DateTimeOffset"/> с указанным смещением во времени по часовому поясу.</returns>
        public static DateTimeOffset ToDateTimeOffset(double unixtimestamp, TimeSpan timezone)
        {
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, timezone).AddMilliseconds(unixtimestamp + timezone.TotalMilliseconds);
        } //---метод ToDateTimeOffset

        /// <summary>
        /// Конвертирует <see cref="DateTimeOffset"/> в Unix Timestamp.
        /// </summary>
        /// <param name="datetime"><see cref="DateTimeOffset"/> для работы.</param>
        /// <returns>Возвращает Unix Timestamp в милисекундах со смещением времени по <see cref="UTC"/>.</returns>
        public static double ToUnixTimestamp(DateTimeOffset datetime)
        {
            TimeSpan epochTicks = new TimeSpan(new DateTime(1970, 1, 1).Ticks);
            TimeSpan unixTicks = new TimeSpan(datetime.UtcTicks) - epochTicks;
            return unixTicks.TotalMilliseconds;
        } //---метод ToUnixtimestamp
    } //---класс UnixTime

    /// <summary>
    /// Класс для двустороннего конвертирования Unix Timestamp - <see cref="string"/>; реализует <see cref="IValueConverter"/>.
    /// </summary>
    public class UnixTimeConverter : IValueConverter
    {
        /// <summary>
        /// Конвертация Unix Timestamp в <see cref="string"/>; используется смещение во времени по <see cref="UnixTime.Local"/>.
        /// </summary>
        /// <param name="value">Unix Timestamp в формате <see cref="double"/> со смещением во времени по <see cref="UnixTime.UTC"/>.</param>
        /// <param name="targetType">Не используется в текущем методе; можно передать null.</param>
        /// <param name="parameter">Не используется в текущем методе; можно передать null.</param>
        /// <param name="culture">Не используется в текущем методе; можно передать null.</param>
        /// <returns>Возвращает соответствующую строку со смещение во времени по <see cref="UnixTime.Local"/>; если введен не <see cref="double"/>, возвращает <see cref="DependencyProperty.UnsetValue"/>.</returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double unix_stamp)
            {
                return UnixTime.ToDateTimeOffset(unix_stamp, UnixTime.Local).ToString();
            }
            else return DependencyProperty.UnsetValue;
        } //---метод Convert

        /// <summary>
        /// Конвертация <see cref="string"/> в <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="value">Строка в формате: <code>год.месяц.день часы:минуты:секунды часовой:пояс</code></param>
        /// <param name="targetType">Не используется в текущем методе; можно передать null.</param>
        /// <param name="parameter">Не используется в текущем методе; можно передать null.</param>
        /// <param name="culture">Не используется в текущем методе; можно передать null.</param>
        /// <returns>Возвращает <see cref="DateTimeOffset"/>; если строка имеет неверный формат, возвращает <see cref="DependencyProperty.UnsetValue"/>.</returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string string_time)
            {
                try
                {
                    string date = string_time.Split(' ')[0];
                    string time = string_time.Split(' ')[1];
                    string tz = string_time.Split(' ')[2];

                    DateTimeOffset tmp_send = new DateTimeOffset(
                    System.Convert.ToInt32(date.Split('.')[0]),
                    System.Convert.ToInt32(date.Split('.')[1]),
                    System.Convert.ToInt32(date.Split('.')[2]),
                    System.Convert.ToInt32(time.Split(':')[0]),
                    System.Convert.ToInt32(time.Split(':')[1]),
                    System.Convert.ToInt32(time.Split(':')[2]),
                    new TimeSpan(System.Convert.ToInt32(tz.Split(':')[0]),
                    System.Convert.ToInt32(tz[0] + tz.Split(':')[1]),
                    0
                    ));
                    return tmp_send;
                }
                catch
                {
                    return DependencyProperty.UnsetValue;
                }
            }
            else return DependencyProperty.UnsetValue;
        } //---метод ConvertBack
    } //---класс UnixTimeConverter
} //---пространство имён CoreWPF.Utilites
//---EOF