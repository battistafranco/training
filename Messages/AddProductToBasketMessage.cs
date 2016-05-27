using System;

namespace Messages
{
  [Serializable]
  public class AddProductToBasketMessage
  {
    public readonly string Name;
    public readonly double Quantity;

    public AddProductToBasketMessage(string name, double quantity)
    {
      Name = name;
      Quantity = quantity;
    }
    public override string ToString()
    {
      return string.Format("Add {0} {1} to basket", Quantity, Name);
    }
  }
}