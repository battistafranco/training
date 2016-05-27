using System;
using System.Collections.Generic;

namespace Messages
{
  public class ProductBasket
  {
    readonly IDictionary<string, double> _products = new Dictionary<string, double>();

    public void AddProduct(string name, double quantity)
    {
      double currentQuantity;
      if (!_products.TryGetValue(name, out currentQuantity))
      {
        currentQuantity = 0;
      }
      _products[name] = quantity + currentQuantity;

      Console.WriteLine("Shopping Basket said: I added {0} unit(s) of '{1}'", quantity, name);
    }

    public void When(AddProductToBasketMessage toBasketMessage)
    {
      Console.Write("[Message Applied]: ");
      AddProduct(toBasketMessage.Name, toBasketMessage.Quantity);
    }

    public IDictionary<string, double> GetProductTotals()
    {
      return _products;
    }
  }
}