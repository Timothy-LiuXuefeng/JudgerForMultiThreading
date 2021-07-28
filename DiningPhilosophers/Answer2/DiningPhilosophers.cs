using System;
using System.Threading;

namespace DiningPhilosophers.Answer2
{
	public class DiningPhilosophers
	{
		private enum PhilosopherState
		{
			Thinking = 0,
			Hungry = 1,
			Eating = 2
		}

		private int GetLeftIdx(int i) => (i + 4) % 5;
		private int GetRightIdx(int i) => (i + 1) % 5;

		private object mutex;
		PhilosopherState[] states;
		Semaphore[] sema;

		public DiningPhilosophers()
		{
			mutex = new object();
			states = new PhilosopherState[5];
			for (int i = 0; i < states.Length; ++i)
			{
				states[i] = PhilosopherState.Thinking;
			}
			sema = new Semaphore[5];
			for (int i = 0; i < sema.Length; ++i)
			{
				sema[i] = new Semaphore(0, 1);
			}
		}

		public void WantsToEat(int idx, Action pickLeft, Action pickRight, Action eat, Action putLeftFork, Action putRightFork)
		{
			TakeForks(idx);
			pickLeft();
			pickRight();
			eat();
			putLeftFork();
			putRightFork();
			PutForks(idx);
		}

		private void TakeForks(int i)
		{
			lock (mutex)
			{
				states[i] = PhilosopherState.Hungry;
				Test(i);
			}
			sema[i].WaitOne();
		}

		private void PutForks(int i)
		{
			lock (mutex)
			{
				states[i] = PhilosopherState.Thinking;
				Test(GetLeftIdx(i));
				Test(GetRightIdx(i));
			}
		}

		private void Test(int i)
		{
			if (
					states[i] == PhilosopherState.Hungry &&
					states[GetLeftIdx(i)] != PhilosopherState.Eating &&
					states[GetRightIdx(i)] != PhilosopherState.Eating
				)
			{
				states[i] = PhilosopherState.Eating;
				sema[i].Release();
			}
		}
	}
}
