using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Original
{
  public class ProductBasket
  {
    readonly IDictionary<string, double> _products = new Dictionary<string, double>();

    internal void AddProduct(string name, double quantity)
    {
      double currentQuantity;
      if (!_products.TryGetValue(name, out currentQuantity))
      {
        currentQuantity = 0;
      }
      _products[name] = quantity + currentQuantity;

      Console.WriteLine("Shopping Basket said: I added {0} unit(s) of '{1}'", quantity, name);
    }

    internal void RemoveProduct(string name , double quantity)
    {
      double currentQuantity;
      if (_products.TryGetValue(name, out currentQuantity))
      {
        var newQuantity = currentQuantity-quantity ;

        if (newQuantity > 0)
        {
          _products[name] = newQuantity;
          Console.WriteLine("Shopping Basket said: I removed {0} unit(s) of '{1}'", quantity, name);
        }
        else
        {
          _products.Remove(name);
          Console.WriteLine("Shopping Basket said: I removed completly '{1}' from cart", quantity, name);
        }
      }
      

      
    }

    internal void Clear()
    {
      _products.Clear();
      Console.WriteLine("Basket is now empty");
    }

    internal void Checkout()
    {
      //some really cool paypal or other integration;
      foreach (var item in _products)
      {
      Console.WriteLine("checked out! {1} unit of {0}",item.Key, item.Value);  
      }
      Console.WriteLine("done check your credit card");
    }
  }
}
