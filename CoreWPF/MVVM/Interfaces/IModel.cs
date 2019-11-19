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
} //---пространство имён CoreWPF.MVVM.Interfaces
//---EOF