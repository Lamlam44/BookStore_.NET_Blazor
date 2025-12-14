using StoreManagement.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreManagement.Client.Services
{
    public class CartService
    {
        private readonly List<CartItem> _items = new();

        public event Action? OnChange;

        public void AddToCart(Book book)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == book.Id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _items.Add(new CartItem
                {
                    ProductId = book.Id,
                    ProductName = book.Title,
                    Price = book.RetailPrice,
                    Quantity = 1,
                    Image = book.Image
                });
            }

            NotifyStateChanged();
        }
        
        public IReadOnlyList<CartItem> GetCartItems() => _items.AsReadOnly();

        public void RemoveFromCart(string productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _items.Remove(item);
                NotifyStateChanged();
            }
        }

        public decimal GetTotal() => _items.Sum(i => i.Price * i.Quantity);

        public void Clear()
        {
            _items.Clear();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
