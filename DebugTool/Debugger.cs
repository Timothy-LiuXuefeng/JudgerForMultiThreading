using System;

namespace DebugTool
{
	public static class Debugger
	{
		public static void Output(string msg)
		{
#if DEBUG
			Console.WriteLine(msg);
#endif
		}
	}
}
