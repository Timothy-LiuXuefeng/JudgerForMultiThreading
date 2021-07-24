using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningPhilosophers.Answer1
{
	public class DiningPhilosophers
	{
		private object[] mtx;

		public DiningPhilosophers()
		{
			mtx = new object[5];
			for (int i = 0; i < 5; i++)
			{
				mtx[i] = new object();
			}
		}

		public void WantsToEat(int idx, Action pickLeft, Action pickRight, Action eat, Action putLeftFork, Action putRightFork)
		{
			if (idx == 0)
			{
				lock (mtx[0])
				{
					pickLeft();
					lock (mtx[4])
					{
						pickRight();
						eat();
						putRightFork();
					}
					putLeftFork();
				}
			}
			else
			{
				lock (mtx[idx - 1])
				{
					pickRight();
					lock (mtx[idx])
					{
						pickLeft();
						eat();
						putLeftFork();
					}
					putRightFork();
				}
			}
		}
	}
}
