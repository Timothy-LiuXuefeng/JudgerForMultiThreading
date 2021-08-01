using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

using Answer = CSharpSemaphore;

namespace CSharpSemaphoreJudger
{
	enum ResourceOperation
	{
		Release = 0,
		Acquire = 1
	}

	class Runner
	{
		public Action Produce { private get; set; } = () => { };
		public Action Consume { private get; set; } = () => { };

		public IEnumerable<ResourceOperation> Run(int nCapacity, int nProducers, int nConsumers, int nTurn)
		{
			var result = new BlockingCollection<ResourceOperation>();

			var empty = new Answer::MySemaphore(nCapacity, nCapacity);
			var full = new Answer::MySemaphore(0, nCapacity);

			var thrdConsumer = new Thread[nConsumers];

			for (int i = 0; i < nConsumers; ++i)
			{
				thrdConsumer[i] = new Thread
					(
						() =>
						{
							for (int j = 0; j < nTurn; ++j)
							{
								full.Down();
								result.Add(ResourceOperation.Acquire);
								empty.Up();
								Consume();
							}
						}
					);
				thrdConsumer[i].Start();
			}

			for (int i = 0; i < nProducers; ++i)
			{
				new Thread
					(
						() =>
						{
							try
							{
								while (true)
								{
									Produce();
									empty.Down();
									result.Add(ResourceOperation.Release);
									full.Up();
								}
							}
							catch { }
						}
					)
				{ IsBackground = true }.Start();
			}

			foreach (var thrd in thrdConsumer)
			{
				thrd.Join();
			}

			result.CompleteAdding();
			return result;
		}
	}
}
