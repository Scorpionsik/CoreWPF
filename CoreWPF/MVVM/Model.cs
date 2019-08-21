using CoreWPF.MVVM.Interfaces;

namespace CoreWPF.MVVM
{
    /// <summary>
    /// Реализует интерфейс <see cref="IModel"/>, базовый вариант
    /// </summary>
    public abstract partial class Model : SimpleModel, IModel
    {
        #region Методы
        /// <summary>
        /// Создает копию данной модели
        /// </summary>
        /// <returns>Возвращает копию модели</returns>
        public abstract IModel Clone();

        /// <summary>
        /// Сравнивает две модели
        /// </summary>
        /// <param name="model">Принимает IModel для сравнения</param>
        /// <returns>Возвращает true, если модели равны</returns>
        public abstract bool Equals(IModel model);

        /// <summary>
        /// Обновляет текущую модель переданной
        /// </summary>
        /// <param name="model">Принимает модель, которой будет обновлена текущая модель</param>
        public abstract void Merge(IModel model);
        #endregion
    } //---класс Model
} //---пространство имён CoreWPF.MVVM
//---EOF