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
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private CartWindowViewModel _viewModel;

        public CartWindow(List<Product> products)
        {
            InitializeComponent();
            _viewModel = new(products);
            DataContext = _viewModel;
            DeliveryDateTb.Text = $"Доставка {_viewModel.DeliveryDate}";       
        }

        private void GenerateOrder(object sender, RoutedEventArgs e)
        {

        }

        private void IncreaseCount(object sender, RoutedEventArgs e)
        {
            
        }

        private void DecreaseCount(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {

        }
    }
}
