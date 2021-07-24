using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgerForDiningPhilosophers
{
	class Tester
	{
		private IdxTransformer idxTransformer;
		public Tester(IdxTransformer idxTransformer)
		{
			this.idxTransformer = idxTransformer;
		}

		public bool Test(IEnumerable<Info> result, uint n)
		{
			Debugger.Output("testing...");

			var cnts = new uint[5] { 0, 0, 0, 0, 0 };
			var forkOwner = new int[5] { -1, -1, -1, -1, -1 };

			foreach (var info in result)
			{

				var forkIdx = info.act == Info.Act.Eat ? "" : idxTransformer.GetBesideIdx(info.idx, info.forkDirect).ToString();
				Debugger.Output($"{info.idx}: {info.forkDirect} {info.act} {forkIdx}");

				switch (info.act)
				{
					case Info.Act.Eat:
						if (forkOwner[idxTransformer.GetLeftIdx(info.idx)] != info.idx
							|| forkOwner[idxTransformer.GetRightIdx(info.idx)] != info.idx)
							return false;
						++cnts[info.idx];
						break;
					case Info.Act.Pick:
						if (forkOwner[idxTransformer.GetBesideIdx(info.idx, info.forkDirect)] != -1) return false;
						forkOwner[idxTransformer.GetBesideIdx(info.idx, info.forkDirect)] = info.idx;
						break;
					case Info.Act.Put:
						if (forkOwner[idxTransformer.GetBesideIdx(info.idx, info.forkDirect)] != info.idx) return false;
						forkOwner[idxTransformer.GetBesideIdx(info.idx, info.forkDirect)] = -1;
						break;
					default:
						throw new Exception("Action illegal!");
				}
			}

			foreach (var cnt in cnts)
			{
				if (cnt != n) return false;
			}

			return true;
		}
	}
}
