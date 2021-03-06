﻿using DomainContext.Generics;
using DomainContext.Interfaces;
using GeneralDI;
using Microsoft.Practices.Unity;
using System;
using System.ComponentModel;
using TuongSo.Models;
using TuongSo.ViewModels;

namespace TuongSo.Navigators
{
    public class Navigator : INavigator, INotifyPropertyChanged
    {
        static BaseViewModel _ViewModel;

        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly IUnityContainer _services = TuongSoServiceContainer.GetCollection();
        public BaseViewModel CurrentViewModel
        {
            get => _ViewModel;
            set
            {
                _ViewModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentViewModel)));
            }
        }
        public IUnityContainer ServicesDI => _services;
        public IAppState AppState => this.ServicesDI.Resolve<IAppState>();

        public System.Windows.Input.ICommand UpdateViewModel { get; }
        public Type CurrentViewModelType { get; internal set; }

        public void NavigateToVM<T>() where T : BaseViewModel
        {
            NavigateToVM(typeof(T));
        }

        public void NavigateToVM(Type type)
        {
            var baseVcType = typeof(BaseViewModel);
            if (baseVcType.IsAssignableFrom(type))
            {
                var vc = this.ServicesDI.Resolve(type);
                this.CurrentViewModel = vc as BaseViewModel;
                this.CurrentViewModelType = type;
            }
        }

        private Navigator()
        {
            this.ServicesDI.RegisterInstance<INavigator>(this);
            UpdateViewModel = new UpdateViewControllerCommand(this);
            
            UpdateViewModel.Execute(typeof(PyCalVM));
            //UpdateViewModel.Execute(typeof(CustomerListVM));
        }

        public static Navigator Current { get; } = new Navigator();
    }
    public class UpdateViewControllerCommand : System.Windows.Input.ICommand
    {
        private readonly Navigator nav;

        public event EventHandler CanExecuteChanged;
        public UpdateViewControllerCommand(Navigator nav)
        {
            this.nav = nav;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Type t)
            {
                this.nav.NavigateToVM(t);
            }
        }
    }
}
