using System;

namespace Queuing
{
  public static class Infraestructure
  {
    public static void Print(string message)
    {
      // just printing messages nicely
      // and without spaces in the beginning
      var oldColor = Console.ForegroundColor;
      foreach (var line in message.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
      {
        var trimmed = line.TrimStart();

        if (trimmed.StartsWith("#"))
        {
          Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        else if (trimmed.StartsWith("*") | trimmed.StartsWith("- "))
        {
          Console.ForegroundColor = ConsoleColor.DarkBlue;
        }
        else
        {
          Console.ForegroundColor = ConsoleColor.White;
        }
        
        Console.WriteLine(trimmed);
      }
      Console.ForegroundColor = oldColor;
    }  
  }
}
