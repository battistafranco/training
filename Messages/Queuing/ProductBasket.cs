using System;
using System.Collections.Generic;

namespace Queuing
{
  public class ProductBasket
  {
    readonly IDictionary<string, double> _products = new Dictionary<string, double>();

    public void When(AddProductToBasketMessage toBasketMessage)
    {
      Console.Write("[Message Applied]: ");
      AddProduct(toBasketMessage.Name, toBasketMessage.Quantity);
    }

    public void When(RemoveProductFromBasketMessage toBasketMessage)
    {
      Console.Write("[Message Applied]: ");
      RemoveProduct(toBasketMessage.Name, toBasketMessage.Quantity);
    }   

    public void When(ClearBasketMessage toBasketMessage)
    {
      Console.Write("[Message Applied]: ");
      Clear();
    }

    public void When(CheckoutBasketMessage toBasketMessage)
    {
      Console.Write("[Message Applied]: ");
      Checkout();
    }

    private void Clear()
    {
      _products.Clear();
      Console.WriteLine("Basket is now empty");
    }

    private void Checkout()
    {
      //some really cool paypal or other integration;
      foreach (var item in _products)
      {
        Console.WriteLine("checked out! {1} unit of {0}", item.Key, item.Value);
      }
      Console.WriteLine("done check your credit card");
    }

    private void RemoveProduct(string name, double quantity)
    {
      double currentQuantity;
      if (_products.TryGetValue(name, out currentQuantity))
      {
        var newQuantity = currentQuantity - quantity;

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

    private void AddProduct(string name, double quantity)
    {
      double currentQuantity;
      if (!_products.TryGetValue(name, out currentQuantity))
      {
        currentQuantity = 0;
      }
      _products[name] = quantity + currentQuantity;

      Console.WriteLine("Shopping Basket said: I added {0} unit(s) of '{1}'", quantity, name);
    }
  }
}
