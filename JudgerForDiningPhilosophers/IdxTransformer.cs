using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgerForDiningPhilosophers
{
	abstract class IdxTransformer
	{
		public abstract int GetLeftIdx(int idx);

		public abstract int GetRightIdx(int idx);

		public int GetBesideIdx(int idx, Info.Direction direction)
		{
			return direction == Info.Direction.Left ? GetLeftIdx(idx) : GetRightIdx(idx);
		}
	}

	class ClockwiseTransformer : IdxTransformer			// 顺时针坐
	{

		public override int GetLeftIdx(int idx)
		{
			return idx;
		}

		public override int GetRightIdx(int idx)
		{
			return (idx + 4) % 5;
		}
	}

	class AntiClockwiseTransformer : IdxTransformer		// 逆时针坐
	{

		public override int GetLeftIdx(int idx)
		{
			return idx;
		}

		public override int GetRightIdx(int idx)
		{
			return (idx + 1) % 5;
		}
	}
}
