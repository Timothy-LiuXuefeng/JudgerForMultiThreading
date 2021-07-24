using System;

namespace JudgerForDiningPhilosophers
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
			try
			{
				var idxTransformers = new IdxTransformer[2]
				{
				new ClockwiseTransformer(),
				new AntiClockwiseTransformer()
				};

				var testers = new Tester[2]
				{
				new Tester(idxTransformers[0]),
				new Tester(idxTransformers[1])
				};

				var runner = new Runner();

				uint maxTime = 35;

				for (uint i = 0; i < maxTime; ++i)
				{
					uint n = i + 1;
					Console.WriteLine($"Start testing n = {n}...");

					for (int modeCnt = 0; modeCnt < 2; ++modeCnt)
					{
						Mode mode = modeCnt == 0 ? Mode.Delay : Mode.NoDelay;
						var result = runner.Run(n, mode);

						//foreach (var info in result)
						//{
						//	var forkIdx = info.act == Info.Act.Eat ? "" : idxTransformers[j].GetBesideIdx(info.idx, info.forkDirect).ToString();
						//	Console.WriteLine($"{info.idx}: {info.forkDirect} {info.act} {forkIdx}");
						//}
						//Console.WriteLine("=================");

						Debugger.Output($"Mode: {mode} ends running");

						if (!testers[0].Test(result, n) && !testers[1].Test(result, n))
						{
							throw new TestFailedException(n);
						}

					}
				}

				Console.WriteLine("Test success!");
			}
			catch (TestFailedException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
