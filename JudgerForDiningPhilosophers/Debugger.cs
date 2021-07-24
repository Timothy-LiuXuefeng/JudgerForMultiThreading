using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
