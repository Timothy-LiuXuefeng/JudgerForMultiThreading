using System;
using System.Threading;

namespace CSharpSemaphoreJudger
{
	class Program
	{
		class TestFailedException : Exception
		{
			private uint n;
			public TestFailedException(uint n)
			{
				this.n = n;
			}
			public override string Message => "Test failed when: n = " + n.ToString();
		}

		static void Main(string[] args)
		{
			var rand = new Random();
			object randLock = new object();
			Func<int, int, int> getRand = (a, b) => { lock (randLock) return rand.Next(a, b); };
			var runners = new Runner[]
			{
				new Runner(),
				new Runner() { Produce = () => { Thread.Sleep(getRand(0, 2)); } },
				new Runner() { Consume = () => { Thread.Sleep(getRand(0, 2)); } },
				new Runner() { Produce = () => { Thread.Sleep(getRand(0, 2)); }, Consume = () => { Thread.Sleep(getRand(0, 2)); } }
			};

			var numbers = new Tuple<int, int>[]
			{
				new Tuple<int, int>(1, 1),
				new Tuple<int, int>(1, 7),
				new Tuple<int, int>(7, 1),
				new Tuple<int, int>(7, 7)
			};

			try
			{
				const int nCapacity = 10;
				for (int n = 1; n <= 15; ++n)
				{
					Console.WriteLine($"Start testing n = {n}...");

					foreach (var runner in runners)
					{
						foreach (var number in numbers)
						{
							var result = runner.Run(nCapacity, number.Item1, number.Item2, n);
							if (!Tester.Test(result, nCapacity, number.Item2 * n))
							{
								throw new TestFailedException((uint)n);
							}
						}
					}
				}
				Console.WriteLine("Test success!");
			}
			catch (TestFailedException e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
