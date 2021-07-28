using System;
using System.Collections.Concurrent;
using System.Threading;

using Answer = DiningPhilosophers.Answer2;

namespace JudgerForDiningPhilosophers
{
	class PhilosopherDriver
	{
		private Random rand = new Random();
		private readonly int idx;
		public PhilosopherDriver(int idx, Answer::DiningPhilosophers ans)
		{
			this.idx = idx;
			this.ans = ans;
		}

		private Answer::DiningPhilosophers ans;

		public void Run(uint n, BlockingCollection<Info> result, Mode mode)
		{
			for (uint i = 0; i < n; i++)
			{
				if (mode == Mode.Delay) Thread.Sleep(rand.Next(0, 3));  // Think
				ans.WantsToEat
				(
					idx,
					() => result.Add(new Info(idx, Info.Direction.Left, Info.Act.Pick)),
					() => result.Add(new Info(idx, Info.Direction.Right, Info.Act.Pick)),
					() =>
					{
						result.Add(new Info(idx, Info.Direction.Null, Info.Act.Eat));   // Eat
						if (mode == Mode.Delay) Thread.Sleep(rand.Next(0, 2));
					},
					() => result.Add(new Info(idx, Info.Direction.Left, Info.Act.Put)),
					() => result.Add(new Info(idx, Info.Direction.Right, Info.Act.Put))
				);
			}
		}
	}
}
