using System;
using System.Threading;

namespace CSharpSemaphore
{
	public class MySemaphore
	{
		private int cnt;
		private int max;
		private object mtx = new object();
		
		public MySemaphore(int initCnt, int maxCnt)
		{
			if (initCnt < 0 || maxCnt <= 0 || initCnt > maxCnt)
			{
				throw new ArgumentException("Illegal argument!");
			}

			cnt = initCnt;
			max = maxCnt;
		}

		public void Down()
		{
			lock (mtx)
			{
				while (cnt == 0)
				{
					Monitor.Wait(mtx);
				}
				--cnt;
			}
		}

		public void Up()
		{
			lock (mtx)
			{
				if (cnt == max)
				{
					throw new SemaphoreFullException("My semaphore is full!");
				}
				++cnt;
				Monitor.Pulse(mtx);
			}
		}
	}
}
