using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Original
{
  class Program
  {
    static void Main(string[] args)
    {

      var carrito = new ProductBasket();
     
      //agrego items
      carrito.AddProduct("butter", 1);
      carrito.AddProduct("pepper", 2);

      //elimino items
      carrito.RemoveProduct("pepper", 1);

      //limpiar carrito
      carrito.Clear();

      //agregar otra vez

      //agrego items
      carrito.AddProduct("demo 1", 1);
      carrito.AddProduct("demo 2", 2);

      carrito.Checkout();

      Console.ReadKey();

    }
  }
}
