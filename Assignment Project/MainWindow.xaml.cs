﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFocusWindow.Navigate(new Uri("management.xaml", UriKind.Relative));
        }

        private void OverView_Click(object sender, RoutedEventArgs e)
        {
            MainFocusWindow.Navigate(new Uri("overView.xaml", UriKind.Relative));

        }

        private void Management_Click(object sender, RoutedEventArgs e)
        {
            MainFocusWindow.Navigate(new Uri("management.xaml", UriKind.Relative));

        }

        private void Contract_Click(object sender, RoutedEventArgs e)
        {
            MainFocusWindow.Navigate(new Uri("Report.xaml", UriKind.Relative));

        }
    }
}
