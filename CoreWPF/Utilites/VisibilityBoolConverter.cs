using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CoreWPF.Utilites
{
    /// <summary>
    /// Класс для двусторонней конвертации <see cref="bool"/> - <see cref="Visibility"/>. Реализует интерфейс <see cref="IValueConverter"/>.
    /// </summary>
    public class VisibilityBoolConverter : IValueConverter
    {
        /// <summary>
        /// Статический метод, конвертирует <see cref="bool"/> в <see cref="Visibility"/>.
        /// </summary>
        /// <param name="boolean">Значение для конвертации.</param>
        /// <returns>Возвращает соответствующий значению <see cref="Visibility"/>.</returns>
        public static Visibility ToVisibility(bool boolean)
        {
            if (boolean == true) return Visibility.Visible;
            else return Visibility.Collapsed;
        } //---метод ToVisibility

        /// <summary>
        /// Статический метод, конвертирует <see cref="Visibility"/> в <see cref="bool"/>.
        /// </summary>
        /// <param name="visibility">Значение для конвертации.</param>
        /// <returns>Возвращает соответствующий значению <see cref="bool"/>.</returns>
        public static bool ToBoolean(Visibility visibility)
        {
            if (visibility == Visibility.Visible) return true;
            else return false;
        } //---метод ToBoolean

        /// <summary>
        /// Конвертирует <see cref="bool"/> в <see cref="Visibility"/>.
        /// </summary>
        /// <param name="value">Значение для конвертации.</param>
        /// <param name="targetType">Не используется в данном методе; можно передать null.</param>
        /// <param name="parameter">Не используется в данном методе; можно передать null.</param>
        /// <param name="culture">Не используется в данном методе; можно передать null.</param>
        /// <returns>Возвращает соответствующий значению <see cref="Visibility"/>; если значение не является <see cref="bool"/>, вернет <see cref="DependencyProperty.UnsetValue"/>.</returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v) return ToVisibility(v);
            else return DependencyProperty.UnsetValue;
        } //---метод Convert

        /// <summary>
        /// Конвертирует <see cref="Visibility"/> в <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Значение для конвертации.</param>
        /// <param name="targetType">Не используется в данном методе; можно передать null.</param>
        /// <param name="parameter">Не используется в данном методе; можно передать null.</param>
        /// <param name="culture">Не используется в данном методе; можно передать null.</param>
        /// <returns>Возвращает соответствующий значению <see cref="bool"/>; если значение не является <see cref="Visibility"/>, вернет <see cref="DependencyProperty.UnsetValue"/>.</returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v) return ToBoolean(v);
            else return DependencyProperty.UnsetValue;
        } //--метод ConvertBack
    } //---класс VisibilityBoolConverter
} //---пространство имён CoreWPF.Utilites
//---EOF