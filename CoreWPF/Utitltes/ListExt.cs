using CoreWPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CoreWPF.Utilites
{
    /// <summary>
    /// Наследуется от <see cref="ObservableCollection{T}"/>, расширяя функционал
    /// </summary>
    /// <typeparam name="T">Принимает любой <see cref="object"/></typeparam>
    public partial class ListExt<T> : ObservableCollection<T>
    {
        #region Свойства
        /// <summary>
        /// Возвращает первый элемент последовательности
        /// </summary>
        public T First
        {
            get
            {
                return this.First();
            }
        } //---свойтсво First

        /// <summary>
        /// Возвращает последний элемент последовательности
        /// </summary>
        public T Last
        {
            get
            {
                return this.Last();
            }
        } //---свойство Last
        #endregion

        #region Констукторы
        /// <summary>
        /// Конструктор
        /// </summary>
        public ListExt() : base() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="collection">Принимает коллекцию, из которой создает массив элементов</param>
        public ListExt(IEnumerable<T> collection) : base(collection) { }
        #endregion

        #region Методы
        /// <summary>
        /// Добавляет коллекцию в конец массива
        /// </summary>
        /// <param name="collection">Принимает коллекцию для добавления</param>
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T model in collection)
            {
                this.Add(model);
            }
        } //---метод AddRange

        /// <summary>
        /// Добавляет коллекцию в указанный индекс массива
        /// </summary>
        /// <param name="index">Принимает индекс</param>
        /// <param name="collection">Принимает коллекцию для добавления</param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (index < 0 && index >= this.Count()) return;
            foreach (T model in Inverse(collection))
            {
                this.Insert(index, model);
            }
        } //---метод InsertRange

        /// <summary>
        /// Возвращает первый элемент последовательности, удовлетворяющий указанному условию
        /// </summary>
        /// <param name="predicate">Функция для проверки каждого элемента на соответствие условию</param>
        public T FindFirst(Func<T,bool> predicate)
        {
            return this.First(predicate);
        } //---метод FindFirst

        /// <summary>
        /// Возвращает последний элемент последовательности, удовлетворяющий указанному условию
        /// </summary>
        /// <param name="predicate">Функция для проверки каждого элемента на соответствие условию</param>
        public T FindLast(Func<T, bool> predicate)
        {
            return this.Last(predicate);
        } //---метод FindLast

        public static ListExt<T> FindRange(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            ListExt<T> tmp_send = new ListExt<T>();

            foreach(T model in collection)
            {
                if (predicate(model)) tmp_send.Add(model);
            }

            return tmp_send;
        }

        public ListExt<T> FindRange(Func<T, bool> predicate)
        {
            return FindRange(this, predicate);
        }

        public ListExt<T> Shuffle()
        {
            int[] tmp_index = new int[this.Count];
            ListExt<T> tmp_shuffle = new ListExt<T>();
            for (int i = 0; i < this.Count; i++)
            {
                tmp_index[i] = i;
            }

            for(int index = 0, step = tmp_index.Length; index < tmp_index.Length; index++, step--)
            {
                int tmp_rand = new Random().Next(step);
                tmp_shuffle.Insert(0, this[tmp_index[tmp_rand]]);
                tmp_index[tmp_rand] = tmp_index[step - 1];
            }
            
            return tmp_shuffle;
        }

        /// <summary>
        /// Удаляет из данного массива элементы коллекции; сравнивает объекты массива и коллекции как критерий
        /// </summary>
        /// <param name="collection">Принимает коллекцию элементов для удаления</param>
        public void RemoveRange(IEnumerable<T> collection)
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

        public ListExt<int> GetId()
        {
            ListExt<int> tmp_send = new ListExt<int>();

            foreach(T model in this)
            {
                if(model is IIdentify i)
                {
                    tmp_send.Add(i.Id);
                }
            }

            return tmp_send;
        }

        /// <summary>
        /// Инвертирует полученную коллекцию
        /// </summary>
        /// <param name="collection">Принимает коллекцию</param>
        /// <returns>Возвращает инвертированную коллекцию</returns>
        public static ListExt<T> Inverse(IEnumerable<T> collection)
        {
            ListExt<T> tmp_send = new ListExt<T>();

            for (int i = collection.Count() - 1; i >= 0; i--)
            {
                tmp_send.Add(collection.ElementAt(i));
            }

            return tmp_send;
        } //---метод Inverse

        /// <summary>
        /// Инвертирует текущую коллекцию
        /// </summary>
        /// <returns>Возвращает инвертированную текущую коллекцию</returns>
        public ListExt<T> Inverse()
        {
            return Inverse(this);
        }//---метод Inverse
        #endregion
    } //---класс ListExt<T>
}
