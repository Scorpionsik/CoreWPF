﻿using CoreWPF.MVVM.Interfaces;
using CoreWPF.Utilites;
using System;
using System.Collections.Generic;

namespace CoreWPF.MVVM.Utilites
{
    /// <summary>
    /// Наследуется от <see cref="ListExt{T}"/>, адаптируя и расширяя функционал для <see cref="IModel"/>
    /// </summary>
    /// <typeparam name="T">Должен наследоваться от <see cref="IModel"/></typeparam>
    [Serializable]
    public class ListModel<T> : ListExt<T> where T : IModel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ListModel{T}"/>.
        /// </summary>
        public ListModel() : base() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ListModel{T}"/>, который содержит элементы, скопированные из указанной коллекции.
        /// </summary>
        /// <param name="collection">Принимает коллекцию, которая будет скорпирована в текущий экземпляр <see cref="ListModel{T}"/>.</param>
        public ListModel(IEnumerable<T> collection) : base(collection) { }

        /// <summary>
        /// Удаляет из данного массива элементы коллекции; сравнивает объекты массива и коллекции, используя <see cref="IModel.Equals(IModel)"/>.
        /// </summary>
        /// <param name="collection">Принимает коллекцию элементов для сравнения и удаления</param>
        public new void RemoveRange(IEnumerable<T> collection)
        {
            foreach (T remove in collection)
            {
                foreach (T model in this)
                {
                    if (model.Equals(remove))
                    {
                        this.Remove(model);
                        break;
                    }
                }
            }
        } //---метод RemoveRange

        public new ListModel<T> FindRange(Func<T, bool> predicate)
        {
            return new ListModel<T>(base.FindRange(predicate));
        }

        /// <summary>
        /// Определяет, входил ли элемент в коллекцию; сравнивает через Equals
        /// </summary>
        /// <param name="model">Принимает <see cref="IModel"/> для сравнения</param>
        /// <returns>Возвращает true, если элемент найден, иначе false</returns>
        public new bool Contains(T model)
        {
            foreach (T check in this)
            {
                if (check.Equals(model)) return true;
            }
            return false;
        } //---метод Contains

        public void Merge(T model)
        {
            foreach (T merge in this)
            {
                if (merge.Equals(model))
                {
                    merge.Merge(model);
                    break;
                }
            }
        }

        public void Merge(IEnumerable<T> models)
        {
            foreach (T merge in models)
            {
                this.Merge(merge);
            }
        }

        /// <summary>
        /// Создает копию текущей коллекции
        /// </summary>
        /// <returns>Возвращает копию текущей коллекции</returns>
        public ListModel<T> Clone()
        {
            ListModel<T> tmp_send = new ListModel<T>();
            foreach (T model in this)
            {
                tmp_send.Add((T)model.Clone());
            }
            return tmp_send;
        }
    } //---класс ListModel<T>
}