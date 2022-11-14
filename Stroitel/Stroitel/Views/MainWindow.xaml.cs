using Microsoft.EntityFrameworkCore;
using Stroitel.Context;
using Stroitel.Models;
using Stroitel.ViewModels;
using System;
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
using System.Windows.Shapes;

namespace Stroitel.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        private AuthStartupWindow _startupWindow;
        private CartWindow _cartWindow;
        private List<Product> _productsToCart;


        public MainWindow()
        {
            InitializeComponent();
            _productsToCart = new();
        }

        public MainWindow(int userId)
        {
            InitializeComponent();
            _viewModel = new(userId);
            DataContext = _viewModel;
            _productsToCart = new();
        }

        private void CartBtnClick(object sender, RoutedEventArgs e)
        {
            _cartWindow = new(_productsToCart);
            _cartWindow.ShowDialog();
            CartBtn.Visibility = Visibility.Hidden;
        }

        private void AddItemToCart(object sender, RoutedEventArgs e)
        {
            bool alreadyAdded = false;
            foreach (var item in _productsToCart)
            {
                if (EqualityComparer<Product>.Default.Equals(item, (Product)ProductsListLayoutLv.SelectedItem))
                {
                    MessageBox.Show("Товар уже добавлен");
                    alreadyAdded = true;
                }
            }

            if (alreadyAdded == false)           
                _productsToCart.Add((Product)ProductsListLayoutLv.SelectedItem);
            
            CartBtn.Visibility = Visibility.Visible;
        }

        private void QuitBtnClick(object sender, RoutedEventArgs e)
        {
            _startupWindow = new();
            _startupWindow.Show();
            Close();
        }
    }
}
