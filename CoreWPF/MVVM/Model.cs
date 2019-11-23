using CoreWPF.Utilites;
using System;
using System.Linq;

namespace CoreWPF.MVVM
{
    /// <summary>
    /// Предоставляет базовый функционал для модели приложения; наследуется от <see cref="NotifyPropertyChanged"/>.
    /// </summary>
    [Serializable]
    public abstract class Model : NotifyPropertyChanged
    {
        #region Поля и свойства
        /// <summary>
        /// Событие выбора данной модели
        /// </summary>
        [field: NonSerialized]
        private event Action<Model> event_select_model;
        /// <summary>
        /// Cобытие выбора данной модели
        /// </summary>
        public event Action<Model> Event_select_model
        {
            add
            {
                this.event_select_model -= value;
                this.event_select_model += value;
            }
            remove
            {
                this.event_select_model -= value;
            }
        } //---свойство Event_select_model

        /// <summary>
        /// Возвращает название класса текущей модели
        /// </summary>
        public string ClassName
        {
            get { return this.GetType().ToString().Split('.').Last(); }
        } //---свойство ClassName
        #endregion

        #region Команды
        /// <summary>
        /// Команда, вызывающее событие выбора данной модели
        /// </summary>
        public virtual RelayCommand Command_select_model
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    this.event_select_model?.Invoke(this);
                });
            }
        } //---команда Command_select_model
        #endregion
    } //---класс Model
} //---пространство имён CoreWPF.MVVM
//---EOF