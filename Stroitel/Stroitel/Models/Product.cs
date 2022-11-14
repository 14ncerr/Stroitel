using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stroitel.Models
{
    public partial class Product
    {
        private string _photo;
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public string ProductArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProudctUnit { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public int ProductCategory { get; set; }
        public string? ProductPhoto 
        {
            get => (_photo == String.Empty) || (_photo == null)
                ? "\\Resources\\picture.png"
                : $"\\Resources\\Products\\{_photo}";

            set => _photo = value; 
        }
        public int ProductProvider { get; set; }
        public int ProductManufacturer { get; set; }
        public int ProductQuantityInStock { get; set; }
        public decimal ProductCost { get; set; }
        public byte? ProductDiscountAmount { get; set; }
        public byte? ProductMaxDiscountAmount { get; set; }

        public virtual Category ProductCategoryNavigation { get; set; } = null!;
        public virtual Manufacturer ProductManufacturerNavigation { get; set; } = null!;
        public virtual Provider ProductProviderNavigation { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        [NotMapped]
        public decimal? TotalCost
        {
            get => (ProductDiscountAmount > 0)
            ? ProductCost * (1 - (decimal)ProductDiscountAmount / 100)
            : ProductCost;
        }

        [NotMapped]
        public bool HasDiscount
        {
            get => (ProductCost != TotalCost);
        }

        [NotMapped]
        public bool DiscountOver15 => HasDiscount && (ProductDiscountAmount > 15);
    }
}
