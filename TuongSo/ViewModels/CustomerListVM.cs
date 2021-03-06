﻿using DomainContext;
using DomainContext.Entities;
using DomainContext.Generics;
using DomainContext.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using TuongSo.Models;

namespace TuongSo.ViewModels
{
    public class CustomerListVM : BaseViewModel
    {
        private readonly LocalDomainContext domainContext;
        private readonly INavigator nav;

        public ObservableCollection<Customer> Customers { get; private set; } = new ObservableCollection<Customer>();
        public IAppState AppState { get; }

        public CustomerListVM()
        {

        }
        public CustomerListVM(LocalDomainContext domainContext, IAppState appState, INavigator nav)
        {
            this.domainContext = domainContext;
            AppState = appState;
            this.nav = nav;
        }

        public async Task GetCustomerList()
        {
            this.Customers.Clear();
            var customerList = await domainContext.Customers.OrderBy(e => e.CustomerName).ToListAsync();
            this.Customers.AddRange(customerList);
        }
        public void SetSelectedCustomer(Guid id)
        {
            this.AppState.SelectedCustomerId = id;
            this.nav.NavigateToVM<PyCalVM>();
        }

        public async Task DeleteCustomer(Guid id)
        {
            var c = await domainContext.Customers.FirstOrDefaultAsync(e => e.Id == id);
            if (c != null)
            {
                domainContext.Customers.Remove(c);
                await domainContext.SaveChangesAsync();
                await this.GetCustomerList();
            }
        }
    }
}
