using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Messages
{
	class MainClass
	{
		static void Main(string[] args)
		{
      Infraestructure.Print(@"
            Let's create a new product basket to hold our shopping items and simply
            add some products to it directly via traditonal BLOCKING method calls.
            ");

			// Create an instance of the ProductBasket class
			// It's AddProduct method takes the following arguments:
			//   a string with the name of a product we want to buy
			//   and a double number indicating the quantity of that item that we want
			// It then stores that item information in its internal _products Dictonary

			var basket = new ProductBasket();

			// Add some products to that shopping basket
			basket.AddProduct("butter", 1);

			basket.AddProduct("pepper", 2);

			// The code above just used normal blocking method calls
			// to add items direclty into the ProductBasket object instance.
			// That works pretty well when the ProductBasket object happens to be
			// running in the same process and thread as the requestor, but not so well when our
			// ProductBasket is running on some other machine or set of machines.
			// In a distributed computing environment like that,
			// a better approach to executing method calls on remote objects like our
			// ProductBasket is to usa a message class with messaging infrastructure.

			// A "message" is just a regular class that you define that will be used to
			// store the required data that the remote object's parameters need you to pass
			// into it as arguments.
			// So from our first example, we know that when we call the ProductBasket's
			// AddProduct method, we need to supply name and quantity arguments to it.
			// We did that directly above but now we are going to use a message class
			// to store the values of the name and quantity arguements for us.
			// The AddProductToBasketMessage is a class defined lower in this Program.cs file
			// that will do exactly that for us.

      Infraestructure.Print(@"
            Now, to add more stuff to the shopping basket via messaging (instead of a
            direct method call), we create an AddProductToBasketMessage to store our name
            and quantity arguments that will be provided to ProductBasket.AddProduct later
            ");

			// creating a new message to hold the arguments of "5 candles" to be addded to the basket
			var message = new AddProductToBasketMessage("candles",5);

      Infraestructure.Print(@"Now, since we created that message, we will apply its item contents of:
            '" + message + "'" + @" 
            by sending it to the product basket to be handled.");

			ApplyMessage(basket, message);


      Infraestructure.Print(@"
            We don't have to send/apply messages immediately.  We can put messages into 
            some queue and send them later if needed. 
            Let's define more messages to put in a queue:
            ");

			// create more AddProductToBasketMessage's and put them in a queue for processing later
			var queue = new Queue<object>();
			queue.Enqueue(new AddProductToBasketMessage("Chablis wine", 1));
			queue.Enqueue(new AddProductToBasketMessage("shrimps", 10));

			// display each to message on the console
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

			while(queue.Count>0)
			{
				ApplyMessage(basket, queue.Dequeue());
			}

      Infraestructure.Print(@"
            Now let's serialize our message to binary form,
            which allows the message object to travel between processes.
            ");

			

			var serializer = new BinarySerializer();

      Infraestructure.Print(@"
            Serialization is a process of recording an object instance
            (which currenly only exists in RAM/memory)
            to a binary representation (which is a set of bytes).
            Serialization is a way that we can save the state of our
            object instances to persistent (non-memory) storage.
            The code will now create another new message for the 'rosmary' product,
            but this time it will serialize it from RAM to disk.
            ");

			// here is just another message with another product item and quantity
			// we have just decided we are going to serialize this specific one to disk
			var msg = new AddProductToBasketMessage("rosemary", 1);

			// this operation will use memory stream to convert message
			// to in-memory array of bytes, which we will operate later
			byte[] bytes;
			using (var stream = new MemoryStream())
			{
				serializer.WriteMessage(msg, msg.GetType(), stream);
				bytes = stream.ToArray();
			}

      Infraestructure.Print(@"
            Let's see how this 'rosmary' message object would look in its binary form:
            ");
			Console.WriteLine(BitConverter.ToString(bytes).Replace("-",""));
      Infraestructure.Print(@"
            And if we tried to open it in a text editor...
            ");
			Console.WriteLine(Encoding.ASCII.GetString(bytes));

      Infraestructure.Print(@"
            Note the readable string content with some 'garbled' binary data!
            Now we'll save (persist) the 'rosmary' message to disk, in file 'message.bin'.
                
            You can see the message.bin file inside of:
            '" + Path.GetFullPath("message.bin") + @"'
            If you open it with Notepad, you will see the 'rosmary' message waiting on disk for you.
            ");
			File.WriteAllBytes("message.bin", bytes);


      Infraestructure.Print(@"
            Let's read the 'rosmary' message we serialized to file 'message.bin' back into memory.
            The process of reading a serialized object from byte array back into intance in memory 
            is called deserialization.
            ");
			using (var stream = File.OpenRead("message.bin"))
			{
				var readMessage = serializer.ReadMessage(stream);
        Infraestructure.Print("[Serialized Message was read from disk:] " + readMessage);
        Infraestructure.Print(@"Now let's apply that messaage to the product basket.
                ");
				ApplyMessage(basket, readMessage);
			}

      Infraestructure.Print(@"
            Now you've learned what a message is (just a remote temporally
            decoupled message/method call, that can be persisted and then
            dispatched to the place that handles the request.
            You also learned how to actually serialize a message to a binary form
            and then deserialize it and dispatch it the handler.");

      Infraestructure.Print(@"
            As you can see, you can use messages for passing information
            between machines, telling a story and also persisting.
            
            By the way, let's see what we have aggregated in our product basket so far:
            ");

			foreach (var total in basket.GetProductTotals())
			{
				Console.WriteLine("  {0}: {1}", total.Key, total.Value);
			}    
      Console.ReadLine();
		}

		static void ApplyMessage(ProductBasket basket, object message)
		{
			// this code accepts the message and actually adds the product to the supplied basket.
			// we cast both basket and message to dynamic in order
			// to dispatch it dynamically to one of "ProductBasket.When(...)" methods,
			// which specifically can handle this type of the message
			((dynamic) basket).When((dynamic)message);
		}    
	}

}
