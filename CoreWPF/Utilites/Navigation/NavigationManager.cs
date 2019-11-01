using CoreWPF.Utilites.Navigation.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CoreWPF.Utilites.Navigation
{
    /// <summary>
    /// Предоставляет инструменты для переключения нескольких ViewModel в одном окне.
    /// <para>Начало работы: Сначала требуется создать экземпляр текущего менеджера и передать в конструкторе <see cref="Dispatcher"/> и <see cref="ContentControl"/> окна, в котором будут меняться ViewModel. 
    /// Далее регистрируются ViewModel (в который передается ссылка на текущий менеджер), View и ключ, по которому можно будет найти ViewModel в коллекции менеджера. Рекомендуется отдельно хранить список ключей, по которым можно обратиться к тому или иному ViewModel.
    /// После чего менеджер передается в конструкторе ViewModel основного окна.</para>
    /// </summary>
    [Serializable]
    public class NavigationManager
    {
        #region Поля и свойства
        private event Action<INavigateModule> navigation_invoke;
        public Action<INavigateModule> Navigation_invoke
        {
            get { return this.navigation_invoke; }
            set
            {
                this.navigation_invoke = new Action<INavigateModule>(value);
            }
        }

        private readonly Dispatcher _dispatcher;
        private readonly ContentControl _frameControl;
        private readonly IDictionary<string, object> _viewModelsByNavigationKey = new Dictionary<string, object>();
        private readonly IDictionary<Type, Type> _viewTypesByViewModelType = new Dictionary<Type, Type>();

        #endregion

        #region Конструкторы
        public NavigationManager(Dispatcher dispatcher, ContentControl frameControl)
        {
            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");
            if (frameControl == null)
                throw new ArgumentNullException("frameControl");

            _dispatcher = dispatcher;
            _frameControl = frameControl;
        }
        #endregion

        #region Методы
        public void Register<TViewModel, TView>(TViewModel viewModel, string navigationKey)
            where TViewModel : class
            where TView : FrameworkElement
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");
            if (navigationKey == null)
                throw new ArgumentNullException("navigationKey");

            _viewModelsByNavigationKey[navigationKey] = viewModel;
            _viewTypesByViewModelType[typeof(TViewModel)] = typeof(TView);
        }

        public void Navigate(string navigationKey, object arg = null)
        {
            if (navigationKey == null)
                throw new ArgumentNullException("navigationKey");

            InvokeInMainThread(() =>
            {
                InvokeNavigatingFrom();
                var viewModel = GetNewViewModel(navigationKey);
                InvokeNavigatingTo(viewModel, arg);

                var view = CreateNewView(viewModel);
                _frameControl.Content = view;

                if (viewModel is INavigateModule module)
                {
                    this.Navigation_invoke?.Invoke(module);
                }
            });
        }

        private void InvokeInMainThread(Action action)
        {
            _dispatcher.Invoke(action);
        }

        private FrameworkElement CreateNewView(object viewModel)
        {
            var viewType = _viewTypesByViewModelType[viewModel.GetType()];
            var view = (FrameworkElement)Activator.CreateInstance(viewType);
            view.DataContext = viewModel;
            return view;
        }

        private object GetNewViewModel(string navigationKey)
        {
            return _viewModelsByNavigationKey[navigationKey];
        }

        private void InvokeNavigatingFrom()
        {
            var oldView = _frameControl.Content as FrameworkElement;
            if (oldView == null)
                return;

            var navigationAware = oldView.DataContext as INavigateModule;
            if (navigationAware == null)
                return;

            navigationAware.OnNavigatingFrom();
        }

        private static void InvokeNavigatingTo(object viewModel, object arg)
        {
            var navigationAware = viewModel as INavigateModule;
            if (navigationAware == null)
                return;

            navigationAware.OnNavigatingTo(arg);
        }

        public string GetSubtitle(string navigationKey)
        {
            return ((INavigateModule)_viewModelsByNavigationKey[navigationKey]).Subtitle;
        }
        #endregion
    }
}
