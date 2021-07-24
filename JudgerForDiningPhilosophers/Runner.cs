using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

using Answer = DiningPhilosophers.Answer1;

namespace JudgerForDiningPhilosophers
{
	enum Mode
	{
		Delay = 0,
		NoDelay = 1
	}

	class Runner
	{
		private Answer::DiningPhilosophers ans;
		PhilosopherDriver[] drivers = new PhilosopherDriver[5];

		public Runner()
		{
			ans = new Answer::DiningPhilosophers();
			drivers = new PhilosopherDriver[5];
			for (int i = 0; i < 5; ++i)
			{
				drivers[i] = new PhilosopherDriver(i, ans);
			}
		}

		public IEnumerable<Info> Run(uint n, Mode mode)
		{
			var result = new BlockingCollection<Info>();
			var order = new int[] { 0, 1, 2, 3, 4 };
			Random rand = new Random();

			for (int i = 0; i < 5; ++i)		// Random shuffle
			{
				int newIdx = rand.Next(0, 5);
				if (newIdx != i)			// Swap (i, newIdx)
				{
					int tmp = order[i];
					order[i] = order[newIdx];
					order[newIdx] = tmp;
				}
			}

			var thrds = new Thread[5];
			
			for (int i = 0; i < 5; ++i)
			{
				int idx = i;
				thrds[idx] = new Thread(() => { drivers[idx].Run(n, result, mode); });
			}
			foreach (var i in order)
			{
				thrds[i].Start();
			}

			foreach (var thrd in thrds)
			{
				thrd.Join();
			}
			return result;
		}
	}
}
