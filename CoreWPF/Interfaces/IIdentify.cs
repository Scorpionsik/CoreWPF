namespace CoreWPF.Interfaces
{
    /// <summary>
    /// Интерфейс для представления данных в формате Id - Имя
    /// </summary>
    public interface IIdentify
    {
        /// <summary>
        /// Id объекта
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Имя объекта
        /// </summary>
        string Name { get; }
    } //---интерфейс IIdentify
} //---пространство имён CoreWPF.Interfaces
//---EOF