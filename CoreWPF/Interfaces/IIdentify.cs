namespace CoreWPF.Interfaces
{
    /// <summary>
    /// Интерфейс для представления данных со свойствами <see cref="IId.Id"/> и <see cref="Name"/>.
    /// </summary>
    public interface IIdentify : IId
    {
        /// <summary>
        /// Имя объекта
        /// </summary>
        string Name { get; }
    } //---интерфейс IIdentify
} //---пространство имён CoreWPF.Interfaces
//---EOF