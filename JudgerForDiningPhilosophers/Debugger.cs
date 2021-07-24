using System;

namespace JudgerForDiningPhilosophers
{
	static class Debugger
	{
		public static void Output(string msg)
		{
#if DEBUG
			Console.WriteLine(msg);
#endif
		}
	}
}
