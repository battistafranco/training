
namespace Queuing
{
   public class AddProductToBasketMessage
  {
    public string Name;
    public double Quantity;

     public AddProductToBasketMessage(string name, double quantity)
     {
       Name = name;
       Quantity = quantity;
     }
  }
}
