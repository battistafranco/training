using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Messages
{
	public class BinarySerializer:ISerializer
	{
		readonly BinaryFormatter _formatter= new BinaryFormatter();

		public void WriteMessage (object message, Type type, Stream stream)
		{
			_formatter.Serialize (stream,message);
		}

		public object ReadMessage (Stream stream)
		{
			return _formatter.Deserialize (stream);
		}
	}
}

