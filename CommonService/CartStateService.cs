using CakeByHtoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.CommonService
{
    public class CartStateService
    {
        private List<Cart> _cartItems = new();

        public IReadOnlyList<Cart> CartItems => _cartItems.AsReadOnly();

        public event Action OnChange;

        public void SetCartItems(List<Cart> items)
        {
            _cartItems = items ?? new List<Cart>();
            NotifyStateChanged();
        }

        public void AddItem(Cart item)
        {
            _cartItems.Add(item);
            NotifyStateChanged();
        }

        public void RemoveItem(Cart item)
        {
            _cartItems.Remove(item);
            NotifyStateChanged();
        }

        public void Clear()
        {
            _cartItems.Clear();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
