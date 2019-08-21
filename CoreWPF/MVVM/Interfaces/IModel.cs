namespace CoreWPF.MVVM.Interfaces
{
    /// <summary>
    /// Интерфейс для модели
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Метод создает копию текущей модели
        /// </summary>
        /// <returns>Возвращает <see cref="IModel"/> - копию</returns>
        IModel Clone();

        /// <summary>
        /// Метод cравнивает модели между собой
        /// </summary>
        /// <param name="model">Принимает <see cref="IModel"/> для сравнения</param>
        /// <returns>Возвращает true, если модели равны</returns>
        bool Equals(IModel model);

        /// <summary>
        /// Метод переписывает данные из другой модели в текущую
        /// </summary>
        /// <param name="model">Принимает <see cref="IModel"/>, с которой будет записывать данные</param>
        void Merge(IModel model);
    } //---интерфейс IModel
} //---пространство имён CoreWPF.MVVM.Interfaces
//---EOF