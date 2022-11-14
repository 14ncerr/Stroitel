using Stroitel.Context;
using Stroitel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroitel.ViewModels
{
    public class CartWindowViewModel : ViewModelBase
    {
        public List<Product> ProductsInCart { get; set; }
        public List<ItemInCart> ItemsInCart { get; set; }
        public record ItemInCart(string photo, string name, decimal cost, int count);
        public List<string> PickupPoints { get; set; }
        public string SelectedPickupPoint { get; set; }
        public string CustomerFullName { get; set; }
        public string DeliveryDate { get; set; }
        public decimal? TotalCost { get; set; }

        public CartWindowViewModel()
        {

        }

        public CartWindowViewModel(List<Product> products)
        {
            ProductsInCart = products;
            ItemsInCart = new();
            PickupPoints = new();
            DeliveryDate = DateTime.Now.AddDays(3).ToShortDateString();
            foreach (var item in ProductsInCart)
            {
                ItemInCart itemInCart = new(item.ProductPhoto, item.ProductName, item.ProductCost, 1);
                ItemsInCart.Add(itemInCart);
                if (item.ProductQuantityInStock < 3)
                    DeliveryDate = DateTime.Now.AddDays(6).ToShortDateString();
                else
                    DeliveryDate = DateTime.Now.AddDays(3).ToShortDateString();
            }

                                                      
            using (AppDbContext context = new())
            {
                foreach (var item in context.PickupPoints)
                {
                    PickupPoints.Add(item.PickupPointName);
                }
            }

            TotalCost = GetTotalCost();
        }

        private decimal GetTotalCost()
        {
            decimal totalCost = 0M;
            foreach (var item in ItemsInCart)
            {
                totalCost += (item.cost * item.count);
            }
            return totalCost;
        }

        public void AddOneUnit(ItemInCart item)
        {
            
        }



    }

   
}
