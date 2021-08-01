using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTool;

namespace CSharpSemaphoreJudger
{
	static class Tester
	{
		public static bool Test(IEnumerable<ResourceOperation> result, int nCapacity, int nTotal)
		{
			int cnt = 0, total = 0;
			foreach (var operation in result)
			{
				switch (operation)
				{
					case ResourceOperation.Release:
						Debugger.Output("Release");
						if (cnt == nCapacity) return false;
						++cnt;
						break;
					case ResourceOperation.Acquire:
						Debugger.Output("Acquire");
						if (cnt == 0) return false;
						--cnt;
						++total;
						break;
					default:
						Debugger.Output("Unknown operation");
						return false;
				}
			}
			return total == nTotal;
		}
	}
}
