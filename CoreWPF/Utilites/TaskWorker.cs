using CoreWPF.MVVM;
using CoreWPF.Utilites.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWPF.Utilites
{
    public abstract class TaskWorker<T> : NotifyPropertyChanged where T : class
    {
        private TaskWorkerStatus status;
        /// <summary>
        /// Состояние потоков программы
        /// </summary>
        public TaskWorkerStatus Status
        {
            get => this.status;
            private set
            {
                this.status = value;
                this.OnPropertyChanged("Status");
            }
        }

        private Exception error;
        /// <summary>
        /// Ошибка во время выполнения потоков
        /// </summary>
        public Exception Error
        {
            get => this.error;
            private set
            {
                this.error = value;
                this.OnPropertyChanged("Error");
            }
        }

        /// <summary>
        /// Коллекция объектов для работы потоков
        /// </summary>
        public ListExt<T> Test_list { get; private set; }
        private T select;
        /// <summary>
        /// Выбранный объект из коллекции, используется для удаления объекта из коллекции.
        /// </summary>
        public T Select
        {
            get => this.select;
            set
            {
                this.select = value;
                this.OnPropertyChanged("Select");
            }
        }

        /// <summary>
        /// Источник для токена отмены
        /// </summary>
        private CancellationTokenSource Cancel_source;

        public TaskWorker() : base()
        {
            this.Error = null;
            this.Status = TaskWorkerStatus.Ready;
            this.Test_list = new ListExt<T>();
        }

        /// <summary>
        /// Добавляет один элемент в нашу коллекцию и запускает выполнение потоков
        /// </summary>
        /// <param name="values">Элемент</param>
        protected void AddAndStart(T values)
        {
            Test_list.Add(values);
            this.Start();
        }

        /// <summary>
        /// Добавляет коллекцию элементов в нашу коллекцию и запускает выполнение потоков
        /// </summary>
        /// <param name="values"></param>
        protected void AddAndStart(IEnumerable<T> values)
        {
            Test_list.AddRange(values);
            this.Start();
        }

        /// <summary>
        /// Проверяет, можно ли запустить потоки
        /// </summary>
        /// <returns>Возвращает true, если запуск возможен</returns>
        protected bool CheckValidStart()
        {
            return (this.Test_list != null && this.Test_list.Count > 0 && this.Status != TaskWorkerStatus.InWork) ? true : false;
        }

        /// <summary>
        /// Проверяет возможность запуска, затем запускает выполнение потоков
        /// </summary>
        /// <returns>Возвращает true, если запуск произошел</returns>
        protected bool Start()
        {
            bool check = this.CheckValidStart();
            if (check)
            {
                this.Error = null;
                this.Cancel_source = new CancellationTokenSource();
                this.DoAsync(this.Cancel_source.Token);
            }
            return check;
        }

        /// <summary>
        /// Отменяет работу потоков
        /// </summary>
        protected void Cancel()
        {
            this.Cancel_source.Cancel();
            this.Status = this.Status != TaskWorkerStatus.Complete && this.Status != TaskWorkerStatus.Ready ? TaskWorkerStatus.Cancel : TaskWorkerStatus.Ready;
        }

        /// <summary>
        /// Отменяет работу потоков и пишет ошибку
        /// </summary>
        /// <param name="ex">Ошибка для записи</param>
        protected void SetError(Exception ex)
        {
            this.Cancel_source.Cancel();
            this.Error = ex;
            this.Status = TaskWorkerStatus.Error;
        }

        /// <summary>
        /// Асинхронный метод, выполняющий в цикле работу с элементами коллекции поочередно, начиная с первого
        /// </summary>
        /// <param name="cancel">Токен для отмены выполнения потоков</param>
        private async void DoAsync(CancellationToken cancel)
        {
            this.Status = TaskWorkerStatus.InWork;
            while (Test_list.Count > 0 && !cancel.IsCancellationRequested)
            {
                T value = this.Test_list.First;
                await Task.Run(() => this.Run(value, cancel), cancel);
                if (!cancel.IsCancellationRequested) this.Test_list.Remove(value);
            }
            if (this.Status != TaskWorkerStatus.Cancel && this.Status != TaskWorkerStatus.Error) this.Status = TaskWorkerStatus.Complete;
        }

        /// <summary>
        /// Метод, который используется для работы с одним элементом
        /// </summary>
        /// <param name="value">Элемент для работы</param>
        /// <param name="cancel">Токен для отмены работы с элементом и выполнения потоков</param>
        private void Run(T value, CancellationToken cancel)
        {
            try
            {
                this.RunMethod(value, cancel);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                this.SetError(ex);
            }
        }

        protected abstract void RunMethod(T value, CancellationToken cancel);
    }
}
