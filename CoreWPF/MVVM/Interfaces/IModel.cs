using System.ComponentModel;

namespace CoreWPF.MVVM.Interfaces
{
    /// <summary>
    /// Реализация клонирования, сравнения и слияния у неявно определенного объекта.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Метод создает копию <see cref="IModel"/>
        /// </summary>
        /// <returns>Возвращает копию текущего <see cref="IModel"/></returns>
        IModel Clone();

        /// <summary>
        /// Метод cравнивает <see cref="IModel"/> между собой
        /// </summary>
        /// <param name="model">Принимает <see cref="IModel"/> для сравнения</param>
        /// <returns>Возвращает true, если <see cref="IModel"/> равны</returns>
        bool Equals(IModel model);

        /// <summary>
        /// Метод переписывает данные из другой <see cref="IModel"/> в текущую
        /// </summary>
        /// <param name="model">Принимает <see cref="IModel"/>, с которой будет записывать данные</param>
        void Merge(IModel model);
    } //---интерфейс IModel

    /// <summary>
    /// Реализация клонирования, сравнения и слияния у явно определенного объекта. 
    /// </summary>
    /// <typeparam name="T">Объект, наследующийся от <see cref="INotifyPropertyChanged"/></typeparam>
    /// <remarks>
    /// Ищу применение для того, чтобы отказаться от неявно определенного <see cref="IModel"/>. Если в итоге использоваться не будет, или в нём не будет толку - удалю.
    /// </remarks>
    public interface IModel<T> where T : INotifyPropertyChanged
    {
        /// <summary>
        /// Метод создает копию T
        /// </summary>
        /// <returns>Возвращает копию текущего объекта</returns>
        T Clone();

        /// <summary>
        /// Метод cравнивает Т между собой
        /// </summary>
        /// <param name="model">Принимает Т для сравнения</param>
        /// <returns>Возвращает true, если Т равны</returns>
        bool Equals(T model);

        /// <summary>
        /// Метод переписывает данные из другой Т в текущую
        /// </summary>
        /// <param name="model">Принимает Т, с которой будет записывать данные</param>
        void Merge(T model);
    } //---интерфейс IModel<T>
} //---пространство имён CoreWPF.MVVM.Interfaces
//---EOF