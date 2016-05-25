using System;
using System.IO;

namespace Messages
{
	public interface ISerializer
	{
		void WriteMessage(object message, Type type, Stream stream);
		object ReadMessage (Stream stream);
	}
}

