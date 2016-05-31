using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queuing
{
  class Program
  {
    static void Main(string[] args)
    {

      var carrito = new ProductBasket();
      // background process.

      var queue = new Queue<object>();
      queue.Enqueue(new AddProductToBasketMessage("Chablis wine", 1));
      queue.Enqueue(new AddProductToBasketMessage("shrimps", 10));

      // nicer messages
      foreach (var enqueuedMessage in queue)
      {
        Infraestructure.Print(" [Message in Queue is:] * " + enqueuedMessage);
      }


      Infraestructure.Print(@"
            This is what temporal decoupling is. Our product basket does not 
            need to be available at the same time that we create and memorize
            our messages. This will be extremely important, when we get to 
            building systems that balance load and can deal with failures.
            Now that we feel like it, let's send our messages that we put in the
            queue to the ProductBasket:
            ");

      while (queue.Count > 0)
      {
        ApplyMessage(carrito, queue.Dequeue());
      }

      Console.ReadKey();
    }

    static void ApplyMessage(ProductBasket basket, object message)
    {    
      ((dynamic)basket).When((dynamic)message);
    }    
  }
}
