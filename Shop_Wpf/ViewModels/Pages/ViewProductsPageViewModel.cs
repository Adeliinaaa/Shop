using GalaSoft.MvvmLight.CommandWpf;
using Shop_Api.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shop_Common.Dtos;
using Shop_Api.Services.ProductService;
using Shop_Api.Models;

namespace Shop_Wpf.ViewModels.Pages
{
    internal class ViewProductsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ViewProductDto> Products { get; set; }
        public ObservableCollection<ProductTypeDto> ProductTypes { get; set; }
        private readonly IProductService _productService;

        public ViewProductsPageViewModel(IProductService iProductService)
        {
            Products = new ObservableCollection<ViewProductDto>();
            ProductTypes = new ObservableCollection<ProductTypeDto>();

            ResetCommand = new RelayCommand(ResetFilters);
            CreateCommand = new RelayCommand(CreateProduct);
            _productService = iProductService;
            LoadProducts();
            LoadProductTypes();
        }

        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                if (_search != value)
                {
                    _search = value;
                    OnPropertyChanged(nameof(Search));
                    ApplyFilters();
                }
            }
        }

        private ProductTypeDto _productType;
        public ProductTypeDto ProductType
        {
            get => _productType;
            set
            {
                if (_productType != value)
                {
                    _productType = value;
                    OnPropertyChanged(nameof(ProductType));
                    ApplyFilters();
                }
            }
        }

        public int QuantityOfProducts => Products.Count;

        private int _quantityOfProductsDisplayed;
        public int QuantityOfProductsDisplayed
        {
            get => _quantityOfProductsDisplayed;
            set
            {
                if (_quantityOfProductsDisplayed != value)
                {
                    _quantityOfProductsDisplayed = value;
                    OnPropertyChanged(nameof(QuantityOfProductsDisplayed)); // Уведомляем об изменении
                }
            }
        }

        public ICommand ResetCommand { get; set; }
        public ICommand CreateCommand { get; set; }

        private ViewProductDto MapToViewProductDto(Product product)
        {
            return new ViewProductDto
            {
                ProductArticleNumber = product.ProductArticleNumber,
                Name = product.Name,
                Measure = product.Measure,
                Cost = product.Cost,
                Description = product.Description ?? "",
                ProductType = product.ProductType != null ? product.ProductType.Name ?? "Неизвестно" : "Неизвестно",
                Supplier = product.Supplier != null ? product.Supplier.Name ?? "Неизвестно" : "Неизвестно",
                Manufacturer = product.Manufacturer != null ? product.Manufacturer.Name ?? "Неизвестно" : "Неизвестно",
                CurrentDiscount = product.CurrentDiscount ?? 0,
                Status = product.Status,
                QuantityInStock = product.QuantityInStock
            };
        }

        private async void LoadProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            Products.Clear();

            foreach (var product in products)
            {
                Products.Add(MapToViewProductDto(product));
            }

            // Обновляем количество отображенных продуктов
            QuantityOfProductsDisplayed = Products.Count;
        }

        private async void LoadProductTypes()
        {
            var productTypes = await _productService.GetProductTypesAsync();
            ProductTypes.Clear();

            foreach (var productType in productTypes)
            {
                ProductTypes.Add(productType);
            }
        }

        private async void ApplyFilters()
        {
            var filteredProducts = await _productService.GetAllProductsAsync();
            if (!string.IsNullOrEmpty(Search))
            {
                filteredProducts = filteredProducts
                    .Where(p => p.Name.Contains(Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (ProductType != null)
            {
                filteredProducts = filteredProducts
                    .Where(p => p.ProductTypeId == ProductType.ProductTypeId)
                    .ToList();
            }

            Products.Clear();
            foreach (var product in filteredProducts)
            {
                Products.Add(MapToViewProductDto(product));
            }

            QuantityOfProductsDisplayed = Products.Count;
        }

        private void ResetFilters()
        {
            Search = string.Empty;
            ProductType = null;
            LoadProducts();
        }

        private void CreateProduct()
        {
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
