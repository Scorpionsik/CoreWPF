using CoreWPF.MVVM.Interfaces;
using CoreWPF.Utilites;
using System;
using System.Linq;

namespace CoreWPF.MVVM
{
    /// <summary>
    /// Реализует интерфейс <see cref="IModel"/>, базовый вариант
    /// </summary>
    public abstract partial class Model : NotifyPropertyChanged, IModel
    {
        #region Поля и свойства
        private event Action<Model> event_select_model;
        /// <summary>
        /// Событие выбора данной модели
        /// </summary>
        public Action<Model> Event_select_model
        {
            get { return this.event_select_model; }
            set
            {
                this.event_select_model = new Action<Model>(value);
            }
        } //---свойство Event_select_model

        /// <summary>
        /// Возвращает имя класса текущей модели
        /// </summary>
        public string ClassName
        {
            get { return this.GetType().ToString().Split('.').Last(); }
        } //---свойство ClassName
        #endregion

        #region Команды
        private RelayCommand command_select_model;
        /// <summary>
        /// Команда, вызывающее событие выбора данной модели
        /// </summary>
        public RelayCommand Command_select_model
        {
            get
            {
                return this.command_select_model ?? (this.command_select_model = new RelayCommand(obj =>
                {
                    this.Event_select_model?.Invoke(this);
                }));
            }
        } //---команда Command_select_model
        #endregion

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