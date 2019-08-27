using CoreWPF.Utilites;
using System;
using System.Linq;

namespace CoreWPF.MVVM
{
    public abstract partial class SimpleModel : NotifyPropertyChanged
    {
        #region Поля и свойства
        private event Action<SimpleModel> event_select_model;
        /// <summary>
        /// Событие выбора данной модели
        /// </summary>
        public event Action<SimpleModel> Event_select_model
        {
            add { this.event_select_model += value; }
            remove { this.event_select_model -= value; }
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
    }
}
