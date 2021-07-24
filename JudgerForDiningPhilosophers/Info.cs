using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgerForDiningPhilosophers
{
	struct Info
	{
		public enum Direction
		{
			Null = 0,
			Left = 1,
			Right = 2
		}

		public enum Act
		{
			Pick,
			Put,
			Eat
		}

		public int idx;
		public Direction forkDirect;
		public Act act;

		public Info(int idx, Direction forkDirect, Act act)
		{
			this.idx = idx;
			this.forkDirect = forkDirect;
			this.act = act;
		}
	}
}
