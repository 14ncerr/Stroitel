using Microsoft.EntityFrameworkCore;
using Stroitel.Context;
using Stroitel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroitel.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private List<Product> _displayingProducts;
        private string _searchingString;
        private string _filtValue;
        private string _sortValue;

        #endregion

        #region Properties

        public List<Product> DisplayingProducts
        {
            get { return _displayingProducts; }
            set 
            { 
                Set(ref _displayingProducts, value, nameof(DisplayingProducts)); 
            }
        }

        public string SearchingString
        {
            get { return _searchingString; }
            set
            {
                Set(ref _searchingString, value, nameof(SearchingString));
                DisplayProducts();
            }
        }
        public string FiltValue
        {
            get { return _filtValue; }
            set
            {
                Set(ref _filtValue, value, nameof(FiltValue));
                DisplayProducts();
            }
        }

        public string SortValue
        {
            get { return _sortValue; }
            set
            {
                Set(ref _sortValue, value, nameof(SortValue));
                DisplayProducts();
            }
        }


        public List<string> FiltItems => new List<string>
        {
            "Все  диапазоны", "Скидка 0 - 9,99%", "Скидка 10 - 14,99%", "Скидка 15% и более"
        };

        public List<string> SortItems => new List<string>
        {
            "Без сортироки", "По возрастанию цены", "По убыванию цены"
        };


        public List<Product> Products { get; set; }

        public User CurrentUser { get; set; }

        #endregion
        
        #region Constructors

        public MainWindowViewModel()
        {
            SortValue = SortItems[0];
            FiltValue = FiltItems[0];
            using (AppDbContext context = new())
            {
                Products = context.Products
                    .Include(p => p.ProductManufacturerNavigation)
                    .ToList();
            }
            _displayingProducts = Products;
        }

        public MainWindowViewModel(int userId)
            :this()
        {          
            using (AppDbContext context = new())
            {
                CurrentUser = context.Users
                    .Include(u => u.UserRoleNavigation)
                    .Single(u => u.UserId == userId);          
            }
            
        }

        #endregion

        #region Sorting, searching and filtering product list

        private void DisplayProducts()
        {
            DisplayingProducts = Sort(Search(Filt(Products)));
        }

        private List<Product> Sort(List<Product> list)
        {
            if (SortValue == SortItems[1])
                return list.OrderBy(p => p.ProductCost).ToList();
            else if (SortValue == SortItems[2])
                return list.OrderByDescending(p => p.ProductCost).ToList();
            else
                return list;        
        }

        private List<Product> Search(List<Product> list)
        {
            if ((SearchingString == String.Empty) || (SearchingString == null))
                return list;
            else
                return list.Where(p => p.ProductName.ToLower().Contains(SearchingString.ToLower())).ToList();           
        }

        private List<Product> Filt(List<Product> list)
        {
            if (FiltValue == FiltItems[1])
                return list.Where(p => p.ProductDiscountAmount < 10).ToList();
            else if (FiltValue == FiltItems[2])
                return list.Where(p => p.ProductDiscountAmount < 15 && p.ProductDiscountAmount > 9.99).ToList();
            else if (FiltValue == FiltItems[3])
                return list.Where(p => p.ProductDiscountAmount > 15).ToList();
            else
                return list;
        }

        #endregion
    }
}
