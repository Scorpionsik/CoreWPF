namespace CoreWPF.MVVM.Interfaces
{
    public interface IModel
    {
        IModel Clone();
        bool Equals(IModel model);
        void Merge(IModel model);
    }

    /// <summary>
    /// Интерфейс для модели
    /// </summary>
    public interface IModel<T> where T : NotifyPropertyChanged
    {
        /// <summary>
        /// Метод создает копию текущей модели
        /// </summary>
        /// <returns>Возвращает <see cref="IModel"/> - копию</returns>
        T Clone();

        /// <summary>
        /// Метод cравнивает модели между собой
        /// </summary>
        /// <param name="model">Принимает <see cref="IModel"/> для сравнения</param>
        /// <returns>Возвращает true, если модели равны</returns>
        bool Equals(T model);

        /// <summary>
        /// Метод переписывает данные из другой модели в текущую
        /// </summary>
        /// <param name="model">Принимает <see cref="IModel"/>, с которой будет записывать данные</param>
        void Merge(T model);
    } //---интерфейс IModel
} //---пространство имён CoreWPF.MVVM.Interfaces
//---EOF