﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TuongSo.ViewControlers;

namespace TuongSo.Views
{
    /// <summary>
    /// Interaction logic for PyramidView.xaml
    /// </summary>
    public partial class PyramidView : UserControl
    {
        public static readonly DependencyProperty UserInputDayProperty = DependencyProperty.Register("Day", typeof(string), typeof(PyramidView));
        public static readonly DependencyProperty UserInputMonthProperty = DependencyProperty.Register("Month", typeof(string), typeof(PyramidView));
        public static readonly DependencyProperty UserInputYearProperty = DependencyProperty.Register("Year", typeof(string), typeof(PyramidView));
        public string Day
        {
            get => GetValue(UserInputDayProperty) as string;
            set => SetValue(UserInputDayProperty, value);
        }
        public string Month
        {
            get => GetValue(UserInputMonthProperty) as string;
            set => SetValue(UserInputMonthProperty, value);
        }
        public string Year
        {
            get => GetValue(UserInputYearProperty) as string;
            set => SetValue(UserInputYearProperty, value);
        }

        public PyramidVC Context => this.DataContext as PyramidVC;

        public PyramidView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetBaseValue();
        }

        public void SetBaseValue()
        {
            this.Context.SetValue(this.Day, this.Month, this.Year);
        }
    }
}