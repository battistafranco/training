
namespace BasicMessages
{
  public class RemoveProductFromBasketMessage
  {
    public readonly string Name;
    public readonly double Quantity;

    public RemoveProductFromBasketMessage(string name, double quantity)
    {
      Name= name;
      Quantity = quantity;
    }
  }
}
