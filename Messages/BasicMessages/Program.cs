using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicMessages
{
  class Program
  {
    static void Main(string[] args)
    {

      var carrito = new ProductBasket();
      //agrego items     
      ApplyMessage(carrito, new AddProductToBasketMessage("butter", 1));
      ApplyMessage(carrito, new AddProductToBasketMessage("pepper", 2));


      //elimino items
      ApplyMessage(carrito, new RemoveProductFromBasketMessage("pepper", 1));      

      //limpiar carrito
      ApplyMessage(carrito, new ClearBasketMessage());      

      //agregar otra vez

      ApplyMessage(carrito, new AddProductToBasketMessage("demo 1", 1));
      ApplyMessage(carrito, new AddProductToBasketMessage("demo 2", 2));

      //checkout
      ApplyMessage(carrito, new CheckoutBasketMessage());

      Console.ReadKey();

     
    }

    static void ApplyMessage(ProductBasket basket, object message)
    {
      // this code accepts the message and actually adds the product to the supplied basket.
      // we cast both basket and message to dynamic in order
      // to dispatch it dynamically to one of "ProductBasket.When(...)" methods,
      // which specifically can handle this type of the message
      ((dynamic)basket).When((dynamic)message);
    }    
  }
}
